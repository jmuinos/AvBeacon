﻿using System.Security.Cryptography;
using AvBeacon.Application.Abstractions.Cryptography;
using AvBeacon.Domain.Users.Shared;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace AvBeacon.Infrastructure.Cryptography;

/// <summary>
///     Represents the password hasher, used for hashing passwords and verifying hashed passwords.
/// </summary>
internal sealed class PasswordHasher : IPasswordHasher, IPasswordHashChecker, IDisposable
{
    private const KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;
    private const int IterationCount = 10000;
    private const int NumberOfBytesRequested = 256 / 8;
    private const int SaltSize = 128 / 8;
    private readonly RandomNumberGenerator _rng;

    /// <summary>
    ///     Initializes a new instance of the <see cref="PasswordHasher" /> class.
    /// </summary>
    public PasswordHasher()
    {
        _rng = RandomNumberGenerator.Create();
    }

    /// <inheritdoc />
    public void Dispose()
    {
        _rng.Dispose();
    }

    /// <inheritdoc />
    public bool HashesMatch(string passwordHash, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(passwordHash);
        ArgumentNullException.ThrowIfNull(providedPassword);

        var decodedHashedPassword = Convert.FromBase64String(passwordHash);
        return decodedHashedPassword.Length != 0 && VerifyPasswordHashInternal(decodedHashedPassword, providedPassword);
    }

    /// <inheritdoc />
    public string HashPassword(Password password)
    {
        ArgumentNullException.ThrowIfNull(password);
        var hashedPassword = Convert.ToBase64String(HashPasswordInternal(password));

        return hashedPassword;
    }

    /// <summary>
    ///     Returns the bytes of the hash for the specified password.
    /// </summary>
    /// <param name="password"> The password to be hashed. </param>
    /// <returns> The bytes of the hash for the specified password. </returns>
    private byte[] HashPasswordInternal(string password)
    {
        var salt = GetRandomSalt();

        var subKey = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, NumberOfBytesRequested);

        var outputBytes = new byte[salt.Length + subKey.Length];

        Buffer.BlockCopy(salt, 0, outputBytes, 0, salt.Length);

        Buffer.BlockCopy(subKey, 0, outputBytes, salt.Length, subKey.Length);

        return outputBytes;
    }

    /// <summary>
    ///     Gets a randomly generated salt.
    /// </summary>
    /// <returns> The randomly generated salt. </returns>
    private byte[] GetRandomSalt()
    {
        var salt = new byte[SaltSize];

        _rng.GetBytes(salt);

        return salt;
    }

    /// <summary>
    ///     Verifies the bytes of the hashed password with the specified password.
    /// </summary>
    /// <param name="hashedPassword"> The bytes of the hashed password. </param>
    /// <param name="password"> The password to verify with. </param>
    /// <returns> True if the hashes match, otherwise false. </returns>
    private static bool VerifyPasswordHashInternal(byte[] hashedPassword, string password)
    {
        try
        {
            var salt = new byte[SaltSize];

            Buffer.BlockCopy(hashedPassword, 0, salt, 0, salt.Length);

            var subKeyLength = hashedPassword.Length - salt.Length;

            if (subKeyLength < SaltSize) return false;

            var expectedSubKey = new byte[subKeyLength];

            Buffer.BlockCopy(hashedPassword, salt.Length, expectedSubKey, 0, expectedSubKey.Length);

            var actualSubKey = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, subKeyLength);

            return ByteArraysEqual(actualSubKey, expectedSubKey);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    ///     Returns true if the specified byte arrays are equal, otherwise false.
    /// </summary>
    /// <param name="a"> The first byte array. </param>
    /// <param name="b"> The second byte array. </param>
    /// <returns> True if the arrays are equal, otherwise false. </returns>
    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a == null && b == null) return true;

        if (a == null || b == null || a.Length != b.Length) return false;

        var areSame = true;

        for (var i = 0; i < a.Length; i++) areSame &= a[i] == b[i];

        return areSame;
    }
}
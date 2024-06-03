﻿using AvBeacon.Domain._Core.Abstractions.Errors;
using AvBeacon.Domain._Core.Abstractions.Primitives;
using AvBeacon.Domain._Core.Abstractions.Primitives.Result;

namespace AvBeacon.Domain.Users;

/// <summary>Representa el value object de tipo last name.</summary>
public sealed class LastName : ValueObject
{
    /// <summary>Es la longitud máxima del last name.</summary>
    private const int MaxLength = 100;

    /// <summary>Inicializa una nueva instancia de la clase <see cref="LastName" /></summary>
    /// <param name="value">El valor del apellido.</param>
    private LastName(string value) { Value = value; }

    /// <summary>Obtiene el valor del last name.</summary>
    public string Value { get; }


    public static Result<LastName> Create(string lastName)
    {
        return string.IsNullOrWhiteSpace(lastName)
                   ? Result.Failure<LastName>(DomainErrors.LastName.NullOrEmpty)
                   : lastName.Length > MaxLength
                       ? Result.Failure<LastName>(DomainErrors.LastName.LongerThanAllowed)
                       : Result.Success(new LastName(lastName));
    }

    public static implicit operator string(LastName lastName) { return lastName.Value; }

    /// <inheritdoc />
    public override string ToString() { return Value; }

    /// <inheritdoc />
    protected override IEnumerable<object> GetAtomicValues() { yield return Value; }
}
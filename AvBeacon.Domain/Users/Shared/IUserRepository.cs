using AvBeacon.Domain._Core.Abstractions;
using AvBeacon.Domain._Core.Primitives.Maybe;

namespace AvBeacon.Domain.Users.Shared;

/// <summary>
///     Represents the user repository interface.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    ///     Gets the user with the specified email.
    /// </summary>
    /// <param name="email"> The user email. </param>
    /// <returns> The maybe instance that may contain the user with the specified email. </returns>
    Task<Maybe<User>> GetByEmailAsync(Email email);

    /// <summary>
    ///     Checks if the specified email is unique.
    /// </summary>
    /// <param name="email"> The email. </param>
    /// <returns> True if the specified email is unique, otherwise false. </returns>
    Task<bool> IsEmailUniqueAsync(Email email);

    Task<List<User>> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
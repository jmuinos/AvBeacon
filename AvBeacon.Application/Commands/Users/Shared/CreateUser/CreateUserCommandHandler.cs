﻿using AvBeacon.Application.Abstractions.Authentication;
using AvBeacon.Application.Abstractions.Cryptography;
using AvBeacon.Application.Abstractions.Data;
using AvBeacon.Application.Abstractions.Messaging;
using AvBeacon.Contracts.Authentication;
using AvBeacon.Domain._Core.Errors;
using AvBeacon.Domain._Core.Factories;
using AvBeacon.Domain._Core.Primitives.Result;
using AvBeacon.Domain.Users.Applicants;
using AvBeacon.Domain.Users.Shared;

namespace AvBeacon.Application.Commands.Users.Shared.CreateUser;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<TokenResponse>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEnumerable<IUserFactory> _userFactories;
    private readonly IUserRepository _userRepository;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CreateUserCommand" /> class.
    /// </summary>
    /// <param name="userRepository"> The user repository. </param>
    /// <param name="passwordHasher"> The password hasher. </param>
    /// <param name="unitOfWork"> The unit of work. </param>
    /// <param name="jwtProvider"> The JWT provider. </param>
    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IEnumerable<IUserFactory> userFactories)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork     = unitOfWork;
        _jwtProvider    = jwtProvider;
        _userFactories  = userFactories;
    }

    public async Task<Result<TokenResponse>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var firstNameResult = FirstName.Create(command.FirstName);
        var lastNameResult = LastName.Create(command.LastName);
        var emailResult = Email.Create(command.Email);
        var passwordResult = Password.Create(command.Password);

        var result = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult, emailResult, passwordResult);
        if (result.IsFailure)
            return Result.Failure<TokenResponse>(result.Error);

        if (!await _userRepository.IsEmailUniqueAsync(emailResult.Value))
            return Result.Failure<TokenResponse>(DomainErrors.Users.DuplicateEmail);

        var passwordHash = _passwordHasher.HashPassword(passwordResult.Value);

        User user;
        switch (command)
        {
            case { UserType: "Applicant" }:
                user = CreateUser<Applicant>(firstNameResult.Value, lastNameResult.Value, emailResult.Value,
                                             passwordHash);
                break;
            case { UserType: "Recruiter" }:
                user = CreateUser<Applicant>(firstNameResult.Value, lastNameResult.Value, emailResult.Value,
                                             passwordHash);
                break;
            default:
                return Result.Failure<TokenResponse>(DomainErrors.Users.InvalidPermissions);
        }

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtProvider.Create(user);

        return Result.Success(new TokenResponse(token));
    }

    private User CreateUser<TUser>(FirstName firstName, LastName lastName, Email email, string passwordHash)
        where TUser : User
    {
        var factory = _userFactories.OfType<UserFactoryBase<TUser>>().FirstOrDefault();
        if (factory == null)
            throw new InvalidOperationException($"Factory for type {typeof(TUser).Name} not registered.");

        return factory.Create(firstName, lastName, email, passwordHash);
    }
}
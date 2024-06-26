﻿using AvBeacon.Application._Core.Abstractions.Authentication;
using AvBeacon.Application._Core.Abstractions.Cryptography;
using AvBeacon.Application._Core.Abstractions.Data;
using AvBeacon.Application._Core.Abstractions.Messaging;
using AvBeacon.Contracts.Responses;
using AvBeacon.Domain._Core.Errors;
using AvBeacon.Domain._Core.Primitives.Result;
using AvBeacon.Domain.Entities;
using AvBeacon.Domain.Repositories;
using AvBeacon.Domain.ValueObjects;

namespace AvBeacon.Application.Users.Commands.Create;

internal sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, Result<TokenResponse>>
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    /// <summary> Initializes a new instance of the <see cref="CreateUserCommand" /> class. </summary>
    /// <param name="userRepository"> The user repository. </param>
    /// <param name="passwordHasher"> The password hasher. </param>
    /// <param name="unitOfWork"> The unit of work. </param>
    /// <param name="jwtProvider"> The JWT provider. </param>
    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher,
        IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
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
            return Result.Failure<TokenResponse>(DomainErrors.User.DuplicateEmail);

        var passwordHash = _passwordHasher.HashPassword(passwordResult.Value);

        User user;
        switch (command)
        {
            case { UserType: "Applicant" }:
                user = Applicant.Create(firstNameResult.Value, lastNameResult.Value, emailResult.Value, passwordHash);
                break;
            case { UserType: "Recruiter" }:
                user = Recruiter.Create(firstNameResult.Value, lastNameResult.Value, emailResult.Value, passwordHash);
                break;
            default:
                return Result.Failure<TokenResponse>(DomainErrors.User.InvalidPermissions);
        }

        _userRepository.Insert(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var token = _jwtProvider.Create(user);

        return Result.Success(new TokenResponse(token));
    }
}
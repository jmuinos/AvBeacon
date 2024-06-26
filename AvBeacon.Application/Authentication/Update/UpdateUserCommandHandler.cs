﻿using AvBeacon.Application._Core.Abstractions.Authentication;
using AvBeacon.Application._Core.Abstractions.Data;
using AvBeacon.Application._Core.Abstractions.Messaging;
using AvBeacon.Domain._Core.Errors;
using AvBeacon.Domain._Core.Primitives.Maybe;
using AvBeacon.Domain._Core.Primitives.Result;
using AvBeacon.Domain.Entities;
using AvBeacon.Domain.Repositories;
using AvBeacon.Domain.ValueObjects;

namespace AvBeacon.Application.Authentication.Update
{
    /// <summary>
    /// Represents the <see cref="UpdateUserCommand"/> handler.
    /// </summary>
    internal sealed class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result>
    {
        private readonly IUserIdentifierProvider _userIdentifierProvider;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserCommandHandler"/> class.
        /// </summary>
        /// <param name="userIdentifierProvider">The user identifier provider.</param>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="unitOfWork">The unit of work.</param>
        public UpdateUserCommandHandler(
            IUserIdentifierProvider userIdentifierProvider,
            IUserRepository userRepository,
            IUnitOfWork unitOfWork)
        {
            _userIdentifierProvider = userIdentifierProvider;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        /// <inheritdoc />
        public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UserId != _userIdentifierProvider.UserId)
                return Result.Failure(DomainErrors.User.InvalidPermissions);

            var firstNameResult = FirstName.Create(request.FirstName);
            var lastNameResult = LastName.Create(request.LastName);
            var firstFailureOrSuccess = Result.FirstFailureOrSuccess(firstNameResult, lastNameResult);
            if (firstFailureOrSuccess.IsFailure)
                return Result.Failure(firstFailureOrSuccess.Error);

            var maybeUser = await _userRepository.GetByIdAsync(request.UserId);
            if (maybeUser.HasNoValue)
                return Result.Failure(DomainErrors.User.NotFound);

            maybeUser.Value.ChangeName(firstNameResult.Value, lastNameResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

﻿using AvBeacon.Domain._Core.Abstractions.Primitives.Result;
using MediatR;

namespace AvBeacon.Application.Applicants.Commands;

public class CreateApplicantCommand(string email, string firstName, string lastName, string password)
    : IRequest<Result<Guid>>
{
    public required string Email { get; init; } = email;
    public required string FirstName { get; init; } = firstName;
    public required string LastName { get; init; } = lastName;
    public required string Password { get; init; } = password;
}
﻿using AvBeacon.Application.Abstractions.Messaging;
using AvBeacon.Domain.Core.Primitives.Result;

namespace AvBeacon.Application.Authentication.ChangePassword;

/// <summary> Represents the change password command. </summary>
public sealed record ChangePasswordCommand(Guid UserId, string Password) : ICommand<Result>;
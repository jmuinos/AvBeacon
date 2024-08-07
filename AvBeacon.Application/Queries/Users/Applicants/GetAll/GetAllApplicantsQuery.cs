﻿using AvBeacon.Application.Abstractions.Messaging;
using AvBeacon.Contracts.Users;
using AvBeacon.Domain._Core.Primitives.Maybe;

namespace AvBeacon.Application.Queries.Users.Applicants.GetAll;

/// <summary>
///     Representa la consulta para obtener todos los solicitantes.
/// </summary>
public sealed class GetAllApplicantsQuery : IQuery<Maybe<List<UserResponse>>>;
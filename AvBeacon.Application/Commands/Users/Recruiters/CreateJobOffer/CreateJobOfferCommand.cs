﻿using AvBeacon.Application.Abstractions.Messaging;
using AvBeacon.Domain._Core.Primitives.Result;

namespace AvBeacon.Application.Commands.Users.Recruiters.CreateJobOffer;

/// <summary>
///     Representa el comando para crear una oferta de empleo.
/// </summary>
/// <param name="Title"> El título de la oferta de empleo. </param>
/// <param name="Description"> La descripción de la oferta de empleo. </param>
/// <returns> Un resultado del comando que indica el éxito o fracaso de la operación. </returns>
public sealed record CreateJobOfferCommand(Guid RecruiterId, string Title, string Description)
    : ICommand<Result<Guid>>;
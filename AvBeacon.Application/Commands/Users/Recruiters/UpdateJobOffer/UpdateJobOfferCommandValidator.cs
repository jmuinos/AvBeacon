﻿using AvBeacon.Application._Core.Errors;
using AvBeacon.Application._Core.Extensions;
using FluentValidation;

namespace AvBeacon.Application.Commands.Users.Recruiters.UpdateJobOffer;

/// <summary>
///     Validador para el comando <see cref="UpdateJobOfferCommand" />.
/// </summary>
internal sealed class UpdateJobOfferCommandValidator : AbstractValidator<UpdateJobOfferCommand>
{
    /// <summary>
    ///     Inicializa una nueva instancia de la clase <see cref="UpdateJobOfferCommandValidator" />
    /// </summary>
    public UpdateJobOfferCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithError(ValidationErrors.UpdateJobOffer.JobOfferIdIsRequired);

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithError(ValidationErrors.UpdateJobOffer.TitleIsRequired)
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithError(ValidationErrors.UpdateJobOffer.DescriptionIsRequired)
            .MaximumLength(500);
    }
}
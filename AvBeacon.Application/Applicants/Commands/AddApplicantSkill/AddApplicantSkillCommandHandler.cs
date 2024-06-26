﻿using AvBeacon.Application._Core.Abstractions.Data;
using AvBeacon.Application._Core.Abstractions.Messaging;
using AvBeacon.Domain._Core.Errors;
using AvBeacon.Domain._Core.Primitives.Result;
using AvBeacon.Domain.Repositories;

namespace AvBeacon.Application.Applicants.Commands.AddApplicantSkill;

/// <summary> Handler para el comando <see cref="AddApplicantSkillCommand" />. </summary>
internal sealed class AddApplicantSkillCommandHandler : ICommandHandler<AddApplicantSkillCommand, Result>
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ISkillRepository _skillRepository;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary> Initializes a new instance of the <see cref="AddApplicantSkillCommandHandler" /> class. </summary>
    /// <param name="skillRepository"> The skill repository. </param>
    /// <param name="applicantRepository"> The applicant repository. </param>
    /// <param name="unitOfWork"> The unit of work. </param>
    public AddApplicantSkillCommandHandler(
        ISkillRepository skillRepository,
        IApplicantRepository applicantRepository,
        IUnitOfWork unitOfWork)
    {
        _skillRepository = skillRepository;
        _applicantRepository = applicantRepository;
        _unitOfWork = unitOfWork;
    }

    /// <summary> Maneja la adición de una habilidad a un solicitante. </summary>
    /// <param name="request"> El comando de adición de habilidad a solicitante. </param>
    /// <param name="cancellationToken"> El token de cancelación. </param>
    /// <returns> Un resultado que indica el éxito o fracaso de la operación. </returns>
    public async Task<Result> Handle(AddApplicantSkillCommand request, CancellationToken cancellationToken)
    {
        var skillResult = await _skillRepository.GetByIdAsync(request.SkillId);
        if (skillResult.HasNoValue)
            return Result.Failure(DomainErrors.Skill.NotFound);

        var applicantResult = await _applicantRepository.GetByIdAsync(request.ApplicantId);
        if (applicantResult.HasNoValue)
            return Result.Failure(DomainErrors.Applicant.NotFound);

        var skill = skillResult.Value;
        var applicant = applicantResult.Value;
        
        if (applicant.Skills.Any(s => s.Id == skill.Id))
            return Result.Failure(DomainErrors.Skill.AlreadyInSkillList);

        applicant.Skills.Add(skill);

        _applicantRepository.Update(applicant);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success(applicantResult);
    }
}
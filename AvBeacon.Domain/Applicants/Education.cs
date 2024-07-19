﻿using AvBeacon.Domain.Core.Primitives;
using AvBeacon.Domain.ValueObjects;

namespace AvBeacon.Domain.Applicants;

public sealed class Education : Entity
{
    private Education(EducationType educationType, Title title, Description description, Guid applicantId)
        : base(Guid.NewGuid())
    {
        EducationType = educationType;
        Title = title;
        Description = description;
        ApplicantId = applicantId;
    }

    public EducationType EducationType { get; init; }
    public Title Title { get; set; }
    public Description Description { get; set; }

    public Guid ApplicantId { get; init; }
    public Applicant Applicant { get; init; } = null!;

    public static Education Create(EducationType educationType, Title title, Description description, Guid applicantId)
    {
        return new Education(educationType, title, description, applicantId);
    }
}
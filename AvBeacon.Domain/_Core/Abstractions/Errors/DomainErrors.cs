﻿using AvBeacon.Domain._Core.Abstractions.Primitives;

namespace AvBeacon.Domain._Core.Abstractions.Errors;

public static class DomainErrors
{
    public static class User
    {
        public static Error NotFound => new("User.NotFound", "The user with the specified identifier was not found.");

        public static Error InvalidPermissions => new("User.InvalidPermissions",
                                                      "The current user does not have the permissions to perform that operation.");

        public static Error DuplicateEmail => new("User.DuplicateEmail", "The specified email is already in use.");

        public static Error CannotChangePassword => new("User.CannotChangePassword",
                                                        "The password cannot be changed to the specified password.");
    }

    public static class Applicant
    {
        public static Error NotFound =>
            new("Applicant.NotFound", "The applicant with the specified identifier was not found.");

        public static Error AlreadyProcessed =>
            new("Applicant.AlreadyProcessed", "The applicant has already been processed.");
    }

    public static class Recruiter
    {
        public static Error NotFound =>
            new("Recruiter.NotFound", "The recruiter with the specified identifier was not found.");
    }

    public static class Skill
    {
        public static Error NotFound => new("Skill.NotFound", "The skill with the specified identifier was not found.");
    }

    public static class Experience
    {
        public static Error NotFound =>
            new("Experience.NotFound", "The experience with the specified identifier was not found.");

        public static Error AlreadyExists => new("Experience.AlreadyExists",
                                                 "The specified experience already exists for this user.");
    }

    public static class Education
    {
        public static Error NotFound =>
            new("Education.NotFound", "The education with the specified identifier was not found.");

        public static Error AlreadyExists =>
            new("Education.AlreadyExists", "The specified education already exists for this user.");
    }

    public static class Notification
    {
        public static Error AlreadySent => new("Notification.AlreadySent", "The notification has already been sent.");
    }

    public static class JobApplication
    {
        public static Error NotFound => new("JobApplication.NotFound",
                                            "The job application with the specified identifier was not found.");

        public static Error UserNotFound => new("JobApplication.UserNotFound",
                                                "The user with the specified identifier was not found.");

        public static Error AlreadyProcessed =>
            new("JobApplication.AlreadyProcessed", "The job application has already been processed.");
    }

    public static class JobOffer
    {
        public static Error NotFound =>
            new("JobOffer.NotFound", "The job offer with the specified identifier was not found.");

        public static Error AlreadyCancelled =>
            new("JobOffer.AlreadyCancelled", "The job offer has already been cancelled.");
    }

    public static class Title
    {
        public static Error NullOrEmpty => new("Title.NullOrEmpty", "The title is required.");
        public static Error LongerThanAllowed => new("Title.LongerThanAllowed", "The title is longer than allowed.");
    }

    public static class Description
    {
        public static Error NullOrEmpty => new("Description.NullOrEmpty", "The description is required.");

        public static Error LongerThanAllowed =>
            new("Description.LongerThanAllowed", "The description is longer than allowed.");
    }

    public static class Name
    {
        public static Error NullOrEmpty => new("Name.NullOrEmpty", "The name is required.");
        public static Error LongerThanAllowed => new("Name.LongerThanAllowed", "The name is longer than allowed.");
    }

    public static class FirstName
    {
        public static Error NullOrEmpty => new("FirstName.NullOrEmpty", "The first name is required.");

        public static Error LongerThanAllowed =>
            new("FirstName.LongerThanAllowed", "The first name is longer than allowed.");
    }

    public static class LastName
    {
        public static Error NullOrEmpty => new("LastName.NullOrEmpty", "The last name is required.");

        public static Error LongerThanAllowed =>
            new("LastName.LongerThanAllowed", "The last name is longer than allowed.");
    }

    public static class Email
    {
        public static Error NullOrEmpty => new("Email.NullOrEmpty", "The email is required.");
        public static Error LongerThanAllowed => new("Email.LongerThanAllowed", "The email is longer than allowed.");
        public static Error InvalidFormat => new("Email.InvalidFormat", "The email format is invalid.");
    }

    public static class General
    {
        public static Error UnprocessableRequest =>
            new("General.UnprocessableRequest", "The server could not process the request.");

        public static Error ServerError => new("General.ServerError", "The server encountered an unrecoverable error.");
    }
}
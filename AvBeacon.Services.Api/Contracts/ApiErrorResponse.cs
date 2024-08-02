﻿using AvBeacon.Domain.Core.Primitives;

namespace AvBeacon.Services.Api.Contracts;

/// <summary>
///     Represents API an error response.
/// </summary>
public class ApiErrorResponse
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ApiErrorResponse" /> class.
    /// </summary>
    /// <param name="errors"> The enumerable collection of errors. </param>
    public ApiErrorResponse(IReadOnlyCollection<Error> errors)
    {
        Errors = errors;
    }

    /// <summary>
    ///     Gets the errors.
    /// </summary>
    public IReadOnlyCollection<Error> Errors { get; }
}
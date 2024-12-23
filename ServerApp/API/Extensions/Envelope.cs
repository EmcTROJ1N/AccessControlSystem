﻿using Domain.Utils;

namespace API.Extensions;

public record Envelope
{
    public object? Result { get; }
    public List<Error>? Errors { get; init; }
    public DateTime TimeCreated { get; }

    private Envelope(object? result, List<Error>? errors)
    {
        Result = result;
        Errors = errors;
        TimeCreated = DateTime.Now;
    }

    public static Envelope Ok(object? result = null) =>
        new(result, null);

    public static Envelope Error(List<Error>? errors) =>
        new(null, errors);
}
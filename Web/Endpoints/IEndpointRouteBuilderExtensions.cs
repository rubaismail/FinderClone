using System.Diagnostics.CodeAnalysis;
using Ardalis.GuardClauses;
using Web.Infastructure;

namespace Web.Endpoints;

public static class IEndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapGet(this IEndpointRouteBuilder builder, Delegate handler,
        [StringSyntax("Route")] string pattern = "")
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapGet(pattern, handler)
            .WithName(handler.Method.Name);

        return builder;
    }

    public static IEndpointRouteBuilder MapPost(this IEndpointRouteBuilder builder, Delegate handler,
        [StringSyntax("Route")] string pattern = "")
    {
        Guard.Against.AnonymousMethod(handler);

        builder.MapPost(pattern, handler)
            .WithName(handler.Method.Name);

        return builder;
    }
}
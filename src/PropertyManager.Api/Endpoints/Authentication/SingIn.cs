
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Api.Extensions;
using PropertyManager.Api.Infrastructure;
using PropertyManager.Application.UseCase.Authentications;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Api.Endpoints.Authentication
{
    public sealed record SingInRequest(
        string UserName,
        string Password);
    public sealed class SingIn : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"api/owners/{nameof(SingIn)}", async (SingInRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                SingInQuery command = request.Adapt<SingInQuery>();
                Result<string> result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
             .ConfigureSwagger();
        }
    }
}

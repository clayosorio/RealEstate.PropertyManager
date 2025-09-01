
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Api.Endpoints.Owners.Add;
using PropertyManager.Application.Commons.Results;
using PropertyManager.Application.UseCase.Authentications;

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
                return !string.IsNullOrEmpty(result.Value) ? Results.Ok(result) : Results.BadRequest(result);
            })
             .ConfigureSwagger();
        }
    }
}

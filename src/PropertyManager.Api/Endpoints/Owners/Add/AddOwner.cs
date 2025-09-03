
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Api.Extensions;
using PropertyManager.Api.Infrastructure;
using PropertyManager.Application.UseCase.Owners.AddOwner;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Api.Endpoints.Owners.Add
{
    public sealed record AddOwnerRequest(
        string Name,
        string Address,
        string Photo,
        DateTime Birthday,
        string UserName,
        string Email,
        string Password
    );
    public sealed class AddOwner : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"api/owners/{nameof(AddOwner)}", async (AddOwnerRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                AddOwnerCommand command = request.Adapt<AddOwnerCommand>();
                Result<int> result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Created, CustomResults.Problem);
            })
            .WithTags("Autenticación")
            .ConfigureSwagger();
        }
    }
}

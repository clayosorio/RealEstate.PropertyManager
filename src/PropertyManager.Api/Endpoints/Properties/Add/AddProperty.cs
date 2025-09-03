
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Api.Extensions;
using PropertyManager.Api.Infrastructure;
using PropertyManager.Application.UseCase.Properties.AddProperty;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Api.Endpoints.Properties.Add
{
    public sealed record AddPropertyRequest(
        string Name,
        string Address,
        decimal Price,
        int Year,
        int IdOwner);
    public sealed class AddProperty : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"api/properties/{nameof(AddProperty)}", async (AddPropertyRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                AddPropertyCommand command = request.Adapt<AddPropertyCommand>();

                Result<int> result = await sender.Send(command, cancellationToken);

                return result.Match(Results.Created, CustomResults.Problem);
            })
            .ConfigureSwagger();
        }
    }
}

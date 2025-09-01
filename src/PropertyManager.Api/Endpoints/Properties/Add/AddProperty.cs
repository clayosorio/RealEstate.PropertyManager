
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Application.Commons.Results;
using PropertyManager.Application.UseCase.Properties.AddProperty;

namespace PropertyManager.Api.Endpoints.Properties.Add
{
    public sealed record AddPropertyRequest(
        string Name,
        string Address,
        decimal Price,
        string CodeInternal,
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

                return result.Value is not 0 ? Results.Ok(result) : Results.BadRequest(result);
            })
            .ConfigureSwagger();
        }
    }
}

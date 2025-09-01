
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Api.Extensions;
using PropertyManager.Api.Infrastructure;
using PropertyManager.Application.UseCase.PropertyImages.AddPropertyImage;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Api.Endpoints.Properties.AddImagePropeties
{
    public sealed record AddImagePropertyRequest(
        int IdProperty,
        IFormFile Image);
    public sealed class AddImageProperty : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost($"api/properties/{nameof(AddImageProperty)}", async (AddImagePropertyRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                AddPropertyImageCommand command = request.Adapt<AddPropertyImageCommand>();
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Created, CustomResults.Problem);
            })
             .ConfigureSwagger();
        }
    }
}

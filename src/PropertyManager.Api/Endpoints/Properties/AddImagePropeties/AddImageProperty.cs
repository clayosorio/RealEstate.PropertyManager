
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Api.Extensions;
using PropertyManager.Api.Infrastructure;
using PropertyManager.Application.UseCase.PropertyImages.AddPropertyImage;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Api.Endpoints.Properties.AddImagePropeties
{
    //public sealed record AddImagePropertyRequest(
    //    IFormFileCollection Images);
    public sealed class AddImageProperty : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("api/properties/{IdProperty:int}/add-image", async (
                int IdProperty,
                IFormFileCollection images,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                AddPropertyImageCommand command = new(IdProperty, images.ToList());
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(
                    Results.Created,
                    CustomResults.Problem);
            })
            .DisableAntiforgery()
            .Accepts<IFormFileCollection>("multipart/form-data")
            .ConfigureSwagger();
        }
    }
}

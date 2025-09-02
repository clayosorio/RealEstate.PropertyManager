
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PropertyManager.Api.Extensions;
using PropertyManager.Api.Infrastructure;
using PropertyManager.Application.UseCase.Properties.UpdateProperty;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Api.Endpoints.Properties.UpdateProperties
{
    public sealed record UpdatePropertyRequest(int IdProperty, string Name, string Address, decimal Price, int CodeInternal, int Year);
    public class UpdateProperty : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"api/properties/{nameof(UpdateProperty)}", async (int idProperty, UpdatePropertyRequest request, [FromServices] ISender sender, CancellationToken cancellationToken) =>
            {
                UpdatePropertyCommand command = request.Adapt<UpdatePropertyCommand>();
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.Created, CustomResults.Problem);
            });
        }
    }
}

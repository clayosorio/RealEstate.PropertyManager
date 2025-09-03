
using MediatR;
using PropertyManager.Api.Endpoints.Authentication;
using PropertyManager.Api.Extensions;
using PropertyManager.Api.Infrastructure;
using PropertyManager.Application.UseCase.Properties.ChagePropertyPrice;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Api.Endpoints.Properties.ChangePrice
{
    public class ChangePriceProperty : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut($"/properties/{nameof(ChangePriceProperty)}", async (int idProperty, decimal newPrice, ISender sender, CancellationToken cancellationToken) =>
            {
                ChangePropertyPriceCommand command = new (idProperty, newPrice);
                Result result = await sender.Send(command, cancellationToken);
                return result.Match(Results.NoContent, CustomResults.Problem);
            })
            .RequireAuthorization()
            .ConfigureSwagger();
        }
    }
}

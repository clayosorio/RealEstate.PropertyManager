using MediatR;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Application.UseCase.Properties.ChagePropertyPrice
{
    public sealed record ChangePropertyPriceCommand(int IdProperty, decimal NewPrice) : IRequest<Result>;
}

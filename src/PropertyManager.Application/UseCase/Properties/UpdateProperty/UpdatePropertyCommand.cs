using MediatR;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Application.UseCase.Properties.UpdateProperty
{
    public sealed record UpdatePropertyCommand(int IdProperty, string Name, string Address, decimal Price, int CodeInternal, int Year) : IRequest<Result>;
}

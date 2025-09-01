using MediatR;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Application.UseCase.Properties.AddProperty
{
    public sealed record AddPropertyCommand(
        string Name,
        string Address,
        decimal Price,
        string CodeInternal,
        int Year,
        int IdOwner) : IRequest<Result<int>>;
}

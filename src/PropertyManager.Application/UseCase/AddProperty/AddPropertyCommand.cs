using MediatR;
using PropertyManager.Application.Commons.Results;

namespace PropertyManager.Application.UseCase.AddProperty
{
    public sealed record AddPropertyCommand(
        string Name,
        string Address,
        decimal Price,
        string CodeInternal,
        int Year,
        int IdOwner) : IRequest<Result<int>>;
}

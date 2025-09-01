using MediatR;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Application.UseCase.Owners.AddOwner
{
    public sealed record AddOwnerCommand(
        string Name,
        string Address,
        string Photo,
        DateTime Birthday,
        string UserName,
        string Email,
        string Password
    ) : IRequest<Result<int>>;
}

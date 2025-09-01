using MediatR;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Application.UseCase.Authentications
{
    public sealed record SingInQuery(string userName, string password) : IRequest<Result<string>>;
}

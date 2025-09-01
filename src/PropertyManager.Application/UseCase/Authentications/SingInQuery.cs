using MediatR;
using PropertyManager.Application.Commons.Results;

namespace PropertyManager.Application.UseCase.Authentications
{
    public sealed record SingInQuery(string userName, string password) : IRequest<Result<string>>;
}

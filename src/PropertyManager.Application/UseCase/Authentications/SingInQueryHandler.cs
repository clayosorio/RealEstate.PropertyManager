using MediatR;
using PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication;
using PropertyManager.Domain.Abstractions.Errors;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Owners.Errors;
using PropertyManager.Domain.Owners.Repositories;

namespace PropertyManager.Application.UseCase.Authentications
{
    public sealed class SingInQueryHandler(
        IOwnerRepository ownerRepository,
        IPasswordService passwordService,
        ITokenProvider tokenProvider
        ) : IRequestHandler<SingInQuery, Result<string>>
    {
        public async Task<Result<string>> Handle(SingInQuery request, CancellationToken cancellationToken)
        {
            Owner? owner = await ownerRepository.GetByUserNameAsync(request.userName, cancellationToken);

            if (owner == null)
            {
                return Result.Failure<string>(AuthenticationError.InvalidCredentials());
            }

            if (!passwordService.VerifyPasswordHash(request.password, owner.PasswordHash, owner.PasswordSalt))
            {
                return Result.Failure<string>(AuthenticationError.InvalidCredentials());
            }

            return tokenProvider.Create(owner);
        }
    }
}

using Mapster;
using MediatR;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication;
using PropertyManager.Application.Commons.Results;
using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.Owners.Repositories;

namespace PropertyManager.Application.UseCase.Owners.AddOwner
{
    public class AddOwnerCommandHandler(IOwnerRepository ownerRepository, IUnitOfWork unitOfWork, IPasswordService passwordService) : IRequestHandler<AddOwnerCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(AddOwnerCommand request, CancellationToken cancellationToken)
        {
            if (await ownerRepository.ExistsByUserNameAsync(request.UserName, cancellationToken))
            {
                return Result<int>.Failure("400","Username already exists.");
            }

            passwordService.CreatePasswordHash(request.Password, out var hash, out var salt);

            Owner owner = request.Adapt<Owner>();

            owner.PasswordHash = hash;
            owner.PasswordSalt = salt;

            await ownerRepository.AddAsync(owner, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<int>.Success(owner.IdOwner);
        }
    }
}

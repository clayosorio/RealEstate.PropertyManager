using Mapster;
using MediatR;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Domain.Abstractions.Errors;
using PropertyManager.Domain.Owners.Errors;
using PropertyManager.Domain.Owners.Repositories;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Errors;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Application.UseCase.Properties.AddProperty
{
    public class AddPropertyCommandHandler(
        IPropertyRepository repository,
        IUnitOfWork unitOfWork,
        IOwnerRepository ownerRepository
        ) : IRequestHandler<AddPropertyCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {
            if (request.Year > DateTime.UtcNow.Year)
            {
                return Result.Failure<int>(PropertyError.InvalidYear(request.Year));
            }

            if (request.Price <= 0)
            {
                return Result.Failure<int>(PropertyError.InvalidPrice(request.Price));
            }

            if (!await ownerRepository.ExistAsync(request.IdOwner, cancellationToken))
            {
                return Result.Failure<int>(OwnerError.OwnerNotFound(request.IdOwner));
            }

            Property property = request.Adapt<Property>();

            property.CodeInternal = Guid.NewGuid().ToString();
            property.CreatedAt = DateTime.UtcNow;

            await repository.AddAsync(property, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return property.IdProperty;
        }
    }
}

using MediatR;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Domain.Abstractions.Errors;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Errors;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Application.UseCase.Properties.UpdateProperty
{
    public sealed class UpdatePropertyCommandHandler(
        IPropertyRepository propertyRepository, 
        IUnitOfWork unitOfWork) 
        : IRequestHandler<UpdatePropertyCommand, Result>
    {
        public async Task<Result> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            Property? property = await propertyRepository.GetByIdAsync(request.IdProperty);

            if (property is null)
            {
                return Result.Failure(PropertyError.PropertyNotFound(nameof(Property), request.IdProperty));
            }

            property.Name = request.Name;
            property.Address = request.Address;
            property.UpdatedAt = DateTime.UtcNow;
             
            propertyRepository.Update(property);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}

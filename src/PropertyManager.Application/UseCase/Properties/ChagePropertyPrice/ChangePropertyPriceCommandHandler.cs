using MediatR;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Domain.Abstractions.Errors;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Errors;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Application.UseCase.Properties.ChagePropertyPrice
{
    public class ChangePropertyPriceCommandHandler(IPropertyRepository propertyRepository, IUnitOfWork unitOfWork) : IRequestHandler<ChangePropertyPriceCommand, Result>
    {
        public async Task<Result> Handle(ChangePropertyPriceCommand request, CancellationToken cancellationToken)
        {
            Property? property = await propertyRepository.GetByIdAsync(request.IdProperty);

            if (property is null)
            {
                return Result.Failure(PropertyError.PropertyAlreadyExists(nameof(Property), request.IdProperty));
            }

            property.Price = request.NewPrice;
            propertyRepository.Update(property);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<object>.Success();
        }
    }
}

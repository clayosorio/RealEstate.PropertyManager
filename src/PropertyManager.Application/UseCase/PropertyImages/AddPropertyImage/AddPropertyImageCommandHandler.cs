using MediatR;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Domain.Abstractions.Errors;
using PropertyManager.Domain.Abstractions.Repositories;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Errors;
using PropertyManager.Domain.Properties.Repositories;
using PropertyManager.Domain.PropertyImages.Entities;
using PropertyManager.Domain.PropertyImages.Repositories;

namespace PropertyManager.Application.UseCase.PropertyImages.AddPropertyImage
{
    public class AddPropertyImageCommandHandler(
        IBlobStorageRepository blobStorageRepository, 
        IPropertyImageRepository propertyImageRepository, 
        IPropertyRepository propertyRepository,
        IUnitOfWork unitOfWork) 
        : IRequestHandler<AddPropertyImageCommand, Result>
    {
        public async Task<Result> Handle(AddPropertyImageCommand request, CancellationToken cancellationToken)
        {
            Property? property = await propertyRepository.GetByIdAsync(request.IdPropery, cancellationToken);

            if (property is null)
            {
                return Result.Failure(PropertyError.PropertyNotFound(nameof(Property), request.IdPropery));
            }

            var blobName = $"{request.IdPropery}/{Guid.NewGuid()}{Path.GetExtension(request.Image.FileName)}";

            await using var stream = request.Image.OpenReadStream();
            string imageUrl = await blobStorageRepository.UploadAsync(stream, blobName, request.Image.ContentType, cancellationToken);

            PropertyImage propertyImage = new()
            {
                IdProperty = request.IdPropery,
                ImageUrl = imageUrl,
                Enabled = true,
                Property = property
            };

            await propertyImageRepository.AddAsync(propertyImage, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}

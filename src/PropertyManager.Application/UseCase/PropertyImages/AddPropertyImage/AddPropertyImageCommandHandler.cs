using MediatR;
using Microsoft.AspNetCore.Http;
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
            Property? property = await propertyRepository.GetByIdAsync(request.IdProperty, cancellationToken);

            if (property is null)
            {
                return Result.Failure(PropertyError.PropertyNotFound(nameof(Property), request.IdProperty));
            }

            foreach (var image in request.Images)
            {
                if (image == null || image.Length == 0)
                    continue;

                string blobName = $"{request.IdProperty}/{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";

                await using var stream = image.OpenReadStream();
                string imageUrl = await blobStorageRepository.UploadAsync(
                    stream,
                    blobName,
                    image.ContentType,
                    cancellationToken
                );


                PropertyImage propertyImage = new()
                {
                    IdProperty = request.IdProperty,
                    ImageUrl = imageUrl,
                    Enabled = true,
                    CreatedAt = DateTime.UtcNow,
                    Property = property
                };
                

                await propertyImageRepository.AddAsync(propertyImage, cancellationToken);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}

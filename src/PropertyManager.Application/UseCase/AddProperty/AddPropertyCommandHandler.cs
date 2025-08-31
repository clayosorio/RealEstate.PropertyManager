using Mapster;
using MediatR;
using PropertyManager.Application.Abstractions.Insfraestructure.Persistence;
using PropertyManager.Application.Commons.Results;
using PropertyManager.Domain.Properties.Entities;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Application.UseCase.AddProperty
{
    public class AddPropertyCommandHandler(IPropertyRepository repository, IUnitOfWork unitOfWork) : IRequestHandler<AddPropertyCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(AddPropertyCommand request, CancellationToken cancellationToken)
        {
            Property property = request.Adapt<Property>();
            await repository.AddAsync(property, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Result<int>.Success(property.IdProperty));
        }
    }
}

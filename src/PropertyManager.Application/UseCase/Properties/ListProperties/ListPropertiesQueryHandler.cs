using MediatR;
using PropertyManager.Application.Common.Pagination;
using PropertyManager.Domain.Abstractions.Errors;
using PropertyManager.Domain.Properties.Queries.Output;
using PropertyManager.Domain.Properties.Repositories;

namespace PropertyManager.Application.UseCase.Properties.ListProperties
{
    public sealed class ListPropertiesQueryHandler(IPropertyRepository propertyRepository) : IRequestHandler<ListPropertiesQuery, Result<PagedResult<PropertyOutputDto>>>
    {
        public async Task<Result<PagedResult<PropertyOutputDto>>> Handle(ListPropertiesQuery request, CancellationToken cancellationToken)
        {
            List<PropertyOutputDto> properties = await propertyRepository.GetPropertiesAsync(
                request.Name,
                request.MinPrice,
                request.MaxPrice,
                request.Page,
                request.PageSize,
                cancellationToken
            );

            int totalCount = await propertyRepository.GetPropertiesCountAsync(
                request.Name,
                request.MinPrice,
                request.MaxPrice,
                cancellationToken
            );

            PagedResult<PropertyOutputDto> pagedResult = new()
            {
                Items = properties,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            };

            return pagedResult;
        }
    }
}

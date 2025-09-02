using MediatR;
using PropertyManager.Application.Common.Pagination;
using PropertyManager.Domain.Abstractions.Errors;
using PropertyManager.Domain.Properties.Queries.Output;

namespace PropertyManager.Application.UseCase.Properties.ListProperties
{
    public sealed record ListPropertiesQuery(
        string? Name,
        decimal? MinPrice,
        decimal? MaxPrice,
        int Page = 1,
        int PageSize = 10
    ) : IRequest<Result<PagedResult<PropertyOutputDto>>>;

}

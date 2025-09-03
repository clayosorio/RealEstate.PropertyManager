
using Mapster;
using MediatR;
using PropertyManager.Api.Endpoints.Authentication;
using PropertyManager.Api.Extensions;
using PropertyManager.Api.Infrastructure;
using PropertyManager.Application.Common.Pagination;
using PropertyManager.Application.UseCase.Properties.ListProperties;
using PropertyManager.Domain.Abstractions.Errors;
using PropertyManager.Domain.Properties.Queries.Output;

namespace PropertyManager.Api.Endpoints.Properties.ListProperties
{
    public sealed record ListPropertiesQueryRequest(
       string? Name,
       decimal? MinPrice,
       decimal? MaxPrice,
       int Page = 1,
       int PageSize = 10
    );
    public class ListPropertiesWithFilters : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet($"/properties/{nameof(ListPropertiesWithFilters)}", async (ISender sender, [AsParameters] ListPropertiesQueryRequest request, CancellationToken cancellationToken = default) =>
            {
                ListPropertiesQuery listPropertiesQuery = request.Adapt<ListPropertiesQuery>();
                Result<PagedResult<PropertyOutputDto>> result = await sender.Send(listPropertiesQuery, cancellationToken);
                return result.Match(Results.Ok, CustomResults.Problem);
            })
            .RequireAuthorization()
            .WithTags("Properties")
            .ConfigureSwagger();
        }
    }
}

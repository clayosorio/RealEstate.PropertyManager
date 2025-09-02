using MediatR;
using Microsoft.AspNetCore.Http;
using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Application.UseCase.PropertyImages.AddPropertyImage
{
    public sealed record AddPropertyImageCommand(int IdProperty, List<IFormFile> Images) : IRequest<Result>;
}

using MediatR;
using Microsoft.Extensions.Logging;
using PropertyManager.Domain.Common.Errors;

namespace PropertyManager.Application.Commons.Behaviors
{
    public class ErrorHandlingBehavior<TRequest, TResponse>(ILogger<ErrorHandlingBehavior<TRequest, TResponse>> logger) 
        : IPipelineBehavior<TRequest, TResponse>
            where TRequest : notnull
    {
        private readonly ILogger<ErrorHandlingBehavior<TRequest, TResponse>> _logger = logger;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (DomainError ex)
            {
                _logger.LogError(ex, "Domain error: {Message}", ex.Message);
                throw; // será capturado por el middleware global
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error");
                throw new ConflictError("An unexpected error occurred.");
            }
        }
    }
}

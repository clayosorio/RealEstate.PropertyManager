namespace PropertyManager.Domain.Common.Errors
{
    /// <summary>
    /// Base class for all domain errors.
    /// </summary>
    public abstract class DomainError(string code, string message, int statusCode = 400) : Exception(message)
    {
        public string Code { get; } = code;
        public int StatusCode { get; } = statusCode;
    }

    /// <summary>
    /// Error when an entity is not found.
    /// </summary>
    public class NotFoundError(string entity, object key) : DomainError($"{entity}.NotFound", $"{entity} with key '{key}' was not found.", 404)
    {
    }

    /// <summary>
    /// Error when a validation fails.
    /// </summary>
    public class ValidationError(Dictionary<string, string[]> failures) : DomainError("Validation.Failed", "One or more validation errors occurred.", 400)
    {
        public IReadOnlyDictionary<string, string[]> Failures { get; } = failures;
    }

    /// <summary>
    /// Error when access is forbidden due to lack of permissions.
    /// </summary>
    public class ForbiddenError(string action) : DomainError("Access.Forbidden", $"You do not have permission to perform '{action}'.", 403)
    {
    }

    /// <summary>
    /// Error when authentication fails.
    /// </summary>
    public class UnauthorizedError : DomainError
    {
        public UnauthorizedError()
            : base("Auth.Unauthorized", "You are not authorized to access this resource.", 401)
        {
        }
    }

    /// <summary>
    /// Error when a conflict occurs (e.g., duplicate record).
    /// </summary>
    public class ConflictError(string message) : DomainError("Conflict.Error", message, 409)
    {
    }
}

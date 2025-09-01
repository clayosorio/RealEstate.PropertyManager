namespace PropertyManager.Domain.Abstractions.Errors
{
    public record Error(string Code, string Description, ErrorType Type)
    {
        public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static readonly Error NullValue = new(
        "General.Null",
        "Null value was provided",
        ErrorType.Failure);

        public static Error Failure(string code, string description, int statusCode = 400) =>
            new(code, description, ErrorType.Failure);

        public static Error Validation(Dictionary<string, string[]> failures, int statusCode = 400) =>
            new(
                "Validation.Failed",
                string.Join("; ", failures.Select(f => $"{f.Key}: {string.Join(", ", f.Value)}")),
                ErrorType.Validation);

        public static Error NotFound(string entity, object key, int statusCode = 404) =>
            new($"{entity}.NotFound", $"{entity} with key '{key}' was not found.", ErrorType.NotFound);

        public static Error Problem(string code, string description) =>
            new(code, description, ErrorType.Problem);

        public static Error Unauthorized(int statusCode = 401) =>
            new("Auth.Unauthorized", "You are not authorized to access this resource.", ErrorType.Unauthorized);

        public static Error Conflict(string description, int statusCode = 409) =>
            new("Conflict.Error", description, ErrorType.Conflict);
    }
}

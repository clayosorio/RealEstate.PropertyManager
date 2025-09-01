using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Domain.Owners.Errors
{
    public static class AuthenticationError
    {
        public static Error InvalidCredentials() => Error.Unauthorized();
    }
}

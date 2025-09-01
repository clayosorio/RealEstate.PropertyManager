using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Domain.Properties.Errors
{
    public static class PropertyError
    {
        public static Error PropertyAlreadyExists(string entity, object key) => Error.NotFound(entity, key);
    }
}

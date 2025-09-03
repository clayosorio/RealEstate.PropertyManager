using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Domain.Owners.Errors
{
    public static class OwnerError
    {
        public static Error OwnerAlreadyExists(string entity, object key) => Error.NotFound(entity, key);
        public static Error OwnerNotFound(int idOwner) => Error.NotFound("Owner", idOwner);
    }
}

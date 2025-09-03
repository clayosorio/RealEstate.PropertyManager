using PropertyManager.Domain.Abstractions.Errors;

namespace PropertyManager.Domain.Properties.Errors
{
    public static class PropertyError
    {
        /// <summary>
        /// Retorna error cuando la propiedad ya existe (conflicto).
        /// </summary>
        public static Error PropertyAlreadyExists(string entity, object key) =>
            Error.Conflict($"La propiedad '{entity}' con clave '{key}' ya existe.");

        /// <summary>
        /// Retorna error cuando la propiedad no se encuentra.
        /// </summary>
        public static Error PropertyNotFound(string entity, object key) =>
            Error.NotFound(entity, key);

        /// <summary>
        /// Retorna error de validación para campos obligatorios o inválidos.
        /// </summary>
        public static Error InvalidFields(Dictionary<string, string[]> failures) =>
            Error.Validation(failures);

        /// <summary>
        /// Retorna error cuando el año de la propiedad es inválido.
        /// </summary>
        public static Error InvalidYear(int year) =>
            Error.Validation(new Dictionary<string, string[]>
            {
                { "Year", new[] { $"El año '{year}' no es válido." } }
            });

        /// <summary>
        /// Retorna error cuando el precio de la propiedad es inválido.
        /// </summary>
        public static Error InvalidPrice(decimal price) =>
            Error.Validation(new Dictionary<string, string[]>
            {
                { "Price", new[] { $"El precio '{price}' no es válido." } }
            });
    }
}

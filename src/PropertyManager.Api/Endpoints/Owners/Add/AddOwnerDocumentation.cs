using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManager.Api.Endpoints.Owners.Add
{
    public static class AddOwnerDocumentation
    {
        public static void ConfigureSwagger(this RouteHandlerBuilder builder)
        {
            builder.WithMetadata(new SwaggerOperationAttribute(
                     summary: "Agregar nuevo propietario al sistema",
                     description: "Permite al usuario asignarle imagenes a una propiedad"
                 ))
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Agregar nuevo propietario al sistema";
                     operation.Description = "Permite al usuario asignarle imagenes a una propiedad";
                     return operation;
                 });
        }
    }
}

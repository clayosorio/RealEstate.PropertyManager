using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManager.Api.Endpoints.Properties.AddImagePropeties
{
    public static class AddImagePropertyDocumentation
    {
        public static void ConfigureSwagger(this RouteHandlerBuilder builder)
        {
            builder.WithMetadata(new SwaggerOperationAttribute(
                     summary: "Asignar imagenes a propiedad",
                     description: "Permite al usuario asignarle imagenes a una propiedad"
                 ))
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Asignar imagenes a propiedad";
                     operation.Description = "Permite al usuario asignarle imagenes a una propiedad";
                     return operation;
                 });
        }
    }
}

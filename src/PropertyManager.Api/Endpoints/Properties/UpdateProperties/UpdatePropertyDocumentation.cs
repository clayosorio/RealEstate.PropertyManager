using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManager.Api.Endpoints.Properties.UpdateProperties
{
    public static class UpdatePropertyDocumentation
    {
        public static void ConfigureSwagger(this RouteHandlerBuilder builder)
        {
            builder.WithMetadata(new SwaggerOperationAttribute(
                     summary: "Actualizar Propiedad",
                     description: "Permite al cliente actualizar la información de una propiedad"
                 ))
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Actualizar Propiedad";
                     operation.Description = "Permite al cliente actualizar la información de una propiedad";
                     return operation;
                 });
        }
    }
}

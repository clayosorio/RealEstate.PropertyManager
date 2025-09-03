using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManager.Api.Endpoints.Properties.Add
{
    public static class AddPropertyDocumentation
    {
        public static void ConfigureSwagger(this RouteHandlerBuilder builder)
        {
            builder.WithMetadata(new SwaggerOperationAttribute(
                     summary: "Agregar propiedad",
                     description: "Permite a un usuario crear una propiedad en el sistema."
            ))
            .WithOpenApi(operation =>
            {
                operation.Summary = "Agregar propiedad";
                operation.Description = "Permite a un usuario crear una propiedad en el sistema.";
                return operation;
            });
        }
    }
}

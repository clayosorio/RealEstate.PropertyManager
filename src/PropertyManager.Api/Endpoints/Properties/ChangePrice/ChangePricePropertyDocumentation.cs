using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManager.Api.Endpoints.Properties.ChangePrice
{
    public static class ChangePricePropertyDocumentation
    {
        public static void ConfigureSwagger(this RouteHandlerBuilder builder)
        {
            builder.WithMetadata(new SwaggerOperationAttribute(
                     summary: "Modificar precio de una propiedad",
                     description: "Permite al usuario modificar el precio de una propiedad"
            ))
            .WithOpenApi(operation =>
            {
                operation.Summary = "Modificar precio de una propiedad";
                operation.Description = "Permite al usuario modificar el precio de una propiedad.";
                return operation;
            });
        }
    }
}

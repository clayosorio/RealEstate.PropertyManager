using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManager.Api.Endpoints.Properties.ListProperties
{
    public static class ListPropertiesWithFiltersDocumentarion
    {
        public static void ConfigureSwagger(this RouteHandlerBuilder builder)
        {
            builder.WithMetadata(new SwaggerOperationAttribute(
                     summary: "Permite listar propiedades",
                     description: "Permite al cliente listar propiedades con diferentes filtros opcionales"
                 ))
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Permite listar propiedades";
                     operation.Description = "Permite al cliente listar propiedades con diferentes filtros opcionales";
                     return operation;
                 });
        }
    }
}

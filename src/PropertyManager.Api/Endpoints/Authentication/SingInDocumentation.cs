using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManager.Api.Endpoints.Authentication
{
    public static class SingInDocumentation
    {
        public static void ConfigureSwagger(this RouteHandlerBuilder builder)
        {
            builder.WithMetadata(new SwaggerOperationAttribute(
                     summary: "Autenticación de un propietario",
                     description: "Permite a un usuario iniciar sesión con su correo electrónico y contraseña. Devuelve un token JWT si la autenticación es exitosa."
                 ))
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Autenticación de un propietario";
                     operation.Description = "Permite a un usuario iniciar sesión con su correo electrónico y contraseña. Devuelve un token JWT si la autenticación es exitosa.";
                     return operation;
                 });
        }
    }
}

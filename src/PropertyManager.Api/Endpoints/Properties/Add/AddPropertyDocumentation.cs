using Swashbuckle.AspNetCore.Annotations;

namespace PropertyManager.Api.Endpoints.Properties.Add
{
    public static class AddOwnerDocumentation
    {
        public static void ConfigureSwagger(this RouteHandlerBuilder builder)
        {
            builder.WithMetadata(new SwaggerOperationAttribute(
                     summary: "Autenticar usuario",
                     description: "Permite a un usuario iniciar sesión con su correo electrónico y contraseña. Devuelve un token JWT si la autenticación es exitosa."
                 ))
                 .WithOpenApi(operation =>
                 {
                     operation.Summary = "Permite a un usuario iniciar sesión con su correo electrónico y contraseña. Devuelve un token JWT si la autenticación es exitosa.";
                     operation.Description = "Autenticación de usuario";
                     return operation;
                 });
        }
    }
}

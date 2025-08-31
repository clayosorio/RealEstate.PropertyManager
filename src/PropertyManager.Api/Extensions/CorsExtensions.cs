using PropertyManager.Api.Commons;

namespace PropertyManager.Api.Extensions
{
    public static class CorsExtensions
    {
        private const string CorsPolicyName = "CustomCorsPolicy";

        /// <summary>
        /// Configura CORS a partir de la configuración en appsettings.json.
        /// </summary>
        public static IServiceCollection AddCorsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            CorsOptionsConfig? corsSettings = configuration.GetSection("CorsSettings").Get<CorsOptionsConfig>();

            if (corsSettings != null && corsSettings.EnableCors)
            {
                services.AddCors(options => options.AddPolicy(CorsPolicyName, builder =>
                {
                    if (corsSettings.AllowAllOrigins)
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    }
                    else
                    {
                        builder.WithOrigins(corsSettings.AllowedOrigins ?? [])
                               .WithMethods(corsSettings.AllowedMethods ?? [])
                               .WithHeaders(corsSettings.AllowedHeaders ?? []);

                        if (corsSettings.AllowCredentials)
                        {
                            builder.AllowCredentials();
                        }
                    }
                }));
            }

            return services;
        }

        /// <summary>
        /// Aplica la configuración de CORS a la aplicación.
        /// </summary>
        public static IApplicationBuilder UseCorsConfiguration(this IApplicationBuilder app, IConfiguration configuration)
        {
            CorsOptionsConfig? corsSettings = configuration.GetSection("CorsSettings").Get<CorsOptionsConfig>();

            if (corsSettings != null && corsSettings.EnableCors)
            {
                app.UseCors(CorsPolicyName);
            }

            return app;
        }
    }
}

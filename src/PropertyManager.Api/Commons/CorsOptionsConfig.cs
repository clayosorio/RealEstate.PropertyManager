namespace PropertyManager.Api.Commons
{
    public class CorsOptionsConfig
    {
        public bool EnableCors { get; set; } = true;
        public bool AllowAllOrigins { get; set; }
        public bool AllowCredentials { get; set; }
        public string[]? AllowedOrigins { get; set; }
        public string[]? AllowedMethods { get; set; }
        public string[]? AllowedHeaders { get; set; }
    }
}

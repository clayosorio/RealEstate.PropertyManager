namespace PropertyManager.Infrastructure.Implementations.Configurations
{
    public class BlobOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string ContainerName { get; set; } = string.Empty;
    }
}

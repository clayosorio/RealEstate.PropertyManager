namespace PropertyManager.Domain.Abstractions.Repositories
{
    public interface IBlobStorageRepository
    {
        Task<string> UploadAsync(Stream fileStream, string blobName, string contentType, CancellationToken cancellationToken = default);
        Task<Stream?> DownloadAsync(string blobName, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string blobName, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> ListAsync(CancellationToken cancellationToken = default);
    }
}

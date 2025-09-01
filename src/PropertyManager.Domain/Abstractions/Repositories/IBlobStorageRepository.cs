﻿namespace PropertyManager.Domain.Abstractions.Repositories
{
    public interface IBlobStorageRepository
    {
        Task<string> UploadAsync(Stream fileStream, string containerName, string blobName, string contentType, CancellationToken cancellationToken = default);
        Task<Stream?> DownloadAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken = default);
        Task<IEnumerable<string>> ListAsync(string containerName, CancellationToken cancellationToken = default);
    }
}

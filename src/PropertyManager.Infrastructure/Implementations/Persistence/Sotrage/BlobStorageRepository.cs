using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using PropertyManager.Domain.Common.Sotrage.Repositories;

namespace PropertyManager.Infrastructure.Implementations.Persistence.Sotrage
{
    public class BlobStorageRepository : IBlobStorageRepository
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobStorageRepository(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadAsync(Stream fileStream, string containerName, string blobName, string contentType, CancellationToken cancellationToken = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

            var blobClient = container.GetBlobClient(blobName);

            var headers = new BlobHttpHeaders { ContentType = contentType };

            await blobClient.UploadAsync(fileStream, headers, cancellationToken: cancellationToken);

            return blobClient.Uri.ToString();
        }

        public async Task<Stream?> DownloadAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = container.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync(cancellationToken)) return null;

            var response = await blobClient.DownloadAsync(cancellationToken);
            return response.Value.Content;
        }

        public async Task<bool> DeleteAsync(string containerName, string blobName, CancellationToken cancellationToken = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);
            var blobClient = container.GetBlobClient(blobName);

            return await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<string>> ListAsync(string containerName, CancellationToken cancellationToken = default)
        {
            var container = _blobServiceClient.GetBlobContainerClient(containerName);

            var blobs = new List<string>();
            await foreach (var blobItem in container.GetBlobsAsync(cancellationToken: cancellationToken))
            {
                blobs.Add(blobItem.Name);
            }

            return blobs;
        }
    }
}

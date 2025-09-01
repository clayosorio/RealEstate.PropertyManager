using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using PropertyManager.Domain.Abstractions.Repositories;
using PropertyManager.Infrastructure.Implementations.Configurations;

namespace PropertyManager.Infrastructure.Implementations.Persistence.Sotrage
{
    public class BlobStorageRepository(BlobServiceClient blobServiceClient, IOptions<BlobOptions> options) : IBlobStorageRepository
    {
        private readonly BlobOptions _options = options.Value;

        public async Task<string> UploadAsync(Stream fileStream, string blobName, string contentType, CancellationToken cancellationToken = default)
        {
            var container = blobServiceClient.GetBlobContainerClient(_options.ContainerName);
            await container.CreateIfNotExistsAsync(PublicAccessType.Blob, cancellationToken: cancellationToken);

            var blobClient = container.GetBlobClient(blobName);

            var headers = new BlobHttpHeaders { ContentType = contentType };

            await blobClient.UploadAsync(fileStream, headers, cancellationToken: cancellationToken);

            return blobClient.Uri.ToString();
        }

        public async Task<Stream?> DownloadAsync(string blobName, CancellationToken cancellationToken = default)
        {
            var container = blobServiceClient.GetBlobContainerClient(_options.ContainerName);
            var blobClient = container.GetBlobClient(blobName);

            if (!await blobClient.ExistsAsync(cancellationToken)) return null;

            var response = await blobClient.DownloadAsync(cancellationToken);
            return response.Value.Content;
        }

        public async Task<bool> DeleteAsync(string blobName, CancellationToken cancellationToken = default)
        {
            var container = blobServiceClient.GetBlobContainerClient(_options.ContainerName);
            var blobClient = container.GetBlobClient(blobName);

            return await blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
        }

        public async Task<IEnumerable<string>> ListAsync(CancellationToken cancellationToken = default)
        {
            var container = blobServiceClient.GetBlobContainerClient(_options.ContainerName);

            var blobs = new List<string>();
            await foreach (var blobItem in container.GetBlobsAsync(cancellationToken: cancellationToken))
            {
                blobs.Add(blobItem.Name);
            }

            return blobs;
        }
    }
}

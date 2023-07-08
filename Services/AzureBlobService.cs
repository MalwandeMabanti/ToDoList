using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class AzureBlobService : IAzureBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public AzureBlobService(IOptions<BlobServiceSettings> settings)
        {
            _blobServiceClient = new BlobServiceClient(settings.Value.ConnectionString);
        }

        public async Task<string> UploadFileAsync(string containerName, IFormFile file, string fileName)
        {
            BlobContainerClient containerClient;

            // Check if the container already exists
            if (await _blobServiceClient.GetBlobContainerClient(containerName).ExistsAsync())
            {
                containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            }
            else
            {
                // Create the container if it does not exist
                containerClient = await _blobServiceClient.CreateBlobContainerAsync(containerName, PublicAccessType.BlobContainer);
            }


            // Get a reference to a blob
            BlobClient blobClient = containerClient.GetBlobClient(fileName);

            // Open the file and upload its data
            using var uploadFileStream = file.OpenReadStream();

            var httpHeaders = new BlobHttpHeaders()
            {
                ContentDisposition = "inline; filename=\"" + fileName + "\"",
                ContentType = "image/png"
                //ContentType = file.ContentType
            };

            await blobClient.UploadAsync(uploadFileStream, httpHeaders);
            uploadFileStream.Close();

            // Return the URI of the uploaded blob
            return blobClient.Uri.AbsoluteUri;
        }
    }
}

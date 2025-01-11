using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using CEG_BAL.Configurations;
using CEG_BAL.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Implements
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly AzureStorageConfig _config;

        public AzureStorageService(IOptions<AzureStorageConfig> config)
        {
            _config = config.Value;
        }

        public BlobContainerClient GetBlobContainerClient()
        {
            // Ensure the configuration values are not null
            if (string.IsNullOrWhiteSpace(_config.BlobConnectionString) || string.IsNullOrWhiteSpace(_config.BlobContainerName))
            {
                throw new InvalidOperationException("Azure Blob Storage configuration is missing or invalid.");
            }

            // Create the BlobContainerClient
            var serviceClient = new BlobServiceClient(_config.BlobConnectionString);
            var containerClient = serviceClient.GetBlobContainerClient(_config.BlobContainerName);

            return containerClient;
        }

        public async Task<string> UploadToBlobAsync(IFormFile file, string containerName)
        {
            try
            {
                var containerClient = GetBlobContainerClient();

                var azureResponse = new List<BlobContentInfo>();

                // Generate a unique name for the file
                var uniqueFileName = containerName + $"{Guid.NewGuid()}_{file.FileName}";

                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await containerClient.UploadBlobAsync(uniqueFileName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = _config.BlobDefaultFolderURL + uniqueFileName;
                return image;
            }

            catch (Exception)
            {
                throw new Exception("An error occurred while uploading the file to Blob Storage.");
            }
        }
    }
}

using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Services.Interfaces
{
    public interface IAzureStorageService
    {
        BlobContainerClient GetBlobContainerClient();
        Task<string> UploadToBlobAsync(IFormFile file, string containerName);
    }
}

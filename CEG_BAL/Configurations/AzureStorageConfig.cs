using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEG_BAL.Configurations
{
    public class AzureStorageConfig
    {
        public string? BlobConnectionString { get; set; }
        public string? BlobDefaultFolderURL { get; set; }
        public string? BlobContainerName { get; set; }
        public string? BlobExampleContainerNamePath { get; set; }
    }
}

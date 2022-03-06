using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Intuit.VideoApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intuit.VideoApp.Data
{
    internal class CloudFileDataProvider : IFileDataProvider
    {
        public async Task<IEnumerable<FileModel>> GetFilesAsync(string _connectionString)
        {
            BlobServiceClient blobServiceClient = new BlobServiceClient(_connectionString);
            var fileModelList = new List<FileModel>();
            Console.WriteLine("3. Listing containers and blobs "
                + $"of '{blobServiceClient.AccountName}' account");

            await foreach (BlobContainerItem blobContainerItem in blobServiceClient.GetBlobContainersAsync())
            {
                BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(blobContainerItem.Name);
                
                await foreach (BlobItem blobItem in blobContainerClient.GetBlobsAsync())
                {
                    fileModelList.Add(new FileModel(blobItem.Name
                                                    , (int)blobItem.Properties.ContentLength / 1000000
                                                    , blobContainerClient.GetBlobClient(blobItem.Name).Uri.ToString()
                                                    , true));
                }
            }
            return fileModelList;
        }
    }
}

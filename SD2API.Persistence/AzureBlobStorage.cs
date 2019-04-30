using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using SD2API.Application.Interfaces;

namespace SD2API.Persistence
{
    public class AzureBlobStorage : IBlobStorage
    {
        private CloudStorageAccount _cloudStorageAccount;

        public AzureBlobStorage(IConfiguration configuration)
        {
            var blobCs = configuration.GetConnectionString("ReplayBlobStorage");
            _cloudStorageAccount = CloudStorageAccount.Parse(blobCs);
        }

        public async Task DeleteBlobIfExists(string containerName, string blobFileName)
        {
            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            var blob = container.GetBlobReference(blobFileName);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> UploadBlob(Stream stream, string containerName, string blobFileName, bool compress)
        {
            Stream streamToUpload = stream;
            stream.Seek(0, SeekOrigin.Begin);

            var fileName = blobFileName;

            MemoryStream memoryStream;
            if (compress)
            {
                var zipFilename = Path.GetFileNameWithoutExtension(blobFileName) + ".zip";
                memoryStream = new MemoryStream();
                var zip = new ZipArchive(memoryStream, ZipArchiveMode.Create, false);
                var entry = zip.CreateEntry(blobFileName);
                using (var zipEntryStream = entry.Open())
                {
                    await stream.CopyToAsync(zipEntryStream);
                }

                await memoryStream.FlushAsync();
                memoryStream.Seek(0, SeekOrigin.Begin);
                streamToUpload = memoryStream;
                fileName = zipFilename;
            }

            var blobClient = _cloudStorageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, new BlobRequestOptions(),
                new OperationContext());

            CloudBlockBlob blob = container.GetBlockBlobReference(fileName);
            if(blob.Exists())
                throw new ArgumentException($"Blob '{fileName}' already exists in container '{containerName}'");

            await blob.UploadFromStreamAsync(streamToUpload);

            if (compress)
            {
                streamToUpload.Close();
                streamToUpload.Dispose();
            }

            return blob.Uri.ToString(); // Todo update to use custom domain name
        }
    }
}

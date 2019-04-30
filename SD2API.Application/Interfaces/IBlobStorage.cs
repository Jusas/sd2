using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SD2API.Application.Interfaces
{
    public interface IBlobStorage
    {
        Task<string> UploadBlob(Stream stream, string containerName, string blobFileName, bool compress);
        Task DeleteBlobIfExists(string containerName, string blobFileName);
    }
}

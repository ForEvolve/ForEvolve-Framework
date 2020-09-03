using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ForEvolve.Azure.Storage.Blob
{
    public interface IFileRepository
    {
        Task<Stream> OpenReadAsync(string fileName);
        Task<string> UploadFileAsync(byte[] file, string fileName);
        Task<string> UploadFileAsync(Stream fileStream, string fileName);
        Task<bool> RemoveFileAsync(string fileName);

        [Obsolete(@"This method will be moved to an extension into another project in a future major release.
Doing that will allow removing the dependency on Microsoft.AspNetCore.Http.Features", error: false)]
        Task<string> UploadFileAsync(IFormFile file, string fileName);
    }
}
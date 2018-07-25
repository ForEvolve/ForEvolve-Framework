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
        Task<string> UploadFileAsync(IFormFile file, string fileName);
        Task<bool> RemoveFileAsync(string fileName);
    }
}
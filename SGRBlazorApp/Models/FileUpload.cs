using BlazorInputFile;
using Microsoft.AspNetCore.Hosting;
using SGRBlazorApp.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SGRBlazorApp.Models
{
    public class FileUpload : IFileUpload
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> Upload(IFileListEntry file, string Id)
        {
            var pathString = Path.Combine(_webHostEnvironment.ContentRootPath, "Files", Id.ToString());
            bool exists = System.IO.Directory.Exists(pathString);
            if (!exists)
                System.IO.Directory.CreateDirectory(pathString);
            var path = Path.Combine(_webHostEnvironment.ContentRootPath, "Files", Id.ToString(), file.Name);
            var memoryStream = new MemoryStream();

            await file.Data.CopyToAsync(memoryStream);

            using (FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                memoryStream.WriteTo(fileStream);
            }

            return await Task.FromResult(path);

        }
    }
}

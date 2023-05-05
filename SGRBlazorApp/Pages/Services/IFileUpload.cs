using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SGRBlazorApp.Services
{
    public interface IFileUpload
    {
        Task<string> Upload(IFileListEntry file, string Id);
    }
}

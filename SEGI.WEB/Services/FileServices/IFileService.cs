using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Services.FileServices
{
    public interface IFileService
    {
        Task<string> SaveFile(IFormFile file, string folderName);
        IFormFile ConvertFilePathToIFormFile(string filePath);
    }
}

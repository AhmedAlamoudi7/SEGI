using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEGI.Services.FileServices
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> SaveFile(IFormFile file, string folderName)
        {
            string fileName = null;
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, folderName);
                fileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                    await file.CopyToAsync(fileStream);
            }
            return fileName;
        }
        public IFormFile ConvertFilePathToIFormFile(string filePath)
        {
            // Read the file into a stream
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            // Create an IFormFile instance
            IFormFile formFile = new FormFile(fileStream, 0, fileStream.Length, "name", Path.GetFileName(filePath))
            {
                Headers = new HeaderDictionary(),
                ContentType = "application/octet-stream"
            };

            return formFile;
        }
    }
}
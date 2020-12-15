using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace Web_BTL_Backend.Models
{
    public class FileServices
    {
        public const string rootAssetsPath = "h:\\root\\home\\fcbtruong-001\\www\\assets\\images";
        public void SaveFile(List<IFormFile> files, string subDirectory)
        {
            subDirectory = subDirectory ?? string.Empty;
            var target = Path.Combine(rootAssetsPath, "");

            Directory.CreateDirectory(target);

            files.ForEach(async file =>
            {
                if (file.Length <= 0) return;
                var filePath = Path.Combine(target, file.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            });
        }
    }
}

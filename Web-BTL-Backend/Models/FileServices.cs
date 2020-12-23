using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;

namespace Web_BTL_Backend.Models
{
    public class FileServices
    {
        public const string rootAssetsPath = "h:\\root\\home\\fcbtruong-001\\www\\assets\\images";
        public void SaveFile(List<IFormFile> files, string subDirectory)
        {
            try
            {
                subDirectory = subDirectory ?? string.Empty;
                var target = Path.Combine(rootAssetsPath, subDirectory);

                Directory.CreateDirectory(target);

                int i = 0;
                files.ForEach(async file =>
                {
                    if (file.Length <= 0) return;
                    var filePath = Path.Combine(target, i.ToString() + ".jpg");
                    i++;
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Error: {exception.Message}");
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MimeTypes;
using System;
using System.IO;

namespace Web_BTL_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        public const string assetImagePath = "h:\\root\\home\\fcbtruong-001\\www\\assets\\images\\";
        [HttpGet]
        public IActionResult Image()
        {
            return Ok();
        }

        [HttpGet]
        [Route("GetImage")]
        public IActionResult GetImage(string subDir)
        {
            if (subDir == null || subDir == "") return BadRequest();
            try
            {
                string typeFile = Path.GetExtension(subDir);
                string mimeType = MimeTypeMap.GetMimeType(typeFile);
                var result = new PhysicalFileResult(assetImagePath + subDir, mimeType)
                {
                    FileDownloadName = subDir,
                    FileName = assetImagePath + subDir,
                };
                return result;
            }
            catch (Exception exception)
            {
                return BadRequest($"Error: {exception.Message}");
            }
        }
    }
}

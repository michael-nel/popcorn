using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace api.Controllers
{
    public class UploadController : Controller
    {
        public static IWebHostEnvironment _environment;

        public UploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [Authorize]
        [HttpPost("api/upload")]
        public async Task<IActionResult> File([FromForm] IFormFile file)
        {
            try
            {
                if (file.Length > 0)
                {

                    var fileName = Guid.NewGuid() + "." + file.FileName.Split(".")[1].ToString();
                    var uploadPath = Path.Combine(_environment.ContentRootPath, "spool", fileName);
                    using (FileStream filestream = System.IO.File.Create(uploadPath))
                    {
                        await file.CopyToAsync(filestream);
                        filestream.Flush();
                        return Ok(fileName);
                    }
                }
                else
                {
                    return BadRequest("Fail to Upload File");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}

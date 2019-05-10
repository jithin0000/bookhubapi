using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace angu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IHostingEnvironment _host;

        public FileUploadController(IHostingEnvironment host )
        {
            _host = host;
        }


        [HttpPost]

        public async Task<IActionResult> UploadFile(IFormFile file)
        {

            var uploadpath = Path.Combine(_host.WebRootPath , "images");

            if(!Directory.Exists(uploadpath))
            {
                Directory.CreateDirectory(uploadpath);
            }

            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filepath = Path.Combine(uploadpath, filename);

            using(var stream = new FileStream(filepath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok( new { file  = filename});

            
        }
        
    }
}
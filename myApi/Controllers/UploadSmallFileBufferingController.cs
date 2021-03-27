using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace myApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class UploadSmallFileBufferingController : ControllerBase
    {
        private ILogger _logger;

        public UploadSmallFileBufferingController(ILogger<UploadSmallFileBufferingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Welcome. Let's start sending files";
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile myfile)
        {
            try
            {
                //Gerar nome do arquivo
                //salvar o arquivo
                var newFileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(myfile.FileName);

                using(var stream = System.IO.File.Create(Path.Combine("Uploads", newFileName)))
                {
                    await myfile.CopyToAsync(stream);
                }
                return await Task.FromResult(Ok());
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error processing file ", $"File name: {Path.GetFileName(myfile.FileName)}, Content type: {myfile.ContentType}, File length: {myfile.Length}");
                return await Task.FromResult(Problem("Não foi possível realizar o upload do arquivo"));
            }
        }
    }
}
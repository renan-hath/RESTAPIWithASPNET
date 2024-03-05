using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTWithNET8.Businesses;
using RESTWithNET8.Data.ValueObjects;

namespace RESTWithNET8.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class FileController : ControllerBase
    {
        private readonly IFileBusiness _fileBusiness;

        public FileController(IFileBusiness fileBusiness)
        {
            _fileBusiness = fileBusiness;
        }

        [HttpGet("downloadFile/{fileName}")]
        [ProducesResponseType((200), Type = typeof(byte[]))]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/octet-stream")]
        public async Task<IActionResult> DownloadFile(string fileName)
        {
            byte[] buffer = _fileBusiness.GetFile(fileName);

            if (buffer != null)
            {
                HttpContext.Response.ContentType = 
                    $"application/{Path.GetExtension(fileName).Replace(".", "")}";

                HttpContext.Response.Headers.Add("content-length", buffer.Length.ToString());

                await HttpContext.Response.Body.WriteAsync(buffer, 0, buffer.Length);
            }

            return new ContentResult();
        }

        [HttpPost("uploadFile")]
        [ProducesResponseType((200), Type = typeof(FileVO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadOneFile([FromForm] IFormFile file)
        {
            FileVO fileVO = await _fileBusiness.SaveFileToDisk(file);

            return new OkObjectResult(fileVO);
        }

        [HttpPost("uploadFiles")]
        [ProducesResponseType((200), Type = typeof(List<FileVO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [Produces("application/json")]
        public async Task<IActionResult> UploadManyFiles([FromForm] List<IFormFile> files)
        {
            List<FileVO> fileVOs = await _fileBusiness.SaveFilesToDisk(files);

            return new OkObjectResult(fileVOs);
        }
    }
}

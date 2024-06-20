using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MSoC_API.Services;
using System.Net.Mime;

namespace MSoC_API.Controllers;

[ApiController]
public class FileController(FileService service) : ControllerBase
{
    [HttpGet("api/files")]
    public ActionResult<string> GetAllFiles()
    {
        var json = service.GetAllFilesInFolder();
        return Ok(json);
    }

    [HttpGet("api/files/{fileName}")]
    public async Task<ActionResult> GetFile([FromRoute] string fileName)
    {
        var file = await service.GetFile(fileName);

        if (file == null)
        {
            return BadRequest();
        }

        return File(file.Content, file.ContentType, file.Name);
    }
}

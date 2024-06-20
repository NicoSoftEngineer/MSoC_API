using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MSoC_API.Services;

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
    public Task GetFile([FromRoute] string fileName)
    {
        return Task.CompletedTask;
    }

}

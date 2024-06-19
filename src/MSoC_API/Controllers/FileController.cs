using Microsoft.AspNetCore.Mvc;

namespace MSoC_API.Controllers;

[ApiController]
public class FileController
{
    [HttpGet("api/files")]
    public Task GetAllFiles()
    {
        return Task.CompletedTask;
    }

    [HttpGet("api/files/{fileName}")]
    public Task GetFile([FromRoute] string fileName)
    {
        return Task.CompletedTask;
    }

}

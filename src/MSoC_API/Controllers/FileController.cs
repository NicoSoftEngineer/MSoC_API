﻿using Microsoft.AspNetCore.Http.HttpResults;
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
            return BadRequest("File does not exist");
        }

        return File(file.Content, file.ContentType, file.Name);
    }

    //an endpoint that allows user to recieve content of a file 
    [HttpGet("api/files/{fileName}/content")]
    public async Task<ActionResult<string>> GetFileContent([FromRoute] string fileName)
    {
        var content = await service.GetFileContent(fileName);

        if (content == null)
        {
            return BadRequest("File does not exist");
        }

        return Ok(content);
    }

    //an endpoint to return a part of a file
    [HttpGet("api/files/{fileName}/content/range")]
    public async Task<ActionResult<string>> GetFileContentInRange([FromRoute] string fileName, [FromQuery] int start, [FromQuery] int end)
    {
        //make start and end 0 based
        start--;
        end--;

        var content = await service.GetFileContentInRange(fileName, start, end);

        if (content == null)
        {
            return BadRequest("File does not exist");
        }

        return Ok(content);
    }
}

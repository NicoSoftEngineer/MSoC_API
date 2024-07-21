using Microsoft.AspNetCore.Mvc;
using MSoC_API.Services;

namespace MSoC_API.Controllers;

[ApiController]
public class KeywordController(KeyWordService service) : ControllerBase
{
    [HttpGet("api/keywords/{fileName}")]
    public ActionResult<string> GetFile([FromRoute] string fileName)
    {
        var keyWordsInJson = service.GetKeywords(fileName);

        if (keyWordsInJson == null)
        {
            return BadRequest("File does not exist");
        }
        return Ok(keyWordsInJson);

    }
}
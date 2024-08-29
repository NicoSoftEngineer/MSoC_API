using Microsoft.AspNetCore.Mvc;
using MSoC_API.Services;

namespace MSoC_API.Controllers;

[ApiController]
public class KeywordController(KeyWordService service) : ControllerBase
{
    [HttpGet("api/keywords/{fileName}")]
    public ActionResult<string> GetKeyWords([FromRoute] string fileName)
    {
        var keyWordsInJson = service.GetKeywords(fileName);

        if (keyWordsInJson == null)
        {
            return BadRequest("File does not exist");
        }
        return Ok(keyWordsInJson);

    }

    [HttpGet("api/keywords/{fileName}/{keyWord}")]
    public ActionResult<string> GetContentOfFunctionByKeyword([FromRoute] string fileName, [FromRoute] string keyWord)
    {
        var functionContentByKeyword = service.GetFunctionContentByKeyword(fileName, keyWord);

        if (functionContentByKeyword == null)
        {
            return BadRequest("File or keyword of a function/method was not found!!");
        }
        return Ok(functionContentByKeyword);

    }
}
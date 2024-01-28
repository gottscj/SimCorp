using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SimCorp.Api.Exceptions;

namespace SimCorp.Api.WordCounter;

[ApiVersion("1.0")]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class WordCountController : ControllerBase
{
    private readonly IWordCounterService _wordCounterService;

    public WordCountController(IWordCounterService wordCounterService)
    {
        _wordCounterService = wordCounterService;
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<WordCountResultResponse> CountWords(IFormFile file)
    {
        if (file is null)
        {
            throw new BadRequestException("File is required");
        }
        
        var result = await _wordCounterService.CountWords(file.OpenReadStream());

        var response = new WordCountResultResponse(result.Count);
        response.AddRange(result.Select(w => new WordCountResponse(w.Word, w.NumberOfOccurrences)));

        return response;
    }
}
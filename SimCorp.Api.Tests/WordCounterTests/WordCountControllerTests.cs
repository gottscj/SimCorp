using Microsoft.AspNetCore.Http;
using NSubstitute;
using SimCorp.Api.Exceptions;
using SimCorp.Api.WordCounter;

namespace SimCorp.Api.Tests.WordCounterTests;

public class WordCountControllerTests
{
    private IWordCounterService _wordCounterService = null!;

    [SetUp]
    public void Setup()
    {
        _wordCounterService = Substitute.For<IWordCounterService>();
    }

    [Test]
    public async Task CountWords_ValidFile_ReturnsResponse()
    {
        // ARRANGE
        var fileForm = Substitute.For<IFormFile>();
        _wordCounterService.CountWords(Arg.Any<Stream>()).Returns(Task.FromResult(new WordCountResult(2)
        {
            new WordCount("test", 10),
            new WordCount("test1", 9)
        }));
        
        fileForm.OpenReadStream().Returns(new MemoryStream());
        var controller = new WordCountController(_wordCounterService);
        
        // ACT
        var result = await controller.CountWords(fileForm);

        // ASSERT
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count, Is.EqualTo(2));
    }

    [Test]
    public void CountWords_NoFile_ThrowsBadRequest()
    {
        // ARRANGE
        var controller = new WordCountController(_wordCounterService);
        
        var exception = Assert.ThrowsAsync<BadRequestException>(async () => await controller.CountWords(null!));

        // ASSERT
        Assert.That(exception, Is.Not.Null);
    }
}
using DanskeBank.Api.WordCounter;

namespace DanskeBank.Api.Tests.WordCounterTests;

public class WordCounterServiceTests
{
    [Test]
    public async Task CountWords_ValidFile_Success()
    {
        // ARRANGE
        var filePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "WordCounterTests", "test.txt");
        var fileStream = File.OpenRead(filePath);
        var wordCounter = new WordCounterService();

        // ACT
        var result = await wordCounter.CountWords(fileStream);

        // ASSERT
        Assert.That(result.Count, Is.EqualTo(5));
        
        Assert.That(result[0].Word, Is.EqualTo("a"));
        Assert.That(result[0].NumberOfOccurrences, Is.EqualTo(5));
        
        Assert.That(result[1].Word, Is.EqualTo("b"));
        Assert.That(result[1].NumberOfOccurrences, Is.EqualTo(4));
        
        Assert.That(result[2].Word, Is.EqualTo("c"));
        Assert.That(result[2].NumberOfOccurrences, Is.EqualTo(3));
        
        Assert.That(result[3].Word, Is.EqualTo("d"));
        Assert.That(result[3].NumberOfOccurrences, Is.EqualTo(2));
        
        Assert.That(result[4].Word, Is.EqualTo("e"));
        Assert.That(result[4].NumberOfOccurrences, Is.EqualTo(1));
    }

    [Test]
    public void CountWords_NoFile_Throws()
    {
        // ARRANGE
        var wordCounter = new WordCounterService();
        
        // ACT
        var exception = Assert.ThrowsAsync<ArgumentNullException>(async () => await wordCounter.CountWords(null!));

        // ASSERT
        Assert.That(exception, Is.Not.Null);
    }
}
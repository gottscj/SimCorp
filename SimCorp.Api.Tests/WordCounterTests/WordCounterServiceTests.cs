using SimCorp.Api.WordCounter;

namespace SimCorp.Api.Tests.WordCounterTests;

public class WordCounterServiceTests
{
    [Test]
    public async Task CountWords_ValidFile_Success()
    {
        // ARRANGE
        var filePath = Path.Combine(
            TestContext.CurrentContext.TestDirectory, 
            "WordCounterTests", 
            "test.txt"
            );
        
        var fileStream = File.OpenRead(filePath);
        var wordCounter = new WordCounterService();

        // ACT
        var result = await wordCounter.CountWords(fileStream);

        // ASSERT
        Assert.That(result.Count, Is.EqualTo(7));
        Assert.That(result[0].NumberOfOccurrences, Is.EqualTo(2));
        Assert.That(result[1].NumberOfOccurrences, Is.EqualTo(2));
        Assert.That(result[2].NumberOfOccurrences, Is.EqualTo(1));
        Assert.That(result[3].NumberOfOccurrences, Is.EqualTo(1));
        Assert.That(result[4].NumberOfOccurrences, Is.EqualTo(1));
        Assert.That(result[5].NumberOfOccurrences, Is.EqualTo(1));
        Assert.That(result[6].NumberOfOccurrences, Is.EqualTo(1));
        
        AssertKey(result, "Go", 1);
        AssertKey(result, "do", 2);
        AssertKey(result, "that", 2);
        AssertKey(result, "thing", 1);
        AssertKey(result, "you", 1);
        AssertKey(result, "so", 1);
        AssertKey(result, "well", 1);
    }

    private void AssertKey(WordCountResult result, string key, int expectedCount)
    {
        var entry = result.FirstOrDefault(r => r.Word == key);
        Assert.That(entry, Is.Not.Null);
        Assert.That(entry!.NumberOfOccurrences, Is.EqualTo(expectedCount));
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
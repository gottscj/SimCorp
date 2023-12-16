namespace DanskeBank.Api.WordCounter;

public interface IWordCounterService
{
    Task<WordCountResult> CountWords(Stream fileStream);
}
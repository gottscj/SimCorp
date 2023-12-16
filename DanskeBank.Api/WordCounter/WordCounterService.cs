namespace DanskeBank.Api.WordCounter;

public class WordCounterService : IWordCounterService
{
    public async Task<WordCountResult> CountWords(Stream fileStream)
    {
        ArgumentNullException.ThrowIfNull(fileStream);

        // make sure we read from the the beginning of the stream
        if (fileStream.CanSeek)
        {
            fileStream.Seek(0, SeekOrigin.Begin);
        }

        using var sr = new StreamReader(fileStream);
        var words = new Dictionary<string, long>(StringComparer.InvariantCultureIgnoreCase);
        while (!sr.EndOfStream)
        {
            var line = await sr.ReadLineAsync() ?? "";
            foreach (var word in line.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            {
                if (words.TryGetValue(word, out var count))
                {
                    words[word] = count + 1;
                }
                else
                {
                    words[word] = 1;
                }
            }
        }

        var result = new WordCountResult(words.Count);
        foreach (var (word, numberOfOccurrences) in words.OrderByDescending(v => v.Value))
        {
            result.Add(new WordCount(word, numberOfOccurrences));
        }

        return result;
    }
}
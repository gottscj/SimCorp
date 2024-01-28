namespace SimCorp.Api.WordCounter;

public record WordCount(string Word, long NumberOfOccurrences);

public class WordCountResult(int capacity) : List<WordCount>(capacity);

// DTO's
public record WordCountResponse(string Word, long NumberOfOccurrences);

public class WordCountResultResponse(int capacity) : List<WordCountResponse>(capacity);
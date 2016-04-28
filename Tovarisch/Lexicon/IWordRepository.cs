namespace Wacton.Tovarisch.Lexicon
{
    public interface IWordRepository
    {
        string GetRandomWord(WordClass wordClass);

        string GetRandomWord();
    }
}

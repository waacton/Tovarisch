namespace Wacton.Tovarisch.Lexicon
{
    public interface IWordProvider
    {
        string GetRandomWord(WordClass wordClass);

        string GetRandomWord();
    }
}

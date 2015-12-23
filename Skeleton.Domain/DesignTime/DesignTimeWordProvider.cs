namespace Wacton.Skeleton.Domain.DesignTime
{
    using Wacton.Tovarisch.Lexicon;

    /// <summary>
    /// used to provide model data during design time of MainView.xaml
    /// (when hooked up correctly)
    /// </summary>
    public class DesignTimeWordProvider : IWordProvider
    {
        private const string DesignTimeWord = "wacton";

        public string GetRandomWord(WordClass wordClass)
        {
            return DesignTimeWord;
        }

        public string GetRandomWord()
        {
            return DesignTimeWord;
        }
    }
}

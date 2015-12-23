namespace Wacton.Skeleton.Domain.Mains
{
    using Wacton.Tovarisch.Lexicon;

    public class Main
    {
        private readonly IWordProvider wordProvider;
        public string Word { get; private set; }

        /// <summary>
        /// parameter will be handled by ninject 
        /// and interface ensures adaptable for design time and testing
        /// </summary>
        public Main(IWordProvider wordProvider)
        {
            this.wordProvider = wordProvider;
            this.Update();
        }

        /// <summary>
        /// the model command uses this method to update the model
        /// </summary>
        public void Update()
        {
            this.Word = this.wordProvider.GetRandomWord();
        }
    }
}

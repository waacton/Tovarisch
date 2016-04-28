namespace Wacton.Skeleton.Domain.Mains
{
    using Wacton.Tovarisch.Lexicon;

    public class Main
    {
        private readonly IWordRepository wordRepository;
        public string Word { get; private set; }

        /// <summary>
        /// parameter will be handled by ninject 
        /// and interface ensures adaptable for design time and testing
        /// </summary>
        public Main(IWordRepository wordRepository)
        {
            this.wordRepository = wordRepository;
            this.Update();
        }

        /// <summary>
        /// the model command uses this method to update the model
        /// </summary>
        public void Update()
        {
            this.Word = this.wordRepository.GetRandomWord();
        }
    }
}

namespace Skeleton.Tests
{
    using Moq;

    using NUnit.Framework;

    using Wacton.Skeleton.Domain.Mains;
    using Wacton.Tovarisch.Lexicon;

    [TestFixture]
    public class ModelTest
    {
        [Test]
        public void MainTest()
        {
            const string TestWord = "wactontest";

            // using moq to help testing without writing dummy classes
            var mockWordProvider = new Mock<IWordProvider>();
            mockWordProvider.Setup(provider => provider.GetRandomWord()).Returns(TestWord);
            var wordProvider = mockWordProvider.Object;

            var main = new Main(wordProvider);
            main.Update();

            Assert.That(main.Word, Is.EqualTo(TestWord));
        }
    }
}

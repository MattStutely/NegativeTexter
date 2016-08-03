using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContentServices;
using NUnit.Framework;
namespace ContentConsole.Test.Unit
{
    public class Tests
    {
        private List<string> _badWords;
        
        [SetUp]
        public void Setup()
        {
            _badWords = new List<string>
            {
                "swine",
                "bad",
                "nasty",
                "horrible"
            };
        }

        [Test]
        public void CheckBadWordCounter()
        {
            var sut = new BadWordService(_badWords);
            var content =
                "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";

            var badWordCount = sut.GetBadWordCount(content);
            Assert.That(badWordCount==2);
        }

        [Test]
        public void CheckBadWordFilter()
        {
            var sut = new BadWordService(_badWords);
            var content =
                "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.";

            var filteredText= sut.FilterContentForBadWords(content);
            Assert.That(filteredText == "The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.");
        }

        [Test]
        public void CheckBadWordList()
        {
            var sut = new BadWordService(_badWords);
            var badWords = sut.GetBadWordList();
            Assert.That(badWords == "swine,bad,nasty,horrible");
        }

        [Test]
        public void CheckSetBadWordList()
        {
            var sut = new BadWordService(_badWords);
            sut.SetBadWordList("swine,bad,nasty,horrible,smelly,stinky,ugly");
            var badWords = sut.GetBadWordList();
            Assert.That(badWords == "swine,bad,nasty,horrible,smelly,stinky,ugly");
        }

        [Test]
        public void TestStory1Output()
        {
            var injectService = new BadWordService(_badWords);
            var sut = new AppService(injectService);
            var output = sut.NegativeWordCounter(
                "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.");

            Assert.That(output == "Scanned the text: The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.\r\nTotal Number of negative words: 2");
        }

        [Test]
        public void TestStory2Output()
        {
            var injectService = new BadWordService(_badWords);
            var sut = new AppService(injectService);
            var output = sut.UpdateBadWordList("swine,bad,nasty,horrible,smelly,stinky,ugly");
            Assert.That(output == "New bad word list set, there are now 7 bad words.");
        }

        [Test]
        public void TestStory3Output()
        {
            var injectService = new BadWordService(_badWords);
            var sut = new AppService(injectService);
            var output = sut.NegativeWordFilter(
                "The weather in Manchester in winter is bad. It rains all the time - it must be horrible for people visiting.");

            Assert.That(output == "Filtered text: The weather in Manchester in winter is b#d. It rains all the time - it must be h######e for people visiting.");
        }
    }
}

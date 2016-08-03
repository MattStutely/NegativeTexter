using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentServices
{
    public class AppService : IAppService
    {
        //in real life I'd use IoC here (StructureMap) but don't really have time to set it all up for this app
        private readonly IBadWordService _badWordService;
        
        public AppService(IBadWordService badWordService)
        {
            _badWordService = badWordService;
        }

        public string NegativeWordCounter(string input)
        {
            var badWordCount = _badWordService.GetBadWordCount(input);
            var output = "Scanned the text: ";
            output += input + Environment.NewLine;
            output += "Total Number of negative words: " + badWordCount;

            return output;
        }

        public string UpdateBadWordList(string input)
        {
            var badWords =_badWordService.SetBadWordList(input);
            var output = string.Format("New bad word list set, there are now {0} bad words.", badWords);
            return output;
        }

        public string NegativeWordFilter(string input)
        {
            var filteredText = _badWordService.FilterContentForBadWords(input);
            var output = "Filtered text: " + filteredText;

            return output;
        }
    }
}

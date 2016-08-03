using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentServices
{
    public class BadWordService : IBadWordService
    {
        private List<string> _badWords { get; set; }

        public BadWordService(List<string> badWords)
        {
            _badWords = badWords;
        }

        private MatchCollection GetMatchedWords(string content)
        {
            var regexPattern = String.Join("|", _badWords.ToArray());
            return Regex.Matches(content, regexPattern, RegexOptions.IgnoreCase);
        }

        private string FilterWord(string wordToFilter)
        {
            var firstLetter = wordToFilter.Substring(0, 1);
            var wordLength = wordToFilter.Length;
            var lastLetter = wordToFilter.Substring(wordToFilter.Length - 1,1);
            var filterChars = new string(Convert.ToChar("#"), wordLength - 2);

            return firstLetter + filterChars + lastLetter;
        }

        public int GetBadWordCount(string content)
        {
            if (_badWords== null)
            {
                //could throw exception here, but for now just return 0 as no badwords set and therefore there can't be any in the content
                return 0;
            }

            var matches = GetMatchedWords(content);
            return matches.Count;
        }

        public string FilterContentForBadWords(string content)
        {
            if (_badWords == null)
            {
                //could throw exception here, but for now just return 0 as no badwords set and therefore there can't be any in the content
                return content;
            }
            var matches = GetMatchedWords(content);
            foreach (var match in matches)
            {
                content = content.Replace(match.ToString(), FilterWord(match.ToString()));
            }
            return content;
        }

        public int SetBadWordList(string words)
        {
            //lose any spaces, not needed
            words = words.Replace(" ", "");
            _badWords = words.Split(',').ToList();
            return _badWords.Count;
        }

        public string GetBadWordList()
        {
            return String.Join(",", _badWords.ToArray());
        }
    }
}

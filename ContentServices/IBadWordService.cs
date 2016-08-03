using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ContentServices
{
    public interface IBadWordService
    {
        int GetBadWordCount(string content);
        string FilterContentForBadWords(string content);
        string GetBadWordList();
        int SetBadWordList(string words);
    }
}

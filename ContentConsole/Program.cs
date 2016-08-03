using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ContentServices;

namespace ContentConsole
{
    public static class Program
    {
        private static List<string> _badWords;
        private static IBadWordService _badWordService;
        private static IAppService _appService;


        public static void Main(string[] args)
        {
            _badWords = new List<string>
            {
                "swine",
                "bad",
                "nasty",
                "horrible"
            };

            //in real life I'd use IoC here (StructureMap) but don't really have time to set it all up for this app
            _badWordService = new BadWordService(_badWords);
            _appService = new AppService(_badWordService);
            var keyValue = "";
            while (keyValue != "q")
            {
                Console.WriteLine("Current negative word list: " +_badWordService.GetBadWordList());
                Console.WriteLine();
                Console.WriteLine("Please select from the following options:");
                Console.WriteLine("-----------------------------------------");
                Console.WriteLine("[S]ee the number of negative words in a text input (story 1)");
                Console.WriteLine("[U]pdate the negative word list (story 2)");
                Console.WriteLine("[F]ilter negative words out of the text (story 3)");
                Console.WriteLine("[Q]uit");
                keyValue = Console.ReadLine().ToLower();
                switch (keyValue)
                {
                    case "s":
                        Story1();
                        break;
                    case "u":
                        Story2();
                        break;
                    case "f":
                        Story3();
                        break;
                    default:
                        Console.WriteLine("Invalid selection, Please select from the options above.");
                        break;
                }
            }
        }

        private static void Story1()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter text to process:");
            var input = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(_appService.NegativeWordCounter(input));
            Console.WriteLine();
        }

        private static void Story2()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter a new bad word list (comma separated) below");
            var input = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(_appService.UpdateBadWordList(input));
            Console.WriteLine();
        }

        private static void Story3()
        {
            Console.WriteLine();
            Console.WriteLine("Please enter text to process:");
            var input = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine(_appService.NegativeWordFilter(input));
            Console.WriteLine();
        }
    }

}

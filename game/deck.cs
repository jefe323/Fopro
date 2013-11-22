using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fopro.game
{
    class deck
    {
        internal static void createDeck()
        {
            string wData = Properties.Resources.wcards;
            string bData = Properties.Resources.bcards;

            string bp = "<>";
            List<string> wWords = wData.Split(new[] { bp }, StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> bWords = bData.Split(new[] { bp }, StringSplitOptions.RemoveEmptyEntries).ToList();

            int i = 0;
            foreach (string s in wWords)
            {
                try
                {
                    Program.whiteDeck.Add(i, s);
                    i++;
                }
                catch { }
            }
            i = 0;
            foreach (string s in bWords)
            {
                try
                {
                    Program.blackDeck.Add(i, s);
                    i++;
                }
                catch { }
            }
        }
    }
}

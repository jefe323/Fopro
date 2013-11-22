using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace fopro
{
    class player
    {
        public string nick;
        public List<int> hand = new List<int>();
        
        public void newPlayer(string n)
        {
            nick = n;
            deal(this);
        }

        private void deal(player p)
        {
            //deal hand to player
            for (int i = 0; i < 10; i++)
            {
                bool check = false;
                //generate random number
                Random r = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                //check to see if number has been played
                while (!check)
                {
                    int card = r.Next(0, Program.whiteDeck.Count);
                    if (!Program.ipWhites.Contains(card))
                    {
                        Program.ipWhites.Add(card);
                        p.hand.Add(card);
                        check = true;
                    }
                }
            }           
        }

        public void draw(int n)
        {
            //draw n cards
            //generate random number
            //check to see if number has been played
            //if it hasn't, place in hand; otherwise generate again
            //rinse and repeat
        }
    }
}

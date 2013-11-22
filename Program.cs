using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meebey.SmartIrc4net;
using System.Threading;

namespace fopro
{
    class Program
    {
        public static IrcClient irc = new IrcClient();
        public static bool active = true;

        internal static Dictionary<int, string> whiteDeck = new Dictionary<int, string>();
        internal static Dictionary<int, string> blackDeck = new Dictionary<int, string>();
        internal static List<int> ipWhites = new List<int>();
        internal static List<int> ipBlacks = new List<int>();
        internal static List<player> players = new List<player>();
        internal static bool gameOn = false;

        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";

            irc.Encoding = System.Text.Encoding.UTF8;
            irc.ActiveChannelSyncing = true;
            irc.AutoReconnect = true;
            irc.AutoRetry = true;
            irc.AutoRelogin = true;
            irc.SendDelay = 500;

            irc.OnChannelMessage += new IrcEventHandler(irc_OnChannelMessage);
            irc.OnConnected += new EventHandler(irc_OnConnected);
            irc.OnDisconnected += new EventHandler(irc_OnDisconnected);

            Console.WriteLine("Connecting to IRC server...");
            try { irc.Connect("irc.esper.net", 6667); }
            catch (Exception e)
            {
                Console.WriteLine("Could not connect, reason: " + e.Message);
            }

            try
            {
                irc.Login("Fopro", "Fopro", 1, "Fopro");
                irc.RfcJoin("#jefe323");
                //irc.SendMessage(SendType.Message, "NickServ", "identify " + bot_ident);

                //new Thread(new ThreadStart(ReadCommands)).Start();

                while (active)
                {
                    try
                    {
                        irc.ListenOnce();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message + "\n\n" + e.StackTrace);
                        continue;
                    }
                }
                irc.Disconnect();
            }
            catch { }
            
        }

        static void irc_OnDisconnected(object sender, EventArgs e)
        {
            bool connected = false;
            Console.WriteLine("Lost connection to server, attempting to reconnect...");

            while (connected == false)
            {
                try
                {
                    irc.Connect("irc.esper.net", 6667);
                    irc.Login("Fopro", "Fopro", 1, "Fopro");
                    irc.RfcJoin("#jefe323");
                    //irc.SendMessage(SendType.Message, "NickServ", "identify " + bot_ident);
                    connected = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Failed to connect, trying again in 30 seconds...");
                    Thread.Sleep(30000);
                }
            }
        }

        static void irc_OnConnected(object sender, EventArgs e)
        {
            Console.WriteLine("Connected!");
        }

        static void irc_OnChannelMessage(object sender, IrcEventArgs e)
        {
            if (e.Data.Message.StartsWith("."))
            {
                string[] args = e.Data.Message.TrimEnd().Substring(1).Split(' ');
                string cmd = args[0];

                switch (cmd.ToLower())
                {
                    case "rules":
                    case "rule":
                        irc.SendMessage(SendType.Notice, e.Data.Channel, "Rules will display here");
                        break;
                    case "ping":
                        irc.SendMessage(SendType.Message, e.Data.Channel, "Pong!");
                        break;
                    //start the game (.newgame)
                    //get list of people who are playing (.join)
                    //user who started game is the first card czar
                    //stop game early if needed(.stop)
                    case "newgame":
                        irc.SendMessage(SendType.Message, e.Data.Channel, utils.TextFormatting.Bold((char)3 + "0,3" + "*** " + e.Data.Nick + " has started a new game. Type '.join' to get in on the action! ***" + (char)3 + "0,3"));
                        fopro.game.deck.createDeck();
                        gameOn = true;
                        break;
                    case "endgame":
                        if (gameOn == true)
                        {
                            irc.SendMessage(SendType.Message, e.Data.Channel, utils.TextFormatting.Bold((char)3 + "0,4" + "*** " + e.Data.Nick + " has stopped the game early :( ***" + (char)3 + "0,4"));
                            gameOn = false;
                        }
                        break;
                    case "join":
                        if (gameOn == true)
                        {
                            bool pCheck = false;
                            player p = new player();
                            p.newPlayer(e.Data.Nick);
                            foreach (player d in players)
                            {
                                if (d.nick == e.Data.Nick)
                                {
                                    pCheck = true;
                                }
                            }
                            if (pCheck == true) { irc.SendMessage(SendType.Message, e.Data.Channel, String.Format("You are already in the game {0}", e.Data.Nick)); }
                            else
                            {
                                players.Add(p);
                                irc.SendMessage(SendType.Message, e.Data.Channel, String.Format(utils.TextFormatting.Bold("*** {0} has joined the game! ***"), e.Data.Nick));
                                Console.WriteLine("New Player Nick: {0}", p.nick);
                            }
                        }
                        else {  irc.SendMessage(SendType.Notice, e.Data.Channel, "There is no active game to join. If you would like to start a new game please type '.NewGame'"); }
                        break;                        
                    case "players":
                        player boss = players.Find(name => name.nick == "jefe_");
                        Console.WriteLine(boss.nick);
                        foreach (player x in players)
                        {
                            if (x.nick == "jefe_")
                            {

                            }
                            Console.WriteLine("nick: {0}", x.nick);
                            Console.WriteLine("{0}:{1}", x.hand[x.hand.Count - 1], x.hand.Count);
                            Console.Write("hand: ");
                            x.hand.RemoveAt(3);
                            for (int i = 0; i < x.hand.Count; i++)
                            {
                                Console.Write("* {0} *", x.hand[i]);
                            }
                            Console.WriteLine();
                            Console.WriteLine("{0}:{1}", x.hand[x.hand.Count - 1], x.hand.Count);
                        }
                        break;
                    case "hand":
                        string hand = string.Empty;
                        player user = players.Find(name => name.nick == e.Data.Nick);
                        for (int i = 0; i < user.hand.Count; i++)
                        {
                            string card = whiteDeck[user.hand[i]];
                            hand += " [" + i + "] " + card;
                        }
                        irc.SendMessage(SendType.Notice, e.Data.Channel, hand);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}

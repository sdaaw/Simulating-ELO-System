using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace ELOTest
{
    class Program
    {

        public static int playerCount = 0;
        public static List<Player> playerList = new List<Player>();


        public static string[] names = new string[]
        {
            "Finland",
            "Sweden",
            "Norway",
            "Germany",
            "Russia",
            "South Korea",
            "Japan",
            "USA",
            "France",
            "North Korea",
            "United Kingdom",
            "Estonia",
            "Poland",
            "Denmark",
            "Brazil",
            "Thailand",
            "China",
            "Italy",
            "Ukraine",
            "Spain",
            "Ireland",
            "Iceland",

        };



        static void Main(string[] args)
        {
            Console.Title = "ELO Testing";
            genPreset(); //generates a ready preset
            Menu();
        }

        public static void genPreset()
        {
            for (int i = 0; i < names.Count(); i++)
            {
                Player a = new Player();
                a.playerName = names[i];
                playerList.Add(a);
            }
        }

        public static void Menu() //some weird menu
        {
            Console.Write(">>");
            string command = Console.ReadLine();
            switch (command)
            {
                case "gen":
                    {
                        genPreset();
                        Menu();
                        break;
                    }
                case "print":
                    {
                        printPlayers(); //prints all active players
                        break;
                    }
                case "create": //creating your own player
                    {
                        CreatePlayer();
                        break;
                    }
                case "cls":
                    {
                        Console.Clear();
                        Menu();
                        break;
                    }
                case "start":
                    {
                        Thread battleThread = new Thread(BattleThread);
                        battleThread.Start();
                        Menu();
                        break;
                    }

                case "find": //for specific 1v1's
                    {
                        Player selectedPlayer = new Player();
                        Console.Write("Find opponent for: ");
                        string sName = Console.ReadLine();
                        for (int i = 0; i < playerList.Count(); i++)
                        {
                            if (playerList[i].playerName.Contains(sName))
                            {
                                selectedPlayer = playerList[i];
                            }
                        }
                        SelectOpponent(selectedPlayer);
                        Menu();
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid command");
                        Menu();
                        break;
                    }
            }
        }

        public static void CreatePlayer()
        {
            Player a = new Player();
            playerList.Add(a);
            Menu();
        }

        public static void BattleThread()
        {
            Random rng = new Random();
            Player plr1 = playerList[rng.Next(0, playerList.Count())];
            Player plr2 = SelectRandom(plr1);
            battle(plr1, plr2);
        }

        public static Player SelectOpponent(Player plr) //buggy and needs a lot of tweaking
        {
            Random rng = new Random();
            double bestResult = 2000;
            List<Player> sortedRating = playerList.OrderByDescending(pL => pL.rating).ToList();
            Player nOpponent = sortedRating[sortedRating.Count() - 1]; //get the 2nd highest player

            for (int i = 0; i < playerList.Count(); i++)
            {
                if (plr.rating < playerList[i].rating && plr.playerName != playerList[i].playerName)
                {
                    if(playerList[i].rating - plr.rating < bestResult)
                    {
                        bestResult = playerList[i].rating - plr.rating;
                        nOpponent = playerList[i];
                    }
                }
            }

            //Console.WriteLine(plr.playerName + "(" + plr.rating + ") vs " + nOpponent.playerName + "(" + nOpponent.rating + ")" );
            return nOpponent;
        }

        public static Player SelectRandom(Player plr)
        {
            Random rng = new Random();
            Player nOpponent = playerList[rng.Next(0, playerList.Count())]; //selects a random player from the list
            if(nOpponent == plr) //to make sure it doesnt pick himself
            {
                SelectRandom(plr); //lets try again
            }
            //Console.WriteLine(plr.playerName + "(" + plr.rating + ") vs " + nOpponent.playerName + "(" + nOpponent.rating + ")" );
            return nOpponent;
        }

        public static void battle(Player p1, Player p2)
        {
            Console.Clear();
            Random rng = new Random();
            Result result;
            bool p1Win;
            EloRating erM = new EloRating();
            if (rng.Next(0, 2) == 1)
                p1Win = true;
            else
                p1Win = false;

            result = erM.UpdateScores(p1.rating, p2.rating, p1Win);
            /*if(p1Win) //for logging
                Console.WriteLine(p1.playerName + "(" + p1.rating + "->" + result.p1 +")" + " wins against " + p2.playerName + "(" + p2.rating + "->" + result.p2 + ")\n");
            else
                Console.WriteLine(p2.playerName + "(" + p2.rating + "->" + result.p2 + ")" + " wins against " + p1.playerName + "(" + p1.rating + "->" + result.p1 + ")\n");*/


            p1.rating = result.p1;
            p2.rating = result.p2;
            List<Player> sortedRating = playerList.OrderByDescending(pL => pL.rating).ToList(); //a bad way to "update" the list
            for (int i = 0; i < playerList.Count(); i++)
            {
                Console.Write("#" + (i + 1) + " " + sortedRating[i].playerName + ": " + sortedRating[i].rating + "\n");
                //Console.Write(playerList[i].playerName + ": " + playerList[i].rating + "\n");
            }
            //Thread.Sleep(1000);
            Console.ReadKey();
            BattleThread();
        }

        public static void printPlayers()
        {
            for (int i = 0; i < playerList.Count(); i++)
            {a
                Console.WriteLine(playerList[i].playerName + ": " + playerList[i].rating);
            }
            Menu();
        }
    }
}

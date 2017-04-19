using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELOTest
{
    class Player
    {
        static Random rng = new Random();
        public int wins = 0;
        public int defeats = 0;
        public int rating = 1500;
        public string playerName = null;

        public static string EnterName() //for adding custom players
        {
            Console.WriteLine("Please enter a name for your player: ");
            string newname = Console.ReadLine();
            return newname;
        }

        public static void UpdateList()
        {

        }
    }
}


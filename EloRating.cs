using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ELOTest
{
    public class EloRating
    {
        /*
        all of this is quickly put together, but should do the job
        */

        public int newP1Rating;
        public int newP2Rating;

        public Result UpdateScores(int player1, int player2, bool user1WonMatch) 
        {
            if(user1WonMatch)
            {
                if (player2 >= 1200) //min. rating at 1.2k, cant go much below this.
                {
                    double e = 120 - Math.Round(1 / (1 + Math.Pow(10, (player2 - player1) / 400)) * 120);
                    player1 = player1 + (int)e;
                    player2 = player2 - (int)e;
                }
                else
                {
                    double e = 120 - Math.Round(1 / (1 + Math.Pow(10, (player2 - player1) / 400)) * 120);
                    player1 = player1 + (int)e;
                }
            }
            else
            {
                if(player1 >= 1200)
                {
                    double e = 120 - Math.Round(1 / (1 + Math.Pow(10, (player1 - player2) / 400)) * 120);
                    player2 = player2 + (int)e;
                    player1 = player1 - (int)e;
                }
                else
                {
                    double e = 120 - Math.Round(1 / (1 + Math.Pow(10, (player1 - player2) / 400)) * 120);
                    player2 = player2 + (int)e;
                }
            }
            Result a = new Result(player1, player2);
            return a;
        }

    }

}
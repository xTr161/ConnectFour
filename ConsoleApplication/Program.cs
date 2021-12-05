using System;
using System.Collections;
using System.IO;

namespace ConsoleApplication
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            FourInOneGame game1 = new FourInOneGame();
            Player player1 = new Player(1, true, "RED");
            Player player2 = new Player(2, true, "YELLOW");
            game1.NewGame();
            game1.PrintBoard();
            while (!game1.GameOver)
            {
                if (game1.TurnValue == 0)
                {
                    game1.Turn(player1);
                }
                else
                {
                    game1.Turn(player2);
                }
                
            }

            Environment.Exit(0);
        }
    }
}
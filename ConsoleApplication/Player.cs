using System;

namespace ConsoleApplication
{
    public class Player
    {
        public int Value { get; set; }
        private bool IsHuman { get; set; }
        private string Color { get; set; }

        public Player(int value, bool isHuman, string color)
        {
            Value = value;
            IsHuman = isHuman;
            Color = color;
        }

        public int GetMove()
        {
            Console.WriteLine("Player"+ Value +" Make a move between 0 and 6");
            string move = Console.ReadLine();
            int imove = Convert.ToInt32(move);
            return imove;
        }
    }
}
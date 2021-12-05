using System;
using System.Data;
using System.Diagnostics;
using System.IO;

namespace ConsoleApplication
{
    public class FourInOneGame
    {
        private int ColumnSize { get; set; }
        private int RowSize { get; set; }
        private int[,] Board { get; set; }
        public bool GameOver { get; set; }
        public int TurnValue { get; set; }
        private Player Player1 { get; set; }
        private Player Player2 { get; set; }

        public FourInOneGame()
        {
            GameOver = false;
            TurnValue = 0;
        }

        public void NewGame()
        {   
            Console.WriteLine("Please enter the number of rows");
            string rows = Console.ReadLine();
            int parsedRows = Convert.ToInt32(rows);
            Console.WriteLine("Please enter the number of columns");
            string columns = Console.ReadLine();
            int parsedColumns = Convert.ToInt32(columns);
            CreateBoard(parsedColumns,parsedRows);
            GeneratePlayers();
        }
        public void CreateBoard(int newColSize, int newRowSize)
        {
            if (newColSize < 4 || newRowSize < 4)
            {
                throw new ArgumentException("The number of columns or rows must be greater than 4");
            }
            ColumnSize = newColSize;
            RowSize = newRowSize;
            Board = new int [RowSize, ColumnSize];
            for (int i = 0; i < RowSize; i++)
            {
                for (int j = 0; j < ColumnSize; j++)
                {
                    Board[i, j] = 0;
                }
            }
        }

        public void GeneratePlayers(bool isHuman = true)
        {
            Player1 = new Player(1, true, color: "RED");
            Console.WriteLine("Who do you want to play against:\n1)Computer opponent\n" +
                              "2)Human opponent");
            int opponent = Convert.ToInt32(Console.ReadLine());
            if (opponent == 1)
            {
                isHuman = false;
            }
            else if (opponent == 2)
            {
                isHuman = true;
            }
            else
            {
                throw new ConstraintException("Expected a value 1 or 2");
            }
            
            Player2 = new Player(2, isHuman, color: "YELLOW");
        }
        public void PrintBoard()
        {
            int rowLength = Board.GetLength(0);
            int colLength = Board.GetLength(1);
            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    if (j == 0)
                    {
                        Console.Write("| {0} |",Board[i, j]);
                    }
                    else if (j == colLength-1)
                    {
                        Console.Write(" {0} |",Board[i, j]);
                    }
                    else if (j< colLength-1 )
                    {
                        Console.Write(" {0} |",Board[i, j]);    
                    }

                }

                Console.Write(Environment.NewLine);
            }
        }
        public void MakeMove(int value, int column)
        {
            if (IsValidLocation(column))
            {
                int row = GenerateNextOpenRow(column);
                Board[row, column] = value;
            }
        }

        public void Turn(Player player)
        {
            int column = player.GetMove();
            MakeMove(player.Value,column);
            if (WinningMove(player.Value))
            {
                Console.WriteLine("Congratulations player " + Convert.ToString(player.Value)+ "\n You have won!");
                GameOver = true;
            }
            PrintBoard();
            TurnValue += 1;
            TurnValue = TurnValue % 2;
        }
        public bool IsValidLocation(int column)
        {
            if (column >= 0 && column < ColumnSize)
            {
                return Board[5, column] == 0;
            }
            throw new IndexOutOfRangeException("The number entered must be greater than 0 and less than 6");
        }
        public int GenerateNextOpenRow(int column)
        {
            int nextRow = 0;
            for (int row = 0; row < RowSize; row++)
            {
                if (Board[row, column] == 0)
                {
                    nextRow = row;
                    break;
                }
            }

            return nextRow;
        }
        
        public bool WinningMove(int player)
        {
            //check horizontal locations for a win
            for (int c = 0; c < ColumnSize - 3; c++)
            {
                for (int r = 0; r < RowSize; r++)
                {
                    if (Board[r, c] == player && Board[r, c + 1] == player &&
                        Board[r, c + 2] == player && Board[r, c + 3] == player)
                    {
                        Console.WriteLine("Congratulations player"+ Convert.ToString(player)+". You are the winner");
                        return true;
                    }
                }
            }
            //check vertical locations for a win
            for (int c = 0; c < ColumnSize; c++)
            {
                for (int r = 0; r < RowSize -3; r++)
                {
                    if (Board[r, c] == player && Board[r+ 1, c] == player &&
                        Board[r +2, c] == player && Board[r + 3, c] == player)
                    {
                        Console.WriteLine("Congratulations player"+ Convert.ToString(player)+". You are the winner");
                        return true;
                    }
                }
            }
            //check positively sloped diagonals for a win
            for (int c = 0; c < ColumnSize -3; c++)
            {
                for (int r = 0; r < RowSize -3; r++)
                {
                    if (Board[r, c] == player && Board[r+ 1, c +1] == player &&
                        Board[r +2, c +2] == player && Board[r + 3, c + 3] == player)
                    {
                        Console.WriteLine("Congratulations player"+ Convert.ToString(player)+". You are the winner");
                        return true;
                    }
                }
            }
            //check negatively sloped diagonals for a win
            for (int c = 0; c < ColumnSize -3; c++)
            {
                for (int r = 0; r < RowSize -3; r++)
                {
                    if (Board[r, c] == player && Board[r - 1, c +1] == player &&
                        Board[r - 2, c +2] == player && Board[r - 3, c + 3] == player)
                    {
                        Console.WriteLine("Congratulations player"+ Convert.ToString(player)+". You are the winner");
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
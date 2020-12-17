using System;
using System.IO;

namespace Sudoku
{
    public class Board
    {
        private int[,] board = new int[9,9];

        public Board(string filePath)
        {
            // check if file exist
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Path {filePath} does not exist");
            }
            
            var i = 0;
            var reader = new StreamReader(filePath);
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                if (values.Length != 9 || i > 8)
                {
                    reader.Close();
                    throw new Exception($"Malformed format");
                }

                if (!int.TryParse(values[0],out board[i,0]) ||
                    !int.TryParse(values[1],out board[i,1]) ||
                    !int.TryParse(values[2],out board[i,2]) ||
                    !int.TryParse(values[3],out board[i,3]) ||
                    !int.TryParse(values[4],out board[i,4]) ||
                    !int.TryParse(values[5],out board[i,5]) ||
                    !int.TryParse(values[6],out board[i,6]) ||
                    !int.TryParse(values[7],out board[i,7]) ||
                    !int.TryParse(values[8],out board[i,8])
                    )
                {
                    reader.Close();
                    throw new Exception($"Not number found in {filePath}");
                }

                i++;
            }
        }

        public void PrintBoard()
        {
            //
            for (int i = 0; i < 9; i++)
            {
                if (i % 3 == 0)
                    Console.WriteLine("-------------------------------");
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0)
                        Console.Write("|");
                    Console.Write($" {board[i,j].ToString()} ");
                }
                Console.Write($"|{Environment.NewLine}");
            }
            Console.WriteLine("-------------------------------");
        }
    }
}
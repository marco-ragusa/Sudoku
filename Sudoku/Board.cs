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

        public bool CheckCompleted()
        {
            var result = true;

            for (int i = 0; i < 9; i++)
            {
                // Check every row
                var sumRow = 0;
                foreach (var cell in new CustomArray<int>().GetRow(board, i))
                {
                    sumRow += cell;
                }
                if (sumRow != 45)
                {
                    // Console.WriteLine($"row {i} not completed");
                    result = false;
                }
                
                // Check every col
                var sumCol = 0;
                foreach (var cell in new CustomArray<int>().GetColumn(board, i))
                {
                    sumCol += cell;
                }
                if (sumCol != 45)
                {
                    // Console.WriteLine($"col {i} not completed");
                    result = false;
                }
            }
            
            // Check every 3x3 cell
            for (int i = 0; i < 9; i+=3)
            {
                for (int j = 0; j < 9; j+=3)
                {
                    int sumCell = 0;
                    for (int k = i; k < i+3; k++)
                    {
                        for (int l = j; l < j+3; l++)
                        {
                            sumCell += board[k, l];
                        }
                    }
                    if (sumCell != 45)
                    {
                        // Console.WriteLine($"cell {i}x{j} not completed");
                        result = false;
                    }
                }
            }

            return result;
        }
    }
}
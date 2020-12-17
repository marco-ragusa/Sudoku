using System;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Insert the csv sudoku file path: ");
            var filePath = Console.ReadLine();
            Board sudokuBoard = new Board(filePath);
            // Board sudokuBoard = new Board("/home/marco/sudoku.txt");
            sudokuBoard.PrintBoard();
        }
    }
}
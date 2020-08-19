using System;
using SudokuSolverLibrary;

namespace SudokuSolverConsole
{
    class SudokuController
    {

        #region Variables
        static int dimension = 3;
        public int iteration = 3;
        static Sudoku_Board mainSudoku = new Sudoku_Board(dimension);
        static Sudoku_Board solveSudoku = new Sudoku_Board(dimension);
        SudokuSolverLogic logicSudoku;
        #endregion

        #region Methods.
        //Start console and main method.
        public void Main()
        {

            //Variables.
            int choice = 0;
            int exampleChoice = 0;
            int iteration = 1000;
            string restartChoice = "";
            string buffer = "";
            int testInt;
            bool valid = false;
            

            Console.Clear();
            choice = UserChoice();

            if (choice == 1) //Enter sudoku.
            {

                mainSudoku = EnterSudokuFromConsole();

                if (mainSudoku == null)
                {
                    Main();
                }

                valid = ValidateSudoku(mainSudoku);
                if (valid == false)
                {
                    Console.WriteLine("Sudoku is unvalid. Please restart.");
                    Console.ReadLine();
                    Main();
                }
                else
                {
                    Console.WriteLine("Sudoku is valid.");
                }
            }

            else if (choice == 2) //Choose example.
            {
                exampleChoice = ChooseExample();

                mainSudoku = SetSudokuExample(exampleChoice);
                Console.WriteLine("Example:");
                DisplaySudokuToConsole(mainSudoku);
                Console.WriteLine("");
            }
            else
            {
                Environment.Exit(0);
            }

            //Iteration
            Console.Write("Set iteration (default: 1000) : ");
            buffer = Console.ReadLine();
            if (int.TryParse(buffer, out testInt))
            {
                iteration = Convert.ToInt32(buffer);
            }

            //Solve
            long tic = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            logicSudoku = new SudokuSolverLogic(mainSudoku, iteration);
            logicSudoku.SolveSudokuBacktracking();
            long toc = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            solveSudoku = logicSudoku.GetSudoku();

            //Display
            DisplaySudokuToConsole(solveSudoku);
            if (logicSudoku.GetNumberOfIteration() < iteration)
            {
                Console.WriteLine("Solution found in: " + (toc - tic) + "milliseconds");
                Console.WriteLine("Solution found in: " + logicSudoku.GetNumberOfIteration() + " iterations");
                Console.ReadLine();
            }
            else if (logicSudoku.GetNumberOfIteration() == iteration)
            {
                Console.WriteLine("Maximum number of iterations reached : " + logicSudoku.GetNumberOfIteration() + " iteration");
                Console.WriteLine("");
            }

            //Restart or close.
            while (restartChoice != "y" && restartChoice != "n")
            {
                Console.Write("Restart? (y/n): ");
                restartChoice = Console.ReadLine().ToLower();
            }
            if (restartChoice == "y")
            {
                Console.WriteLine("");
                Main();
            }
            else if (restartChoice == "n")
            {
                Environment.Exit(0);
            }

        }

        private int UserChoice()
        {
            int choice = 0;
            char choiceChar;
            Console.WriteLine("What do you want to do: ");
            Console.WriteLine("1) Enter your own Sudoku. ");
            Console.WriteLine("2) Enter an example. ");
            Console.WriteLine("3) Exit.");
            Console.Write("Choice:");

            choice = Console.Read();
            Console.WriteLine("");
            choiceChar = (char)choice;
            choice = int.Parse(choiceChar.ToString());

            if (choice == 1 || choice == 2)
            {
                return choice;
            }
            else if (choice == 3)
            {
                Environment.Exit(0);
            }
            else
            {
                UserChoice();
            }

            return choice;

        }

        private Sudoku_Board EnterSudokuFromConsole()
        {
            Sudoku_Board sudoku = new Sudoku_Board(3);
            int[,] board = new int[9, 9];
            bool sudokuFill = false;
            int row =0;
            int col =0;
            string buffer = "";
            int testInt = 0;

            Console.ReadLine();
            while (sudokuFill != true)
            {
                Console.Clear();
                Console.Write("Sudoku: (q to quit)");
                Console.WriteLine();
                for (int i = 0; i <= row; i++)
                {
                    if (i == row)
                    {
                        for (int j = 0; j < col; j++)
                        {
                            Console.Write(board[i,j] + " ");
                        }
                        Console.Write("X");
                        Console.WriteLine();
                        sudokuFill = IsFilled(row, col);
                    }
                    else if (i < row)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            Console.Write(board[i, j] + " ");
                        }
                        Console.WriteLine();
                    }
                }
                Console.WriteLine();
                Console.Write("Next: ");
                buffer = Console.ReadLine();
                if (int.TryParse(buffer, out testInt) || (row != 0 && col != 0))
                {
                    board[row, col] = testInt;
                    if (col != 8)
                    {
                        col++;
                    }
                    else
                    {
                        col = 0;
                        row++;
                    }
                }
                else if (buffer == "q")
                {
                    return null;
                }
                Console.WriteLine();
            }

            sudoku.FillSudoku(board);
            return sudoku;
        }

        private bool IsFilled(int row, int col)
        {
            if (row == 8 && col == 8)
            {
                return true;
            }

            return false;
        }

        private bool ValidateSudoku(Sudoku_Board sudoku)
        {
            bool b = true;
            int sudokuSquareBuffer = 0;
            for (int i = 0; i<=8; i++)
            {
                for(int j =0; j <=8; j++)
                {
                    if (sudoku.GetSquare(i, j) != 0)
                    {
                        sudokuSquareBuffer = sudoku.GetSquare(i, j);
                        sudoku.SetSquare(i, j, 0);
                        b = sudoku.IsValid(i, j, sudokuSquareBuffer);
                        sudoku.SetSquare(i, j, sudokuSquareBuffer);
                    }
                }
            }
            return b;
        }

        private int ChooseExample()
        {
            int choice = 0;
            Char choiceChar;

            Console.ReadLine();
            Console.Write("Choose an example (1,2 or 3) or return to sudoku choice (4): ");
            choice = Console.Read();
            Console.WriteLine("");
            choiceChar = (char)choice;
            choice = int.Parse(choiceChar.ToString());

            if (choice >= 1 || choice <= 3)
            {
                return choice;
            }
            else if(choice == 4)
            {
                Main();
            }
            else
            {
                ChooseExample();
            }

            return choice;
        }

        private void DisplaySudokuToConsole(Sudoku_Board sudoku)
        {
            for (int i = 0; i < sudoku.GetLength(); i++)
            {
                for (int j = 0; j < sudoku.GetLength(); j++)
                {
                    Console.Write(sudoku.GetSquare(i, j) + " ");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        //Define sudoku from examples.
        public Sudoku_Board SetSudokuExample(int choice)
        {
            SudokuExamples sudokuExample = new SudokuExamples(choice);
            return sudokuExample.GetSudokuExample();
        }
        #endregion
    }
}

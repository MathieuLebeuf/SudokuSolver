using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolver
{
    public class Sudoku_Board
    {
        //N*N  Board
        int N = 3;
        int activeRow = 0;
        int activeCol = 0;

        //Main Board
        int[,] board = new int[9, 9];
        int[,] knownBoard = new int[9, 9];

        //constructor
        public Sudoku_Board()
        {
            //Fill main Array board by 0 value
            for (int i = 0; i <= (N * N) - 1; i++)
            {
                for (int j = 0; j <= (N * N) - 1; j++)
                {
                    board[i, j] = 0;
                    knownBoard[i, j] = 0;
                }
            }

        }

        public void setSquare(int i, int j, int value)
        {
            board[i, j] = value;
        }

        //get value for across row and col value
        public int getSquare(int i, int j)
        {
            return board[i, j];
        }

        public int Size()
        {
            return N;
        }

        public int getActiveRow()
        {
            return activeRow;
        }

        public void setActiveRow(int newRow)
        {
            activeRow= newRow;
        }

        public int getActiveCol()
        {
            return activeCol;
        }

        public int getSquareKnownBoard(int i, int j)
        {
            return knownBoard[i, j];
        }

        public void setActiveCol(int newCol)
        {
            activeCol = newCol;
        }

        // Index number for end condition;
        public int index(int row, int col)
        {
            int indexNumber = (col+1) + (row)*9;
            return indexNumber;
        }

        public void fillSudoku(int[,] sudokuArray)
        {
            // Fill the Sudoku board with the example.
            for (int i = 0; i <= (N * N) - 1; i++)
            {
                for (int j = 0; j <= (N * N) - 1; j++)
                {
                    board[i, j] = sudokuArray[i, j];
                    if (sudokuArray[i, j] != 0)
                    {
                        knownBoard[i, j] = 1;
                    }
                }
            }
        }

        public void moveFowardNextValidSquare()
        {
            if (activeCol != 8)
            {
                activeCol++;
            }
            else
            {
                activeCol = 0;
                activeRow++;
            }

            if (knownBoard[activeRow,activeCol] == 1)
            {
                moveFowardNextValidSquare();
            }
        }

        public void moveBackwardNextValidSquare()
        {
            if (activeCol != 0)
            {
                activeCol--;
            }
            else if(activeCol == 0 & activeRow != 0)
            {
                activeCol = 8;
                activeRow--;
            }

            if (knownBoard[activeRow, activeCol] == 1)
            {
                moveBackwardNextValidSquare();
            }
        }

        //Verify unicity of data.
        public bool IsValid(int row, int col, int n)
        {

            //Verify column.
            for (int k = 0; k < N * N; k++)
            {
                if (board[k, col] == n)
                {
                    return false;
                }
            }

            // Verify row..
            for (int l = 0; l < N * N; l++)
            {
                if (board[row, l] == n)
                {
                    return false;
                }
            }

            // Vérifier si dans carré.
            if ((row >= 0 & row <= 2) & (col >= 0 & col <= 2)) // Premier cadran
            {
                return testSubGrid(0, 0, n);
            }
            else if ((row >= 0 & row <= 2) & (col >= 3 & col <= 5)) // Deuxième cadran
            {
                return testSubGrid(0, 3, n);
            }
            else if ((row >= 0 & row <= 2) & (col >= 6 & col <= 8)) // Troisieme cadran
            {
                return testSubGrid(0, 6, n);
            }
            else if ((row >= 3 & row <= 5) & (col >= 0 & col <= 2)) // Quatrieme cadran
            {
                return testSubGrid(3, 0, n);
            }
            else if ((row >= 3 & row <= 5) & (col >= 3 & col <= 5)) // Cinquième cadran
            {
                return testSubGrid(3, 3, n);
            }
            else if ((row >= 3 & row <= 5) & (col >= 6 & col <= 8)) // Sixième cadran
            {
                return testSubGrid(3, 6, n);
            }
            else if ((row >= 6 & row <= 8) & (col >= 0 & col <= 2)) // Septième cadran
            {
                return testSubGrid(6, 0, n);
            }
            else if ((row >= 6 & row <= 8) & (col >= 3 & col <= 5)) // Huitième cadran
            {
                return testSubGrid(6, 3, n);
            }
            else if ((row >= 6 & row <= 8) & (col >= 6 & col <= 8)) // Neuvième cadran
            {
                return testSubGrid(6, 6,n);
            }

            return true;
        }

        private bool testSubGrid(int row, int col,int n)
        {
            for (int s = row; s <= row + 2; s++) //row
            {
                for (int t = col; t <= col +2; t++) //col
                {
                    if (board[s, t] == n)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}

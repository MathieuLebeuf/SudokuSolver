
namespace SudokuSolverLibrary
{
    public class Sudoku_Board
    {
        #region Variables
        public int Dimension { get; private set; } = 3;
        public int ActiveRow { get; private set; } = 0;
        public int ActiveCol { get; private set; } = 0;
        int[,] board = new int[9, 9];
        int[,] knownBoard = new int[9, 9];
        #endregion

        #region Constructor
        public Sudoku_Board()
        {
            //Fill main Array board by 0 value
            for (int i = 0; i <= (Dimension * Dimension) - 1; i++)
            {
                for (int j = 0; j <= (Dimension * Dimension) - 1; j++)
                {
                    board[i, j] = 0;
                    knownBoard[i, j] = 0;
                }
            }

        }
        #endregion

        #region Methods
        public int[,] GetBoard()
        {
            return board;
        }

        public void SetSquare(int i, int j, int value)
        {
            board[i, j] = value;
        }

        public int GetSquare(int i, int j)
        {
            return board[i, j];
        }

        public int GetLength()
        {
            int length = Dimension * Dimension;

            return length;
        }

        public int GetSquareKnownBoard(int i, int j)
        {
            return knownBoard[i, j];
        }

        public int ReturnIndex(int row, int col)
        {
            int indexNumber = (col+1) + (row)*9;
            return indexNumber;
        }

        public void FillSudoku(int[,] sudokuArray)
        {
            // Fill the Sudoku board with the example.
            for (int i = 0; i <= (Dimension * Dimension) - 1; i++)
            {
                for (int j = 0; j <= (Dimension * Dimension) - 1; j++)
                {
                    board[i, j] = sudokuArray[i, j];
                    if (sudokuArray[i, j] != 0)
                    {
                        knownBoard[i, j] = 1;
                    }
                }
            }
        }

        public void MoveFowardNextValidSquare()
        {
            if (ActiveCol != 8)
            {
                ActiveCol++;
            }
            else
            {
                ActiveCol = 0;
                ActiveRow++;
            }

            if (knownBoard[ActiveRow,ActiveCol] == 1)
            {
                MoveFowardNextValidSquare();
            }
        }

        public void MoveBackwardNextValidSquare()
        {
            if (ActiveCol != 0)
            {
                ActiveCol--;
            }
            else if(ActiveCol == 0 & ActiveRow != 0)
            {
                ActiveCol = 8;
                ActiveRow--;
            }

            if (knownBoard[ActiveRow, ActiveCol] == 1)
            {
                MoveBackwardNextValidSquare();
            }
        }

        //Verify unicity of data.
        public bool IsValid(int row, int col, int n)
        {
            int modRow = row - (row %3);
            int modCol = col - (col %3);

            //Verify column.
            for (int i = 0; i < Dimension * Dimension; i++)
            {
                if (board[i, col] == n)
                {
                    return false;
                }
            }

            // Verify row.
            for (int j = 0; j < Dimension * Dimension; j++)
            {
                if (board[row, j] == n)
                {
                    return false;
                }
            }

            // Verify bloc.
            for (int k = 0; k < 3; k++)
            {
                for (int l = 0; l < 3; l++)
                {
                    if (board[k + modRow, l + modCol] == n)
                    {
                        return false;
                    }
                }
            }
            return true;
        } 
        #endregion
    }
}

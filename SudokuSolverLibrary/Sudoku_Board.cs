
namespace SudokuSolverLibrary
{
    public class Sudoku_Board
    {
        #region Variables
        public int dimension;
        public int ActiveRow { get; private set; } = 0;
        public int ActiveCol { get; private set; } = 0;
        int[,] board = new int[9, 9];
        int[,] knownBoard = new int[9, 9];
        #endregion

        #region Constructor
        public Sudoku_Board(int dim)
        {
            dimension = dim;
            //Fill main Array board by 0 value
            for (int i = 0; i <= (dimension * dimension) - 1; i++)
            {
                for (int j = 0; j <= (dimension * dimension) - 1; j++)
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

        public int GetDimension()
        {
            return dimension;
        }

        public int GetLength()
        {
            int length = dimension * dimension;

            return length;
        }

        public int GetSquareKnownBoard(int i, int j)
        {
            return knownBoard[i, j];
        }

        //Return row array.
        public int[] GetRow(int rowNumber)
        {
            int[] row = new int[dimension * dimension];

            for (int i = 0; i <= 8; i++)
            {
                row[i] = board[rowNumber, i];
            }

            return row;
        }

        //Return col array.
        public int[] GetCol(int colNumber)
        {
            int[] row = new int[dimension * dimension];

            for (int i = 0; i <= 8; i++)
            {
                row[i] = board[i, colNumber];
            }

            return row;
        }

        public int ReturnIndex(int row, int col)
        {
            int indexNumber = (col+1) + (row)*9;
            return indexNumber;
        }

        public void FillSudoku(int[,] sudokuArray)
        {
            // Fill the Sudoku board with the example.
            for (int i = 0; i <= (dimension * dimension) - 1; i++)
            {
                for (int j = 0; j <= (dimension * dimension) - 1; j++)
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

            //Verify column.
            for (int k = 0; k < dimension * dimension; k++)
            {
                if (board[k, col] == n)
                {
                    return false;
                }
            }

            // Verify row..
            for (int l = 0; l < dimension * dimension; l++)
            {
                if (board[row, l] == n)
                {
                    return false;
                }
            }

            // Vérifier si dans carré.
            if ((row >= 0 & row <= 2) & (col >= 0 & col <= 2)) // Premier cadran
            {
                return TestSubGrid(0, 0, n);
            }
            else if ((row >= 0 & row <= 2) & (col >= 3 & col <= 5)) // Deuxième cadran
            {
                return TestSubGrid(0, 3, n);
            }
            else if ((row >= 0 & row <= 2) & (col >= 6 & col <= 8)) // Troisieme cadran
            {
                return TestSubGrid(0, 6, n);
            }
            else if ((row >= 3 & row <= 5) & (col >= 0 & col <= 2)) // Quatrieme cadran
            {
                return TestSubGrid(3, 0, n);
            }
            else if ((row >= 3 & row <= 5) & (col >= 3 & col <= 5)) // Cinquième cadran
            {
                return TestSubGrid(3, 3, n);
            }
            else if ((row >= 3 & row <= 5) & (col >= 6 & col <= 8)) // Sixième cadran
            {
                return TestSubGrid(3, 6, n);
            }
            else if ((row >= 6 & row <= 8) & (col >= 0 & col <= 2)) // Septième cadran
            {
                return TestSubGrid(6, 0, n);
            }
            else if ((row >= 6 & row <= 8) & (col >= 3 & col <= 5)) // Huitième cadran
            {
                return TestSubGrid(6, 3, n);
            }
            else if ((row >= 6 & row <= 8) & (col >= 6 & col <= 8)) // Neuvième cadran
            {
                return TestSubGrid(6, 6,n);
            }

            return true;
        }

        private bool TestSubGrid(int row, int col,int n)
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
        #endregion
    }
}

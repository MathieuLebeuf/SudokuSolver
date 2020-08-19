
namespace SudokuSolverLibrary
{
    public class SudokuSolverLogic
    {
        #region Variables
        public Sudoku_Board sudokuBoard;
        public int iteration;
        public int compt = 0;
        #endregion

        #region Constructor
        public SudokuSolverLogic(Sudoku_Board sudoku, int itr)
        {
            sudokuBoard = sudoku;
            iteration = itr;
        }
        #endregion

        #region Methods
        public void SolveSudokuBacktracking()
        {
            
            bool endOfloop = false;
            int cmptr = 0;
            int nbToAdd = 0;

            while (endOfloop != true)
            {
               // l_itr.Text = String.Concat("Iteration: ", Convert.ToString(cmptr));

                if (sudokuBoard.GetSquareKnownBoard(sudokuBoard.ActiveRow, sudokuBoard.ActiveCol) == 0)
                {

                    nbToAdd = findNumber(sudokuBoard); // Find if number can be put inside square from active square.

                    if (nbToAdd < 10)
                    {
                        sudokuBoard.SetSquare(sudokuBoard.ActiveRow, sudokuBoard.ActiveCol, nbToAdd);
                        endOfloop = VerifierFin(sudokuBoard.ReturnIndex(sudokuBoard.ActiveRow, sudokuBoard.ActiveCol));
                        if (endOfloop == false)
                        {
                            sudokuBoard.MoveFowardNextValidSquare();
                        }

                    }
                    else if (nbToAdd == 10)
                    {
                        sudokuBoard.SetSquare(sudokuBoard.ActiveRow, sudokuBoard.ActiveCol, 0);
                        sudokuBoard.MoveBackwardNextValidSquare();
                    }

                    cmptr++;

                    if (cmptr == iteration)
                    {
                        endOfloop = true;
                    }
                }
                else
                {
                    sudokuBoard.MoveFowardNextValidSquare();
                }

            }

            compt = cmptr;

        }

        static bool VerifierFin(int sudokuIndex) //Fonction pour vérifier si c'est la fin du sudoku. False = Pas fini. True = fini
        {

            if (sudokuIndex == 81)
            {
                return true;
            }
            return false;
        }

        public int findNumber(Sudoku_Board Sudoku)
        {
            for (int i = Sudoku.GetSquare(Sudoku.ActiveRow, Sudoku.ActiveCol); i <= 10; i++)
            {
                if (Sudoku.IsValid(Sudoku.ActiveRow, Sudoku.ActiveCol, i))
                {
                    return i;
                }
            }
            return 10;
        }

        public Sudoku_Board GetSudoku()
        {
            return sudokuBoard;
        }

        public int GetNumberOfIteration()
        {
            return compt;
        }
        #endregion
    }
}
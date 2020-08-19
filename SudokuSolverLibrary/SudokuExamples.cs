
namespace SudokuSolverLibrary
{
    public class SudokuExamples
    {
        #region Variables
        static int Dimension =3;
        private int nbExampleAvailable = 3;
        private Sudoku_Board SudokuExample = new Sudoku_Board(Dimension);

        private int[,] sudokuExemple1 = new int[,]
       {
            { 0, 3, 9, 2, 0, 0, 8, 6, 0 }, //1
            { 0, 0, 0, 0, 0, 0, 9, 4, 7 }, //2
            { 0, 4, 0, 7, 9, 0, 0, 0, 3 }, //3
            { 0, 5, 3, 0, 7, 9, 0, 2, 0 }, //4
            { 0, 0, 1, 8, 3, 2, 4, 0, 0 }, //5
            { 0, 7, 0, 5, 6, 0, 1, 3, 0 }, //6
            { 6, 0, 0, 0, 5, 1, 0, 8, 0 }, //7
            { 2, 1, 4, 0, 0, 0, 0, 0, 0 }, //8
            { 0, 8, 5, 0, 0, 6, 7, 1, 0 } //9
       };

        private int[,] sudokuExemple2 = new int[,]
        {
            { 0, 5, 0, 0, 6, 9, 0, 0, 0 }, //1
            { 0, 0, 0, 0, 0, 0, 0, 8, 0 }, //2
            { 7, 4, 0, 0, 0, 8, 0, 0, 9 }, //3
            { 2, 7, 0, 0, 1, 0, 0, 4, 0 }, //4
            { 0, 0, 6, 4, 0, 2, 7, 0, 0 }, //5
            { 0, 1, 0, 0, 7, 0, 0, 5, 3 }, //6
            { 6, 0, 0, 9, 0, 0, 0, 7, 1 }, //7
            { 0, 9, 0, 0, 0, 0, 0, 0, 0 }, //8
            { 0, 0, 0, 7, 3, 0, 0, 9, 0 } //9
        };

        private int[,] sudokuExemple3 = new int[,]
        {
            { 0, 0, 0, 0, 0, 0, 6, 8, 0 }, //1
            { 0, 0, 0, 0, 7, 3, 0, 0, 9 }, //2
            { 3, 0, 9, 0, 0, 0, 0, 4, 5 }, //3
            { 4, 9, 0, 0, 0, 0, 0, 0, 0 }, //4
            { 8, 0, 3, 0, 5, 0, 9, 0, 2 }, //5
            { 0, 0, 0, 0, 0, 0, 0, 3, 6 }, //6
            { 9, 6, 0, 0, 0, 0, 0, 0, 0 }, //7
            { 7, 0, 0, 6, 8, 0, 0, 0, 0 }, //8
            { 0, 2, 8, 0, 0, 0, 0, 0, 0 } //9
        };
        #endregion

        #region Constructor
        public SudokuExamples(int sudokuChoice)
        {
            if (sudokuChoice == 1)
            {
                SudokuExample.FillSudoku(sudokuExemple1);
            }
            else if (sudokuChoice == 2)
            {
                SudokuExample.FillSudoku(sudokuExemple2);
            }
            else if (sudokuChoice == 3)
                SudokuExample.FillSudoku(sudokuExemple3);
        }
        #endregion

        #region Methods
        public int getNbExampleAvailable()
        {
            return nbExampleAvailable;
        }

        public Sudoku_Board GetSudokuExample()
        {
            return SudokuExample;
        }
        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuSolverConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variables
            SudokuController mainController = new SudokuController();
            
            mainController.Main();
         
        }
    }
}

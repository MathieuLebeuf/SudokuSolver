using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;


// Fonction: Permet de résoudre un Sudoku.
// Date de création: 3 septembre 2019
// Auteur: Mathieu Lebeuf

namespace Sudoku_V2
{


    public partial class Form1 : Form
    {
        int[,] Sudoku = new int[9, 9]; //Array pour le sudoku.
        int[,] grilleConnus = new int[9, 9];
        public long finCmptr = 0;
        Sudoku_Board SudokuBoard = new Sudoku_Board();
        Sudoku_Board KnownBoard = new Sudoku_Board(); //Track the already known number and position.

        int[,] sudokuExemple = new int[,]
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

        int[,] sudokuExemple2 = new int[,]
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

        //Check.
        public Form1()
        {

            InitializeComponent();
            GUI_Start();
            tb_iteration.Text = "1000"; //default iteration

        }

        // Fonction principale qui résoud le Sudoku.
        private void btn_Resoudre_Click(object sender, EventArgs e)
        {
            bool v = checkForEmptyTextBox();
            if (v == true && tb_iteration.Text != "")
            {
                l_itr.Show();
                finCmptr = Convert.ToInt32(tb_iteration.Text);
                Main(SudokuBoard, finCmptr);
            }
            else if (v == true)
            {
                lbl_warning.Text = "Il manque des chiffres. Il en faut au moins 17.";
                lbl_warning.Show();
            }
            else if (tb_iteration.Text != "")
            {
                lbl_warning.Show();
                lbl_warning.Text = "Veuillez entrer un nombre d'itérations";
            }

        }

        private void btn_Confirmer_Click(object sender, EventArgs e)
        {
            if (checkForEmptyTextBox())
            {
                SudokuBoard.fillSudoku(arrayFromTextBoxSudokuGrid()); //Fill sudoku.
                showSudoku(SudokuBoard); //Affiche le Sudoku visuellement

                //show
                gB_Sudoku.Show();
                btn_Effacer.Show();
                btn_Resoudre.Show();
                btn_recommencer.Show();

                //hide
                gB_Enter.Hide();
                btn_Effacer.Hide();
                btn_Confirmer.Hide();
                btn_Exemple.Hide();

                checkCheck();
            }
            else
            {
                lbl_warning.Text = "Il manque des chiffres. Il en faut au moins 17.";
                lbl_warning.Show();
            }
        }

        private void Main(Sudoku_Board Sudoku, long endCmptr)
        {
            long tic = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            bool endOfloop = false;
            int cmptr = 0;
            int nbToAdd = 0;

            while (endOfloop != true)
            {
                l_itr.Text = String.Concat("Iteration: ", Convert.ToString(cmptr));

                if (!checkB_Freeze.Checked)
                {
                    UpdateSudoku(Sudoku); //Visual update on screen.
                    this.Refresh();
                }

                if (Sudoku.getSquareKnownBoard(Sudoku.getActiveRow(), Sudoku.getActiveCol()) == 0)
                {

                    nbToAdd = findNumber(Sudoku); // Find if number can be put inside square from active square.

                     if (nbToAdd < 10)
                    {
                        Sudoku.setSquare(Sudoku.getActiveRow(), Sudoku.getActiveCol(), nbToAdd);
                        endOfloop = VerifierFin(Sudoku.index(Sudoku.getActiveRow(), Sudoku.getActiveCol()));
                        if (endOfloop == false)
                        {
                            Sudoku.moveFowardNextValidSquare();
                        }
                        
                    }
                    else if (nbToAdd == 10)
                    {
                        Sudoku.setSquare(Sudoku.getActiveRow(), Sudoku.getActiveCol(), 0);
                        Sudoku.moveBackwardNextValidSquare();
                    }
                
                    cmptr++;

                    if (cmptr == endCmptr)
                    {
                        endOfloop = true;
                    }
                }
                else
                {
                    Sudoku.moveFowardNextValidSquare();
                }

            }

            if (cmptr < endCmptr)
            {
                l_itr.Text = String.Concat("Iteration: ", Convert.ToString(cmptr));
                UpdateSudoku(Sudoku); //Mise à jour du Sudoku dans la page visuelle.
                this.Refresh();
                long toc = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                lbl_ms.Text = String.Concat("Solution trouvée en ", Convert.ToString(toc - tic), "millisecondes"); ;
                lbl_ms.Show();
            }
            else
            {
                l_itr.Text = String.Concat("Iteration: ", Convert.ToString(cmptr)) + " Nb maximal d'itérations atteintes";
                UpdateSudoku(Sudoku); //Mise à jour du Sudoku dans la page visuelle.
                this.Refresh();
            }

        }

        //Check.
        public int findNumber(Sudoku_Board Sudoku)
        {
            for (int i = Sudoku.getSquare(Sudoku.getActiveRow(), Sudoku.getActiveCol()); i <= 10; i++)
            {
                if(Sudoku.IsValid(Sudoku.getActiveRow(), Sudoku.getActiveCol(),i))
                {
                    return i;
                }
            }
                return 10;
        }

        static bool VerifierFin(int sudokuIndex) //Fonction pour vérifier si c'est la fin du sudoku. False = Pas fini. True = fini
        {

            if (sudokuIndex == 81)
            {
                return true;
            }
            return false;
        }

        private int[,] arrayFromTextBoxSudokuGrid()
        {
            int row;
            int col;
            int[,] Sudoku= new int[9, 9];

            foreach (TextBox tb in gB_Enter.Controls.OfType<TextBox>())
            {
                if (tb.Name.StartsWith("e"))
                {
                    row = Convert.ToInt16(Convert.ToString(tb.Name[2]));
                    col = Convert.ToInt16(Convert.ToString(tb.Name[1]));

                    if (!string.IsNullOrEmpty(tb.Text))
                    {
                        Sudoku[row, col] = System.Convert.ToInt16(tb.Text);
                    }
                    else
                    {
                        Sudoku[row, col] = 0;
                    }
                }
            }
            return Sudoku;
        }

        private void UpdateSudoku(Sudoku_Board Sudoku)
        {
            int row;
            int col;

            foreach (Label lbl in gB_Sudoku.Controls.OfType<Label>())
            {
                if (lbl.Name.StartsWith("c"))
                {
                    row = Convert.ToInt16(Convert.ToString(lbl.Name[2]));
                    col = Convert.ToInt16(Convert.ToString(lbl.Name[1]));

                    if (!string.IsNullOrEmpty(lbl.Text))
                    {
                        lbl.Text = Convert.ToString(Sudoku.getSquare(row, col));
                    }
                }
            }
        }

        //Enter Sudoku in GUI.
        public void showSudoku(Sudoku_Board Sudoku)
        {
            int row;
            int col;

            foreach (Label lbl in gB_Sudoku.Controls.OfType<Label>())
            {
                if (lbl.Name.StartsWith("c"))
                {
                    col = Convert.ToInt16(Convert.ToString(lbl.Name[1]));
                    row = Convert.ToInt16(Convert.ToString(lbl.Name[2]));

                    if (!string.IsNullOrEmpty(lbl.Text))
                    {
                        lbl.Text = Convert.ToString(Sudoku.getSquare(row,col));
                    }
                }
            }
        }

        private void e00_TextChanged(object sender, EventArgs e) //Vérifie si le caractère mis dans la case est un chiffre.
        {
            TextBox txt = (TextBox)sender;

            if (System.Text.RegularExpressions.Regex.IsMatch(txt.Text, "[^0-9]"))
            {
                txt.Text = "";
            }
        }

        private bool checkForEmptyTextBox() //Vérifie le nb de case avec des chiffres. Selon les maths, il faut au moins 17 chiffres pour avoir une solution unique à un sudoku.
        {
            int itr = 0;

            foreach (Label lbl in gB_Sudoku.Controls.OfType<Label>())
            {
                if (Convert.ToInt16(lbl.Text) != 0)
                {
                    itr++;
                }
            }

            if (itr <= 16)
            {
                return false;
            }
            return true;
        }

        private void quittezToolStripMenuItem_Click_1(object sender, EventArgs e) //Fermer le programme.
        {
            Application.Exit();
        }

        private void btn_Effacer_Click(object sender, EventArgs e) //Efface les informations dans les cases et remet tout à 0.
        {
            EffacerCase();
        }

        private void EffacerCase() //Efface les cases.
        {
            foreach (TextBox tb in gB_Enter.Controls.OfType<TextBox>())
            {
                if (tb.Name.StartsWith("e"))
                {
                    tb.Text = "";
                }
            }
        }

        private void btn_Exemple_Click(object sender, EventArgs e)
        {
            if (tb_iteration.Text != "")
            {

                SudokuBoard.fillSudoku(sudokuExemple);
                showSudoku(SudokuBoard); //Affiche le Sudoku visuellement

                //show
                gB_Sudoku.Show();
                btn_recommencer.Show();
                btn_Resoudre.Show();
                gB_data.Show();
                lbl_itr.Show();
                tb_iteration.Show();
                checkB_Freeze.Show();
                gB_info.Show();

                //hide
                gB_Enter.Hide();
                btn_Confirmer.Hide();
                btn_Effacer.Hide();
                lbl_warning.Hide();
               

            }
            else
            {
                lbl_warning.Show();
                lbl_warning.Text = "Veuillez entrer un nombre d'itérations";
            }

        }

        private void checkCheck() //Check automatique pour ne pas prendre trop de temps et afficher check.
        {
            if (Convert.ToInt64(tb_iteration.Text) > 2000)
            {
                checkB_Freeze.Checked = true;
            }
            checkB_Freeze.Show();
        }

        private void GUI_Start()
        {
            //show
            gB_Enter.Show();
            btn_Confirmer.Show();
            btn_Exemple.Show();
            btn_Effacer.Show();
            gB_data.Show();
            lbl_itr.Show();
            tb_iteration.Show();

            //hide
            checkB_Freeze.Hide();
            gB_Sudoku.Hide();
            btn_Resoudre.Hide();
            btn_recommencer.Hide();
            l_itr.Hide();
            lbl_warning.Hide();
            lbl_ms.Hide();

        }

        private void btn_recommencer_Click(object sender, EventArgs e)
        {
            //show
            gB_Enter.Show();
            btn_Confirmer.Show();
            btn_Exemple.Show();
            btn_Effacer.Show();
            gB_data.Show();
            lbl_itr.Show();
            tb_iteration.Show();

            //hide
            checkB_Freeze.Hide();
            gB_Sudoku.Hide();
            btn_Resoudre.Hide();
            btn_recommencer.Hide();
            l_itr.Hide();
            lbl_warning.Hide();
            lbl_ms.Hide();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}

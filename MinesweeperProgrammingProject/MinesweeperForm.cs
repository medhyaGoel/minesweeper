using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperProgrammingProject
{
    public partial class MinesweeperForm : Form
    {
        //////////////////////////////////////////
        // class constants
        private const int ROWS = 15;
        private const int COLS = 24;
        private const int BUTTON_SIZE = 25;
        private const string BOMB = "\uD83D\uDCA3";
        private const string FLAG = "\uD83D\uDEA9";

        //////////////////////////////////////////
        // fields and properties
        private int Rows { get; set; }
        private int Cols { get; set; }
        private int totalSweepable = ROWS * COLS;
        private int sweepedSquares = 0;
        //make array that stores cell objects (use button click to access corresponding Cell in array)
        private MineSweeperCell[,] cellObjects;

        //////////////////////////////////////////
        // constructor
        public MinesweeperForm()
        {
            InitializeComponent();
            this.Rows = ROWS;
            this.Cols = COLS;
            cellObjects = new MineSweeperCell[Rows, Cols];
        }

        //////////////////////////////////////////
        // event handlers
        private void MinesweeperForm_Load(object sender, EventArgs e)
        {
            // resize the form
            this.Width = BUTTON_SIZE * this.Cols + this.Cols;
            int titleHeight = this.Height - this.ClientRectangle.Height;
            this.Height = BUTTON_SIZE * this.Rows + this.Rows + titleHeight;

            // create the buttons on the form
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    // create a new button control
                    Button b = new Button();
                    // set the button width and height
                    b.Width = BUTTON_SIZE;
                    b.Height = BUTTON_SIZE;
                    // position the button on the form
                    b.Top = i * BUTTON_SIZE;
                    b.Left = j * BUTTON_SIZE;
                    // no text
                    b.Text = String.Empty;
                    // set the button style
                    b.FlatStyle = FlatStyle.Popup;
                    // add a MouseDown event handler
                    b.MouseDown += new MouseEventHandler(MinesweeperForm_MouseDown);
                    // give the button a name in "row_col" format 
                    b.Name = i + "_" + j;
                    // add the button control to the form
                    this.Controls.Add(b);

                    // do other stuff here?
                    cellObjects[i, j] = new MineSweeperCell(b);
                }
            }

            // set up the board
            reset();

            //debug();
        }

        private void MinesweeperForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                Button b = (Button)sender;
                // extract the row and column from the button name
                int index = b.Name.IndexOf("_");
                int i = int.Parse(b.Name.Substring(0, index));
                int j = int.Parse(b.Name.Substring(index + 1));

                // handle mousebuttons left and right differently
                if (e.Button == MouseButtons.Left)
                {
                    // dig the position to reveal the contents 
                    reveal(i, j);
                }
                else
                {
                    // flag the position as a possible mine
                    flag(i, j);
                }
            }
        }

        //////////////////////////////////////////
        // instance methods

        public void endGame(Boolean wonGame)
        {
            // open every button
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    open(i, j);
                }
            }
            if (wonGame)
            {
                status_label.Text = "Game Status: you won!!!";
            }
            else
            {
                status_label.Text = "Game Status: you lost!!!";
            }
        }

        public void reset()
        {
            status_label.Text = "";
            //close all squares, unflag all, and make nearby 0 for everyone (erase all bombs)
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    close(i, j);
                    cellObjects[i,j].isFlagged = false;
                    cellObjects[i, j].nearby = 0;
                }
            }
            //place totalBombs
            int totalBombs = 25;

            Random myObject = new Random();
            for(int i = 0; i<totalBombs; i++)
            {
                int row;
                int col;
                do
                {
                    //generate random row
                    row = myObject.Next(0, Rows);
                    //generate random col
                    col = myObject.Next(0, Cols);
                } while (cellObjects[row, col].nearby == -1);
                cellObjects[row, col].nearby = -1;
            }

            //calculate nearby for each cell
            for (int i = 0; i < this.Rows; i++)
            {
                for (int j = 0; j < this.Cols; j++)
                {
                    //if not a mine
                    if (cellObjects[i, j].nearby != -1)
                    {
                        int counter = 0;
                        //check bordering cells
                        for (int m = -1; m<2; m++){
                            for(int n = -1; n<2; n++){
                                try
                                {
                                    if (cellObjects[i + m, j + n].nearby == -1)
                                        counter++;
                                }
                                catch (Exception) { }
                            }
                        }
                        //set nearby = to counter
                        cellObjects[i, j].nearby = counter;
                    }
                }
            }
        }

        public void close(int row, int col)
        {
            MineSweeperCell myCell = cellObjects[row, col];
            myCell.isOpen = false;
            myCell.myButton.BackColor = Color.Moccasin;
            myCell.myButton.Text = "";
            myCell.myButton.BackgroundImage = null;
        }

        public void debug()
        {
            for(int i = 0; i<Rows; i++)
            {
                for(int j = 0; j<Cols; j++)
                {
                    open(i, j);
                }
            }
        }
        
        public void open(int row, int col)
        {
            try
            {
                MineSweeperCell myCell = cellObjects[row, col];
                myCell.isOpen = true;
                //change appearance based on nearby
                switch (myCell.nearby)
                {
                    case -1:
                        //change to bomb image;
                        myCell.myButton.BackgroundImage = Properties.Resources.bomb;
                        //resize image;
                        myCell.myButton.BackgroundImageLayout = ImageLayout.Stretch;
                        break;
                    case 0:
                        //change to blank square;
                        myCell.myButton.BackColor = Color.Gray;
                        break;
                    case 1:
                        //change to 1 image;
                        myCell.myButton.Text = "1";
                        myCell.myButton.BackColor = Color.Yellow;
                        break;
                    case 2:
                        //change to 2 image;
                        myCell.myButton.Text = "2";
                        //color
                        myCell.myButton.BackColor = Color.Green;
                        break;
                    case 3:
                        //change to 3 image;
                        myCell.myButton.Text = "3";
                        //color
                        myCell.myButton.BackColor = Color.Blue;
                        break;
                    case 4:
                        myCell.myButton.Text = "4";
                        //color
                        myCell.myButton.BackColor = Color.Purple;
                        break;
                    case 5:
                        myCell.myButton.Text = "5";
                        //color
                        myCell.myButton.BackColor = Color.Orange;
                        break;
                    case 6:
                        myCell.myButton.Text = "6";
                        //color
                        myCell.myButton.BackColor = Color.Pink;
                        break;
                    case 7:
                        myCell.myButton.Text = "7";
                        //color
                        myCell.myButton.BackColor = Color.White;
                        break;
                    case 8:
                        myCell.myButton.Text = "8";
                        //color
                        myCell.myButton.BackColor = Color.Red;
                        break;
                }
            }
            catch (Exception) { }
        }

        public void flag(int row, int col)
        {
            MineSweeperCell myCell = cellObjects[row, col];
            if(!myCell.isOpen){
                if (myCell.isFlagged == false)
                {
                    myCell.isFlagged = true;
                    myCell.myButton.Text = FLAG;
                }
                else
                {
                    myCell.isFlagged = false;
                    close(row, col);
                }
            }
        }

        public void reveal(int row, int col)
        {
            if (row < 0 || row >= ROWS || col < 0 || col >= COLS)
                return;
            // try{

                MineSweeperCell myCell = cellObjects[row, col];
                if(myCell.isOpen){
                    return;
                }
                else{
                    open(row, col);
                    //catch exceptions

                    if(myCell.nearby == -1)
                    {
                        endGame(false);
                    }
                    else if(myCell.nearby == 0)
                    {
                        //try revealing all nearby squares
                        for(int m = -1; m<2; m++)
                        {
                            for (int n = -1; n < 2; n++){
                                if(m != 0 || n != 0) {
                                    reveal(row + n, col + m);
                                }
                            }

                        }
                        if (sweepedSquares == totalSweepable)
                            endGame(true);
                    }
                }
          //  }catch(Exception){}
        }

        private void reset_button_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

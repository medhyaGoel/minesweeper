using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinesweeperProgrammingProject
{
    class MineSweeperCell
    {
        public Boolean isFlagged { get; set; }
        public Boolean isOpen { get; set; }
        public int nearby { get; set; }
        public Button myButton;

        //constructor
            //can pass reference to button in constructor
        public MineSweeperCell(Button b)
        {
            myButton = b;
        }
    }
}

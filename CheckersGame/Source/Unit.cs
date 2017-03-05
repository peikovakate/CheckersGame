using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Source
{
    class Unit
    {
        public int Row;
        public int Column;
        public CheckerColor Color;
        
        public virtual List<Cell> getBeatebleCells(Unit[,] checkersGrid)
        {
            Debug.WriteLine("need to be override");
            return new List<Cell>();
        }
    }
}

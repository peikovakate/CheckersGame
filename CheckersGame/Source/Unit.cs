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

        public int TargetDirection
        {
            get { return Color == CheckerColor.White ? -1 : 1; }
        }

        public virtual List<Cell> getBeatebleCells(Unit[,] checkersGrid)
        {
            Debug.WriteLine("need to be override");
            return new List<Cell>();
        }

        public virtual bool CheckMove(Unit[,] checkersGrid, Cell targetCell)
        {
            Debug.WriteLine("need to be override");
            return false;
        }

        public virtual bool IsSimpleMove(Unit[,] checkersGrid, Cell targetCell)
        {
            Debug.WriteLine("need to be override");
            return false;
        }
    }
}

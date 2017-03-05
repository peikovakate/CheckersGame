using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Source
{
    class AI : Player
    {
        public AI(CheckerColor color) : base(color)
        {
        }

        public override void MakeMove(Unit[,] chackersGrid)
        {
            Debug.WriteLine("Asking AI make move");
            foreach (var checker in Checkers)
            {
                List<Cell> cells = checker.getBeatebleCells(chackersGrid);
                if (cells.Count != 0)
                {
                    Cell target;
                    target.row = cells[0].row * 2 - checker.Row;  
                    target.col = cells[0].col * 2 - checker.Column;
                    RaiseSampleEvent(checker.CurrentCell, target);
                    return;
                }
            }
            foreach (var checker in Checkers)
            {
                List<Cell> cells = checker.PossibleCellsToGo(chackersGrid);
                if(cells.Count != 0)
                {
                    RaiseSampleEvent(checker.CurrentCell, cells[0]);
                    return;
                }
            }
        }            
    }

}


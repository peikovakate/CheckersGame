using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Source
{
    class Checker : Unit
    {
        private int[,] beatebleDirections = { { -1, -1 }, { -1, 1 }, { 1, 1 }, { 1, -1 } };

        public override List<Cell> getBeatebleCells(Unit[,] checkersGrid)
        {
            List<Cell> cells = new List<Cell>();
            for (int i = 0; i < beatebleDirections.Length; i++)
            {
                int targetRow = Row + beatebleDirections[i, 0];
                int targetCol = Column + beatebleDirections[i, 1];
                if (checkersGrid[targetRow, targetCol] != null &&
                    checkersGrid[targetRow, targetCol].Color != Color &&
                    (targetRow != 0 && targetRow != 7 && targetCol != 0 && targetCol != 7)
                    )
                {
                    Cell target;
                    target.row = targetRow;
                    target.col = targetCol;
                    cells.Add(target);
                }
            }
            return cells;
        }
    }
}

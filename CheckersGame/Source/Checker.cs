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
            for (int i = 0; i < beatebleDirections.Length / beatebleDirections.Rank; i++)
            {
                int targetRow = Row + beatebleDirections[i, 0];
                int targetCol = Column + beatebleDirections[i, 1];
                if ((targetRow > 0 && targetRow < 7 && targetCol > 0 && targetCol < 7) &&
                    checkersGrid[targetRow, targetCol] != null &&
                    checkersGrid[targetRow, targetCol].Color != Color &&
                    checkersGrid[targetRow + beatebleDirections[i, 0], targetCol + beatebleDirections[i, 1]]==null
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

        //check for right turn
        //check for right direction
        //this cell should be empty
        //checker can move one cell by diagonal
        //always beat enemy's checker if it's possible
        //TO DO:

        //check for borders
        //check for king
        //check for ather possible beatings

        public override bool CheckMove(Unit[,] checkersGrid, Cell targetCell)
        {
            if (checkersGrid[targetCell.row, targetCell.col] != null)
            {
                return false;
            }

            if (!(Math.Abs(targetCell.row - Row) == Math.Abs(targetCell.col - Column)))
            {
                return false;
            }

            if (Math.Abs(targetCell.row - Row) == 1)
            {
                if (!(targetCell.row - Row == TargetDirection))
                {
                    return false;
                }
            } //if checker probably trying beat other checker
            else if (Math.Abs(targetCell.row - Row) == 2)
            {
                int targetRow = (targetCell.row + Row) / 2;
                int targetCol = (targetCell.col + Column) / 2;

                if (checkersGrid[targetRow, targetCol] == null ||
                    checkersGrid[targetRow, targetCol].Color == Color)
                {
                    return false;
                }

            }
            else
            {
                return false;
            }

            return true;
        }

        public override bool IsSimpleMove(Unit[,] checkersGrid, Cell targetCell)
        {
            if (Math.Abs(targetCell.row - Row) == 1)
            {
                if (targetCell.row - Row == TargetDirection)
                {
                    return true;
                }else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}

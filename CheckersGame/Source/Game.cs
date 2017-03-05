using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace CheckersGame.Source
{
    public enum CheckerColor { White, Black };

    //public class CheckerUnit
    //{
    //    public int row;
    //    public int column;
    //    public CheckerColor color;
    //    public bool isKing;
    //}

    public struct Cell
    {
        public int row;
        public int col;
    }

    public struct CheckerTransaction
    {
        public int startRow;
        public int startCol;
        public int targetRow; // if targetRow is -1 then it should be removed from grid
        public int targetCol;
    }

    class Game
    {
        private Player[] players;
        private Unit[,] checkersGrid;
        private List<CheckerTransaction> transactions;

        private int turn;

        private void nextTurn()
        {
            turn = (turn + 1) % 2;
        }

        private int enemy()
        {
            return (turn + 1) % 2; 
        }

        public string Turn
        {
            get
            {
                if(turn == 0)
                {
                    return "White GO!";
                }else
                {
                    return "Black Go!";
                }
            }
        }


        public Game()
        {
            players = new Player[2];
            players[0] = new Player(CheckerColor.White);
            players[1] = new Player(CheckerColor.Black);

            checkersGrid = new Unit[8,8];

            foreach (Unit checker in players[0].Checkers)
            {
                checkersGrid[checker.Row, checker.Column] = checker;
            }

            foreach (Unit checker in players[1].Checkers)
            {
                checkersGrid[checker.Row, checker.Column] = checker;
            }

            transactions = new List<CheckerTransaction>();

        }

        public List<Unit> GetCheckers()
        {
            List<Unit> checkers = new List<Unit>();
            checkers.AddRange(players[0].Checkers);
            checkers.AddRange(players[1].Checkers);
            return checkers;
        }

        public List<CheckerTransaction> GetMoves()
        {
            return transactions;
        }

        public void PassTransaction(CheckerTransaction transaction)
        {
            //Clear previous transactions
            transactions.Clear();

            Unit checker = checkersGrid[transaction.startRow, transaction.startCol];
            if (checker != null)
            {
                if(CheckMove(checker, transaction.targetRow, transaction.targetCol))
                {
                    checker.Row = transaction.targetRow;
                    checker.Column = transaction.targetCol;
                    checkersGrid[transaction.startRow, transaction.startCol] = null;
                    checkersGrid[transaction.targetRow, transaction.targetCol] = checker;
                    transactions.Add(transaction);
                    //if there is no continue
                    nextTurn();
                }
            }else
            {
                Debug.WriteLine("CheckerUnit was not found in that cell");
            }

           
        }


        //check for right turn
        //check for right direction
        //this cell should be empty
        //checker can move one cell by diagonal
        //TO DO:
        //always beat enemy's checker if it's possible
        //check for borders
        //check for king
        //check for ather possible beatings

        private int[,] beatebleDirections = { { -1, -1}, { -1, 1}, { 1, 1 }, { 1, -1 } };

        private List<Cell> getBeatebleCells(Unit checker)
        {
            List<Cell> cells = new List<Cell>();
            for(int i=0; i<beatebleDirections.Length; i++)
            {
                int targetRow = checker.Row + beatebleDirections[i, 0];
                int targetCol = checker.Column + beatebleDirections[i, 1];
                if (checkersGrid[targetRow, targetCol]!=null &&
                    (int)checkersGrid[targetRow, targetCol].Color != turn &&
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

        private bool CheckMove(Unit checker, int row, int col)
        {
            if (turn != (int)checker.Color)
            {
                return false;
            }
            if (checkersGrid[row, col]!=null)
            {
                return false;
            }

            if(!(Math.Abs(row - checker.Row) == Math.Abs(col - checker.Column)))
            {
                return false;
            }

            if (Math.Abs(row - checker.Row) == 1)
            {
                if (!(row - checker.Row == players[turn].TargetDirection))
                {
                    return false;
                }
            }else if(Math.Abs(row - checker.Row) == 2)
            {
                int targetRow = (row + checker.Row) / 2;
                int targetCol = (col + checker.Column) / 2;

                if (checkersGrid[targetRow, targetCol] != null &&
                    (int)checkersGrid[targetRow, targetCol].Color != turn)
                {
                    
                    players[enemy()].Checkers.Remove(checkersGrid[targetRow, targetCol]);
                    checkersGrid[targetRow, targetCol] = null;
                    CheckerTransaction transaction;
                    transaction.startRow = targetRow;
                    transaction.startCol = targetCol;
                    transaction.targetRow = transaction.targetCol = -1;
                    transactions.Add(transaction);
                    players[turn].Score++;
                }
                else
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

    }
}

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

    //public struct CheckerTransaction
    //{
    //    public int startRow;
    //    public int startCol;
    //    public int targetRow; // if targetRow is -1 then it should be removed from grid
    //    public int targetCol;
    //}

    public struct CheckerTransaction
    {
        public Cell startCell;
        public Cell targetCell;// if targetRow is -1 then it should be removed from grid
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

            Unit checker = checkersGrid[transaction.startCell.row, transaction.startCell.col];
            if (checker != null)
            {
                if(CheckMove(checker, transaction.targetCell))
                {
                    checker.Row = transaction.targetCell.row;
                    checker.Column = transaction.targetCell.col;
                    checkersGrid[transaction.startCell.row, transaction.startCell.col] = null;
                    checkersGrid[transaction.targetCell.row, transaction.targetCell.col] = checker;
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

        

        private bool CheckMove(Unit checker, Cell targetCell)
        {
            if (turn != (int)checker.Color)
            {
                return false;
            }
            if (checkersGrid[targetCell.row, targetCell.col] !=null)
            {
                return false;
            }

            if(!(Math.Abs(targetCell.row - checker.Row) == Math.Abs(targetCell.col - checker.Column)))
            {
                return false;
            }

            if (Math.Abs(targetCell.row - checker.Row) == 1)
            {
                if (!(targetCell.row - checker.Row == players[turn].TargetDirection))
                {
                    return false;
                }
            }else if(Math.Abs(targetCell.row - checker.Row) == 2)
            {
                int targetRow = (targetCell.row + checker.Row) / 2;
                int targetCol = (targetCell.col + checker.Column) / 2;

                if (checkersGrid[targetRow, targetCol] != null &&
                    (int)checkersGrid[targetRow, targetCol].Color != turn)
                {
                    
                    players[enemy()].Checkers.Remove(checkersGrid[targetRow, targetCol]);
                    checkersGrid[targetRow, targetCol] = null;
                    CheckerTransaction transaction;
                    transaction.startCell.row = targetRow;
                    transaction.startCell.col = targetCol;
                    transaction.targetCell.row = transaction.targetCell.col = -1;
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

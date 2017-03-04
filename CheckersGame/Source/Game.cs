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

    public class CheckerUnit
    {
        public int row;
        public int column;
        public CheckerColor color;
        public bool isKing;
    }

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
        private CheckerUnit[,] checkersGrid;
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

            checkersGrid = new CheckerUnit[8,8];

            foreach (CheckerUnit checker in players[0].Checkers)
            {
                checkersGrid[checker.row, checker.column] = checker;
            }

            foreach (CheckerUnit checker in players[1].Checkers)
            {
                checkersGrid[checker.row, checker.column] = checker;
            }

            transactions = new List<CheckerTransaction>();

        }

        public List<CheckerUnit> GetCheckers()
        {
            List<CheckerUnit> checkers = new List<CheckerUnit>();
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

            CheckerUnit checker = checkersGrid[transaction.startRow, transaction.startCol];
            if (checker != null)
            {
                if(CheckMove(checker, transaction.targetRow, transaction.targetCol))
                {
                    checker.row = transaction.targetRow;
                    checker.column = transaction.targetCol;
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

        private List<Cell> getBeatebleCells(CheckerUnit checker)
        {
            List<Cell> cells = new List<Cell>();
            for(int i=0; i<beatebleDirections.Length; i++)
            {
                int targetRow = checker.row + beatebleDirections[i, 0];
                int targetCol = checker.column + beatebleDirections[i, 1];
                if (checkersGrid[targetRow, targetCol]!=null &&
                    (int)checkersGrid[targetRow, targetCol].color != turn &&
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

        private bool CheckMove(CheckerUnit checker, int row, int col)
        {
            if (turn != (int)checker.color)
            {
                return false;
            }
            if (checkersGrid[row, col]!=null)
            {
                return false;
            }

            if(!(Math.Abs(row - checker.row)== Math.Abs(col - checker.column)))
            {
                return false;
            }

            if (Math.Abs(row - checker.row) == 1)
            {
                if (!(row - checker.row == players[turn].TargetDirection))
                {
                    return false;
                }
            }else if(Math.Abs(row - checker.row) == 2)
            {
                int targetRow = (row + checker.row) / 2;
                int targetCol = (col + checker.column) / 2;

                if (checkersGrid[targetRow, targetCol] != null &&
                    (int)checkersGrid[targetRow, targetCol].color != turn)
                {
                    
                    players[enemy()].Checkers.Remove(checkersGrid[targetRow, targetCol]);
                    checkersGrid[targetRow, targetCol] = null;
                    CheckerTransaction transaction;
                    transaction.startRow = targetRow;
                    transaction.startCol = targetCol;
                    transaction.targetRow = transaction.targetCol = -1;
                    transactions.Add(transaction);
                }else
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

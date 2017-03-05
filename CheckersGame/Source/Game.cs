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
        public Player[] Players { get; private set; }
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
            Players = new Player[2];
            Players[0] = new AI(CheckerColor.White);
            Players[1] = new AI(CheckerColor.Black);

            checkersGrid = new Unit[8,8];

            foreach (Unit checker in Players[0].Checkers)
            {
                checkersGrid[checker.Row, checker.Column] = checker;
            }

            foreach (Unit checker in Players[1].Checkers)
            {
                checkersGrid[checker.Row, checker.Column] = checker;
            }

            transactions = new List<CheckerTransaction>();
            
        }

        public List<Unit> GetCheckers()
        {
            List<Unit> checkers = new List<Unit>();
            checkers.AddRange(Players[0].Checkers);
            checkers.AddRange(Players[1].Checkers);
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
                    //there is no beateble checker to active checker or checker did just simple move
                    if (checker.getBeatebleCells(checkersGrid).Count == 0 || transactions.Count==1)
                    {
                        nextTurn();
                    }
                    
                }
            }else
            {
                Debug.WriteLine("CheckerUnit was not found in that cell");
            }

           
        }

        public void AskToMove()
        {
            Players[turn].MakeMove(checkersGrid);
        }

        private bool CheckIfPlayerCanBeat(Player player)
        {
            foreach (var checker in player.Checkers)
            {
                if (checker.getBeatebleCells(checkersGrid).Count != 0)
                {
                    return true;
                }
            }
            return false;
        }

        private bool CheckMove(Unit checker, Cell targetCell)
        {
            if (turn != (int)checker.Color)
            {
                return false;
            }

            if(checker.CheckMove(checkersGrid, targetCell))
            {
                //TO DO
                //for king!!!!
                if (!checker.IsSimpleMove(checkersGrid, targetCell))
                {
                    int targetRow = (targetCell.row + checker.Row) / 2;
                    int targetCol = (targetCell.col + checker.Column) / 2;

                    Players[enemy()].Checkers.Remove(checkersGrid[targetRow, targetCol]);
                    checkersGrid[targetRow, targetCol] = null;
                    CheckerTransaction transaction;
                    transaction.startCell.row = targetRow;
                    transaction.startCell.col = targetCol;
                    transaction.targetCell.row = transaction.targetCell.col = -1;
                    transactions.Add(transaction);
                    Players[turn].Score++;
                }else
                {
                    //player can beat some 
                    //TO DO:
                    //check for right checker
                    if (CheckIfPlayerCanBeat(Players[turn]))
                    {
                        return false;
                    }
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

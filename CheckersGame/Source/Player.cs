using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Source
{
    public class MoveEventArgs
    {
        public Cell StartCell { get; private set; }
        public Cell TargetCell { get; private set; }
        public MoveEventArgs(Cell start, Cell target)
        {
            StartCell = start;
            TargetCell = target;
        }
    }

    class Player
    {
        public delegate void MoveEventHandler(object sender, MoveEventArgs e);

        //Событие OnCount c типом делегата MethodContainer.
        public event MoveEventHandler onMove;

        protected virtual void RaiseSampleEvent(Cell s, Cell t)
        {
            if (onMove != null)
                onMove(this, new MoveEventArgs(s, t));
        }

        private List<Unit> checkers;

        public int Score;
        
        //where checker turns to king
        public int TargetRow
        {
            get { return color == CheckerColor.White ? 0 : 7; }
        }

        public int TargetDirection
        {
            get { return color == CheckerColor.White ? -1 : 1; }
        }

        public List<Unit> Checkers
        {
            get
            {
                return checkers;
            }
        }

        CheckerColor color;

        public Player(CheckerColor color)
        {
            checkers = new List<Unit>();
            this.color = color;
            Score = 0;
            initCheckers();
        }

        //set positions for checkers
        private void initCheckers()
        {
            int startRow;
            if (color == CheckerColor.Black)
            {
                startRow = 0;
            }
            else
            {
                startRow = 5;  
            }
            for (int i = 0; i < 12; i++)
            {
                Unit checker = new Checker();
                checker.Color = color;
                checker.Row = startRow + i * 2 / 8;
                checker.Column = (1 + i * 2 + checker.Row % 2) % 8;
                checkers.Add(checker);
            }
        }

        public virtual void MakeMove(Unit[,] chackersGrid)
        {

        }

    }
}

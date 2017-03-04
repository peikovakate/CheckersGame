using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Source
{
    class Player
    {
        private List<CheckerUnit> checkers;

        //where checker turns to king
        public int TargetRow
        {
            get { return color == CheckerColor.White ? 0 : 7; }
        }

        public int TargetDirection
        {
            get { return color == CheckerColor.White ? -1 : 1; }
        }

        public List<CheckerUnit> Checkers
        {
            get
            {
                return checkers;
            }
        }

        CheckerColor color;

        public Player(CheckerColor color)
        {
            checkers = new List<CheckerUnit>();
            this.color = color;
            initCheckers();
        }

        //set positions for checkers
        private void initCheckers()
        {
            int startRow, startColumn;
            if (color == CheckerColor.Black)
            {
                startRow = 0;
                startColumn = 1;
            }
            else
            {
                startRow = 5;
                startColumn = 0;
            }
            for (int i = 0; i < 12; i++)
            {
                CheckerUnit checker = new CheckerUnit();
                checker.color = color;
                checker.row = startRow + i * 2 / 8;
                checker.column = (startColumn + i * 2 + checker.row % 2) % 8;
                checker.isKing = false;
                checkers.Add(checker);

            }
        }

    }
}

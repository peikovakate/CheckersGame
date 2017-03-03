using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Source
{
    class Player
    {
        List<CheckerUnit> checkers;
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
                CheckerUnit checker;
                checker.color = color;
                checker.row = startRow + i * 2 / 8;
                checker.column = (startColumn + i * 2) % 8;
                checker.isKing = false;
                checkers.Add(checker);

            }
        }

    }
}

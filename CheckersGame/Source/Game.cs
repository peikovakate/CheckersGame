using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckersGame.Source
{
    public enum CheckerColor { White, Black };

    public struct CheckerUnit
    {
        public int row;
        public int column;
        public CheckerColor color;
        public bool isKing;
    }

    class Game
    {
        public Game()
        {
            Player first = new Player(CheckerColor.White);
            Player second = new Player(CheckerColor.Black);
        }

        public void ClickOnTheGrid(int row, int col)
        {

        }

    }
}

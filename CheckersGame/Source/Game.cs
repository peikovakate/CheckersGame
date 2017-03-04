using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

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
        private Player[] players;

        public Game()
        {
            players = new Player[2];
            players[0] = new Player(CheckerColor.White);
            players[1] = new Player(CheckerColor.Black);
        }

        public List<CheckerUnit> GetCheckers()
        {
            List<CheckerUnit> checkers = new List<CheckerUnit>();
            checkers.AddRange(players[0].Checkers);
            checkers.AddRange(players[1].Checkers);
            return checkers;
        }

    }
}

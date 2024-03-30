using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    internal class Game
    {
        private Board board;
        private Player player1;
        private Player player2;
        public Game() 
        {
            board.Initialize();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    internal class Game : INotifyPropertyChanged
    {
        private Board board;
        public Board Board
        {
            get { return board; }
            set
            {
                if (board != value)
                {
                    board = value;
                    NotifyPropertyChanged(nameof(Board));
                }
            }
        }
        private Player player1;
        public Player Player1
        {
            get { return player1; }
            set
            {
                if (player1 != value)
                {
                    player1 = value;
                    NotifyPropertyChanged(nameof(Player1));
                }
            }
        }

        private Player player2;
        public Player Player2
        {
            get { return player2; }
            set
            {
                if (player2 != value)
                {
                    player2 = value;
                    NotifyPropertyChanged(nameof(Player2));
                }
            }
        }
        public Game()
        {
            Board = new Board();
            Player1 = new Player();
            Player2 = new Player();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
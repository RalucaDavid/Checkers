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
        private Board _board;
        public Board Board
        {
            get { return _board; }
            set
            {
                if (_board != value)
                {
                    _board = value;
                    NotifyPropertyChanged(nameof(Board));
                }
            }
        }
        private Player _player1;
        public Player Player1
        {
            get { return _player1; }
            set
            {
                if (_player1 != value)
                {
                    _player1 = value;
                    NotifyPropertyChanged(nameof(Player1));
                }
            }
        }

        private Player _player2;
        public Player Player2
        {
            get { return _player2; }
            set
            {
                if (_player2 != value)
                {
                    _player2 = value;
                    NotifyPropertyChanged(nameof(Player2));
                }
            }
        }
        public Game()
        {
            Board = new Board();
            Player1 = new Player();
            Player2 = new Player();

            Board.Initialize();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
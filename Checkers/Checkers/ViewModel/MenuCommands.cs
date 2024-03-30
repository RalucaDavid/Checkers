using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Checkers.ViewModel
{
    class MenuCommands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ICommand newGame;
        private ICommand closeGame;
        private ICommand saveGame;
        private ICommand openGame;
        private ICommand showStatistics;
        private ICommand about;
        public MenuCommands()
        {
            /*empty*/
        }
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        public void ShowAbout(object parameter)
        {
            MessageBox.Show("Name: David Andreea Raluca\n" +
                "Group: 10LF221\n" +
                "Institutional email address: raluca.david@student.unitbv.ro\n" +
                "\r\nThe game of checkers, also known as draughts, is a strategy and " +
                "tactics game played by two players on an 8x8 board called a checkerboard. " +
                "Each player places their pieces on the dark squares of their side of the board. " +
                "The pieces are typically of two colors, such as red and white.\r\n\r\nThe objective " +
                "of the game is to capture or block the opponent's pieces, leaving them with no valid moves" +
                " or capturing all of their pieces. Moves are made diagonally forward, and pieces can jump over " +
                "opponent's pieces to capture them. Once a piece reaches the opposite end of the board, it can be " +
                "promoted to a \"king\", granting it the ability to move both forward and backward diagonally.\r\n\r\n" +
                "Checkers involves strategy in the placement and movement of pieces, as well as anticipating the opponent's " +
                "moves. It's a game that combines elements of strategic and tactical calculation, providing an engaging experience for players of all ages.");
        }
        public ICommand About
        {
            get
            {
                if (about == null)
                {
                    about = new RelayCommand(ShowAbout);
                }
                return about;
            }
        }
    }
}

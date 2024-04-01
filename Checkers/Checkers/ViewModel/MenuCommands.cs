using Checkers.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Checkers.ViewModel
{
    class MenuCommands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Game game;
        private List<Tuple<int, int>> validMoves;
        private Piece currentPiece = null;
        private ICommand newGame;
        private ICommand closeGame;
        private ICommand saveGame;
        private ICommand openGame;
        private ICommand showStatistics;
        private ICommand about;
        private ICommand cellClicked;
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
        public void CheckersNewGame(object parameter)
        {
            game = new Game();
            Grid grid = parameter as Grid;
            if ((grid != null) && (game != null) && (game.Board != null) && (game.Board.Pieces != null))
            {
                for (int index=1;index<=8;index++)
                {
                    for(int jndex=1;jndex<=8;jndex++)
                    {
                        var image = new Image();
                        string imagePath = GetImagePath(PieceType.None, ColorType.None);
                        image.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                        Grid.SetRow(image, index);
                        Grid.SetColumn(image, jndex);
                        image.MouseLeftButtonDown += ClickMouseLeftButtonDown;
                        grid.Children.Add(image);
                    }
                }
                foreach (var piece in game.Board.Pieces)
                {
                    var image = new Image();
                    string imagePath = GetImagePath(piece.Type, piece.Color);
                    image.Source = new BitmapImage(new Uri(imagePath, UriKind.Absolute));
                    Grid.SetRow(image, piece.Coordinates.Item1);
                    Grid.SetColumn(image, piece.Coordinates.Item2);
                    image.MouseLeftButtonDown += ClickMouseLeftButtonDown;
                    grid.Children.Add(image);
                }
            }
        }
        public Piece FindPieceByCoordonates(int row, int column)
        {
            foreach (var piece in game.Board.Pieces)
            {
                if(piece.Coordinates.Item1==row && piece.Coordinates.Item2==column)
                {
                    return piece; 
                }
            }
            return null;
        }

        public List<Tuple<int,int>> ValidMoves(int row, int column)
        {
            List < Tuple<int, int> > possibleMoves = new List<Tuple<int, int>>();
            if(FindPieceByCoordonates(row+1,column-1)==null)
            {
                possibleMoves.Add(Tuple.Create(row + 1, column - 1));
            }
            if (FindPieceByCoordonates(row+1,column+1)==null)
            {
                possibleMoves.Add(Tuple.Create(row + 1, column + 1));
            }
            return possibleMoves;
        }
        private void ClickMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = sender as Image;
            if (clickedImage != null)
            {
                int row = Grid.GetRow(clickedImage);
                int column = Grid.GetColumn(clickedImage);
                currentPiece = FindPieceByCoordonates(row, column);
                if (currentPiece != null)
                {
                    validMoves = ValidMoves(row, column);
                    MessageBox.Show("cf");
                }
            }
        }
        private string GetImagePath(PieceType type, ColorType color)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\..\\Resources\\Images"));
            if ((color == ColorType.None) && (type == PieceType.None))
            {
                return Path.Combine(directoryPath, $"None.png");
            }
            else
            {
                string colorString = color == ColorType.White ? "White" : "Red";
                string typeString = type == PieceType.Simple ? "Simple" : "King";
                return Path.Combine(directoryPath, $"Piece{colorString}{typeString}.png");
            }
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
        public ICommand NewGame
        {
            get
            {
                if (newGame == null)
                    newGame = new RelayCommand(CheckersNewGame);
                return newGame;
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
    }
}

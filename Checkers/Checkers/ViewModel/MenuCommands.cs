using Checkers.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
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
        public Game Game
        {
            get { return game; }
            set
            {
                if (game != value)
                {
                    game = value;
                    OnPropertyChanged(nameof(Game));
                    OnPropertyChanged(nameof(Board));
                }
            }
        }
        private List<Tuple<int, int>> validMoves;
        private ICommand newGame;
        private ICommand closeGame;
        private ICommand saveGame;
        private ICommand openGame;
        private ICommand showStatistics;
        private ICommand about;
        private ICommand pieceClicked;
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
        public void InitializeBoard()
        {
            for(int col=0;col<8;col++)
            {
                ObservableCollection<Piece> colPices = new ObservableCollection<Piece>();
                for(int row=0;row<8;row++)
                {
                    Piece piece;
                    if (((row==0)||(row==2))&&(col%2==1))
                    {
                        piece = new Piece(PieceType.Simple,ColorType.White, GetImagePath(PieceType.Simple,ColorType.White,row,col));
                    }
                    else if((row==1)&&(col%2==0))
                    {
                        piece = new Piece(PieceType.Simple, ColorType.White, GetImagePath(PieceType.Simple, ColorType.White,row,col));
                    }
                    else if(((row==5)||(row==7))&&(col%2==0))
                    {
                        piece = new Piece(PieceType.Simple, ColorType.Red, GetImagePath(PieceType.Simple, ColorType.Red, row, col));
                    }
                    else if((row==6)&&(col%2==1))
                    {
                        piece = new Piece(PieceType.Simple, ColorType.Red, GetImagePath(PieceType.Simple, ColorType.Red,row,col));
                    }
                    else
                    {
                        piece = new Piece(PieceType.None, ColorType.None, GetImagePath(PieceType.None, ColorType.None, row, col));
                    }
                    colPices.Add(piece);
                }
                game.Board.Pieces.Add(colPices);
            }
            OnPropertyChanged("Game");
        }
        public void CheckersNewGame(object parameter)
        {
            game = new Game();
            InitializeBoard();
        }
        public Piece FindPieceByCoordonates(int row, int column)
        {
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
        private void ExecutePieceClicked(object parameter)
        {
            if (parameter is Piece piece)
            {
                MessageBox.Show($"Piece clicked: {piece.Type}, Color: {piece.Color}");
            }
        }
        private string GetImagePath(PieceType type, ColorType color, int row, int col)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\..\\Resources\\Images"));
            if ((color == ColorType.None) && (type == PieceType.None))
            {
                if(col%2==row%2)
                    return Path.Combine(directoryPath, $"WhiteSquare.png");
                return Path.Combine(directoryPath, $"BlackSquare.png");
            }
            else
            {
                string colorString = color == ColorType.White ? "White" : "Red";
                string typeString = type == PieceType.Simple ? "Simple" : "King";
                return Path.Combine(directoryPath, $"Piece{colorString}{typeString}.png");
            }
        }
        private void ClickOnBoard(object parameter)
        {
            MessageBox.Show("Sal");
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
        public ICommand PieceClicked
        {
            get
            {
                if (pieceClicked== null)
                    pieceClicked = new RelayCommand(ClickOnBoard);
                return pieceClicked;
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

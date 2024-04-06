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
        public event MouseButtonEventHandler MouseDownOnBoard;
        private Game game;
        private Player currentPlayer;
        private List<Tuple<int, int>> validMoves = new List<Tuple<int, int>>();
        private Piece currentPiece;
        private string round;
        public string Round
        {
            get { return round; }
            set
            {
                if (round != value)
                {
                    round = value;
                    OnPropertyChanged(nameof(Round));
                }
            }
        }
        private string numberPiecesRed;
        public string NumberPiecesRed
        {
            get { return numberPiecesRed; }
            set
            {
                if (numberPiecesRed != value)
                {
                    numberPiecesRed = value;
                    OnPropertyChanged(nameof(NumberPiecesRed));
                }
            }
        }

        private string numberPiecesWhite;
        public string NumberPiecesWhite
        {
            get { return numberPiecesWhite; }
            set
            {
                if (numberPiecesWhite != value)
                {
                    numberPiecesWhite = value;
                    OnPropertyChanged(nameof(NumberPiecesWhite));
                }
            }
        }
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
        public void OnMouseDownOnBoard(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Image clickedImage = sender as Image;
                if (clickedImage != null)
                {
                    Piece clickedPiece = clickedImage.DataContext as Piece;
                    if ((clickedPiece.Type != PieceType.None) && (clickedPiece.Color != ColorType.None) && (clickedPiece.Color == currentPlayer.Color))
                    {
                        currentPiece = clickedPiece;
                        ClearValidMoves();
                        validMoves.Clear();
                        ValidMoves(clickedPiece.Coordonates.Item1, clickedPiece.Coordonates.Item2);
                        if (validMoves.Count > 0)
                        {
                            ShowValidMoves();
                            OnPropertyChanged("Game");
                        }
                    }
                }
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Image clickedImage = sender as Image;
                if (clickedImage != null)
                {
                    string basePath = AppDomain.CurrentDomain.BaseDirectory;
                    string directoryPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\..\\Resources\\Images"));
                    Piece clickedPiece = clickedImage.DataContext as Piece;
                    if (clickedPiece.Type == PieceType.None && clickedPiece.ImagePath == Path.Combine(directoryPath, $"PossibleMove.png"))
                    {
                        ClearValidMoves();
                        validMoves.Clear();
                        if (clickedPiece.Coordonates.Item1 == 7 || clickedPiece.Coordonates.Item1 == 0 && currentPiece.Type == PieceType.Simple)
                        {
                            currentPiece.Type = PieceType.King;
                            if (currentPiece.Color == ColorType.Red)
                            {
                                currentPiece.ImagePath = Path.Combine(directoryPath, $"PieceRedKing.png");
                            }
                            if (currentPiece.Color == ColorType.White)
                            {
                                currentPiece.ImagePath = Path.Combine(directoryPath, $"PieceWhiteKing.png");
                            }
                        }
                        if (Math.Abs(currentPiece.Coordonates.Item1 - clickedPiece.Coordonates.Item1) == 2)
                        {
                            if (currentPiece.Coordonates.Item1 < clickedPiece.Coordonates.Item1 && currentPiece.Coordonates.Item2 < clickedPiece.Coordonates.Item2)
                            {
                                game.Board.Pieces[currentPiece.Coordonates.Item1 + 1][currentPiece.Coordonates.Item2 + 1] =
                                    new Piece(PieceType.None, ColorType.None, Path.Combine(directoryPath, $"BlackSquare.png"), Tuple.Create(currentPiece.Coordonates.Item1 + 1, currentPiece.Coordonates.Item2 + 1));
                            }
                            if (currentPiece.Coordonates.Item1 < clickedPiece.Coordonates.Item1 && currentPiece.Coordonates.Item2 > clickedPiece.Coordonates.Item2)
                            {
                                game.Board.Pieces[currentPiece.Coordonates.Item1 + 1][currentPiece.Coordonates.Item2 - 1] =
                                    new Piece(PieceType.None, ColorType.None, Path.Combine(directoryPath, $"BlackSquare.png"), Tuple.Create(currentPiece.Coordonates.Item1 + 1, currentPiece.Coordonates.Item2 - 1));
                            }
                            if (currentPiece.Coordonates.Item1 > clickedPiece.Coordonates.Item1 && currentPiece.Coordonates.Item2 < clickedPiece.Coordonates.Item2)
                            {
                                game.Board.Pieces[currentPiece.Coordonates.Item1 - 1][currentPiece.Coordonates.Item2 + 1] =
                                    new Piece(PieceType.None, ColorType.None, Path.Combine(directoryPath, $"BlackSquare.png"), Tuple.Create(currentPiece.Coordonates.Item1 - 1, currentPiece.Coordonates.Item2 + 1));
                            }
                            if (currentPiece.Coordonates.Item1 > clickedPiece.Coordonates.Item1 && currentPiece.Coordonates.Item2 > clickedPiece.Coordonates.Item2)
                            {
                                game.Board.Pieces[currentPiece.Coordonates.Item1 - 1][currentPiece.Coordonates.Item2 - 1] =
                                    new Piece(PieceType.None, ColorType.None, Path.Combine(directoryPath, $"BlackSquare.png"), Tuple.Create(currentPiece.Coordonates.Item1 - 1, currentPiece.Coordonates.Item2 - 1));
                            }
                        }
                        game.Board.Pieces[currentPiece.Coordonates.Item1][currentPiece.Coordonates.Item2] =
                            new Piece(PieceType.None, ColorType.None, Path.Combine(directoryPath, $"BlackSquare.png"), Tuple.Create(currentPiece.Coordonates.Item1, currentPiece.Coordonates.Item2));
                        game.Board.Pieces[clickedPiece.Coordonates.Item1][clickedPiece.Coordonates.Item2] =
                            new Piece(currentPiece.Type, currentPiece.Color, currentPiece.ImagePath, Tuple.Create(clickedPiece.Coordonates.Item1, clickedPiece.Coordonates.Item2));
                        if(currentPlayer.Color == ColorType.Red)
                        {
                            currentPlayer = game.Player2;
                        }
                        else
                        {
                            currentPlayer = game.Player1;
                        }
                    }
                }
                Round = currentPlayer.Color.ToString() + " player's turn.";
                CountPieces();
                if (NumberPiecesRed == "0")
                {
                    MessageBox.Show("White player won the game!");
                }
                else if (NumberPiecesWhite == "0")
                {
                    MessageBox.Show("Red player won the game!");
                }
            }
        }
        private void CountPieces()
        {
            NumberPiecesRed = game.Board.Pieces.SelectMany(x => x).Count(x => x.Color == ColorType.Red).ToString();
            NumberPiecesWhite = game.Board.Pieces.SelectMany(x => x).Count(x => x.Color == ColorType.White).ToString();
        }
        private void ShowValidMoves()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\..\\Resources\\Images"));
            foreach (var move in validMoves)
            {
                game.Board.Pieces[move.Item1][move.Item2].ImagePath = Path.Combine(directoryPath, $"PossibleMove.png");
            }
        }
        private void ClearValidMoves()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\..\\Resources\\Images"));
            foreach (var move in validMoves)
            {
                game.Board.Pieces[move.Item1][move.Item2].ImagePath = Path.Combine(directoryPath, $"BlackSquare.png");
            }
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
            for (int row = 0; row < 8; row++)
            {
                ObservableCollection<Piece> rowPices = new ObservableCollection<Piece>();
                for (int col = 0; col < 8; col++)
                {
                    Piece piece;
                    if (((row == 0) || (row == 2)) && (col % 2 == 1))
                    {
                        piece = new Piece(PieceType.Simple, ColorType.White, GetImagePath(PieceType.Simple, ColorType.White, col, row), Tuple.Create(row, col));
                    }
                    else if ((row == 1) && (col % 2 == 0))
                    {
                        piece = new Piece(PieceType.Simple, ColorType.White, GetImagePath(PieceType.Simple, ColorType.White, col, row), Tuple.Create(row, col));
                    }
                    else if (((row == 5) || (row == 7)) && (col % 2 == 0))
                    {
                        piece = new Piece(PieceType.Simple, ColorType.Red, GetImagePath(PieceType.Simple, ColorType.Red, col, row), Tuple.Create(row, col));
                    }
                    else if ((row == 6) && (col % 2 == 1))
                    {
                        piece = new Piece(PieceType.Simple, ColorType.Red, GetImagePath(PieceType.Simple, ColorType.Red, col, row), Tuple.Create(row, col));
                    }
                    else
                    {
                        piece = new Piece(PieceType.None, ColorType.None, GetImagePath(PieceType.None, ColorType.None, col, row), Tuple.Create(row, col));
                    }
                    rowPices.Add(piece);
                }
                game.Board.Pieces.Add(rowPices);
            }
            CountPieces();
            Round = currentPlayer.Color.ToString() + " player's turn.";
            OnPropertyChanged("Game");
        }
        public void CheckersNewGame(object parameter)
        {
            game = new Game();
            currentPlayer = game.Player1;
            InitializeBoard();
        }
        public void ValidMoves(int row, int column)
        {
            if (column < 0 && column > 7)
            {
                return;
            }
            if (game.Board.Pieces[row][column].Type == PieceType.King || game.Board.Pieces[row][column].Color == ColorType.White)
            {
                if (row > -1 && row < 7)
                {
                    if (column > 0)
                    {
                        if (game.Board.Pieces[row + 1][column - 1].Type == PieceType.None)
                        {
                            validMoves.Add(Tuple.Create(row + 1, column - 1));
                        }
                        if (game.Board.Pieces[row + 1][column - 1].Type != PieceType.None && (game.Board.Pieces[row + 1][column - 1].Color == ColorType.Red
                            || (game.Board.Pieces[row][column].Type == PieceType.King && game.Board.Pieces[row + 1][column - 1].Color == ColorType.White)))
                        {
                            if (row + 1 < 7 && column - 1 > 0)
                            {
                                if (game.Board.Pieces[row + 2][column - 2].Type == PieceType.None)
                                {
                                    validMoves.Add(Tuple.Create(row + 2, column - 2));
                                }
                            }
                        }
                    }
                    if (column < 7)
                    {
                        if (game.Board.Pieces[row + 1][column + 1].Type == PieceType.None)
                        {
                            validMoves.Add(Tuple.Create(row + 1, column + 1));
                        }
                        if (game.Board.Pieces[row + 1][column + 1].Type != PieceType.None && (game.Board.Pieces[row + 1][column + 1].Color == ColorType.Red
                            || (game.Board.Pieces[row][column].Type == PieceType.King && game.Board.Pieces[row + 1][column + 1].Color == ColorType.White)))
                        {
                            if (row + 1 < 7 && column + 1 < 7)
                            {
                                if (game.Board.Pieces[row + 2][column + 2].Type == PieceType.None)
                                {
                                    validMoves.Add(Tuple.Create(row + 2, column + 2));
                                }
                            }
                        }
                    }
                }
            }
            if (game.Board.Pieces[row][column].Type == PieceType.King || game.Board.Pieces[row][column].Color == ColorType.Red)
            {
                if (row > 0 && row < 8)
                {
                    if (column > 0)
                    {
                        if (game.Board.Pieces[row - 1][column - 1].Type == PieceType.None)
                        {
                            validMoves.Add(Tuple.Create(row - 1, column - 1));
                        }
                        if (game.Board.Pieces[row - 1][column - 1].Type != PieceType.None && (game.Board.Pieces[row - 1][column - 1].Color == ColorType.White
                            || (game.Board.Pieces[row][column].Type == PieceType.King && game.Board.Pieces[row - 1][column - 1].Color == ColorType.Red)))
                        {
                            if (row - 1 > 0 && column - 1 > 0)
                            {
                                if (game.Board.Pieces[row - 2][column - 2].Type == PieceType.None)
                                {
                                    validMoves.Add(Tuple.Create(row - 2, column - 2));
                                }
                            }
                        }
                    }
                    if (column < 7)
                    {
                        if (game.Board.Pieces[row - 1][column + 1].Type == PieceType.None)
                        {
                            validMoves.Add(Tuple.Create(row - 1, column + 1));
                        }
                        if (game.Board.Pieces[row - 1][column + 1].Type != PieceType.None && (game.Board.Pieces[row - 1][column + 1].Color == ColorType.White
                            || (game.Board.Pieces[row][column].Type == PieceType.King && game.Board.Pieces[row - 1][column + 1].Color == ColorType.Red)))
                        {
                            if (row - 1 > 0 && column + 1 < 7)
                            {
                                if (game.Board.Pieces[row - 2][column + 2].Type == PieceType.None)
                                {
                                    validMoves.Add(Tuple.Create(row - 2, column + 2));
                                }
                            }
                        }
                    }
                }
            }
        }
        private string GetImagePath(PieceType type, ColorType color, int row, int col)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string directoryPath = Path.GetFullPath(Path.Combine(basePath, "..\\..\\..\\Resources\\Images"));
            if ((color == ColorType.None) && (type == PieceType.None))
            {
                if (col % 2 == row % 2)
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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Checkers.Model
{
    internal class Board : INotifyPropertyChanged
    {
        private ObservableCollection<Piece> pieces { get; set; }
        public ObservableCollection<Piece> Pieces
        {
            get { return pieces; }
            set
            {
                if (pieces != value)
                {
                    pieces = value;
                    NotifyPropertyChanged(nameof(Pieces));
                }
            }
        }

        public Board()
        {
            /*empty*/
        }
        public void Initialize()
        {
            pieces = new ObservableCollection<Piece>();
            for (int index = 0; index < 8; index++)
            {
                for (int jndex = 0; jndex < 8; jndex++)
                {
                    if(((index==0)||(index==2))&& (jndex % 2 == 1))
                    {
                        Piece piece = new(PieceType.Simple, ColorType.White, Tuple.Create(index+1, jndex+1));
                        pieces.Add(piece);
                    }
                    else if((index==1)&&(jndex%2==0))
                    {
                        Piece piece = new(PieceType.Simple, ColorType.White, Tuple.Create(index + 1, jndex + 1));
                        pieces.Add(piece);
                    }
                    else if(((index==5)||(index==7))&&(jndex%2==0))
                    {
                        Piece piece = new(PieceType.Simple, ColorType.Red, Tuple.Create(index + 1, jndex + 1));
                        pieces.Add(piece);
                    }
                    else if((index==6)&&(jndex%2==1))
                    {
                        Piece piece = new(PieceType.Simple, ColorType.Red, Tuple.Create(index + 1, jndex + 1));
                        pieces.Add(piece);
                    }
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

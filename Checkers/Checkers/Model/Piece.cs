using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    internal class Piece : INotifyPropertyChanged
    {
        private PieceType type;
        public PieceType Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    NotifyPropertyChanged(nameof(Type));
                }
            }
        }
        private ColorType color;
        public ColorType Color
        {
            get { return color; }
            set
            {
                if (color != value)
                {
                    color = value;
                    NotifyPropertyChanged(nameof(Color));
                }
            }
        }
        private Tuple<int, int> coordinates;
        public Tuple<int, int> Coordinates
        {
            get { return coordinates; }
            set
            {
                if (coordinates != value)
                {
                    coordinates = value;
                    NotifyPropertyChanged(nameof(Coordinates));
                }
            }
        }
        public Piece()
        {
            color = ColorType.None;
            type = PieceType.None;
            coordinates = Tuple.Create(-1, -1);
        }
        public Piece(PieceType type, ColorType color, Tuple<int, int> coordinates)
        {
            this.type = type;
            this.color = color;
            this.coordinates = coordinates;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

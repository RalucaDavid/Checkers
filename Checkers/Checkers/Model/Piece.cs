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
        private String imagePath;
        public String ImagePath
        {
            get { return imagePath; }
            set
            {
                if (imagePath != value)
                {
                    imagePath = value;
                    NotifyPropertyChanged(nameof(ImagePath));
                }
            }
        }

        private Tuple<int, int> coordonates;
        public Tuple<int, int> Coordonates
        {
            get { return coordonates; }
            set
            {
                if (coordonates != value)
                {
                    coordonates = value;
                    NotifyPropertyChanged(nameof(Coordonates));
                }
            }
        }
        public Piece()
        {
            color = ColorType.None;
            type = PieceType.None;
            imagePath = "";
            coordonates = Tuple.Create(-1, -1);
        }
        public Piece(PieceType type, ColorType color, String imagePath, Tuple<int,int> coordonates)
        {
            this.type = type;
            this.color = color;
            this.imagePath = imagePath;
            this.coordonates = coordonates;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

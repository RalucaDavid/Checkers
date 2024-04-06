using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    internal class Player : INotifyPropertyChanged
    {
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
        public Player(ColorType color)
        {
            this.color = color;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

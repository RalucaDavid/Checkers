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
        private ObservableCollection<ObservableCollection<Piece>> pieces;
        public ObservableCollection<ObservableCollection<Piece>> Pieces
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
            pieces = new ObservableCollection<ObservableCollection<Piece>>();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

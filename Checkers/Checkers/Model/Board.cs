using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Checkers.Model
{
    internal class Board
    {
        private ObservableCollection<ObservableCollection<bool>> matrix { get; set; }
        private ObservableCollection<Piece> pieces { get; set; }
        public Board()
        {
            /*empty*/
        }
        public void Initialize()
        {
            // Add the logic
            matrix = new ObservableCollection<ObservableCollection<bool>>();
            for (int index = 0; index < 8; index++)
            {
                ObservableCollection<bool> row = new ObservableCollection<bool>();
                for (int jndex = 0; jndex < 8; jndex++)
                {
                    row.Add(false);
                }
                matrix.Add(row);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Model
{
    internal class Piece
    {
        private PieceType type;
        private ColorType color;
        private Tuple<int, int> coordinates;

        Piece()
        {
            color = ColorType.None;
            type = PieceType.None;
            coordinates = Tuple.Create(-1, -1);
        }
    }
}

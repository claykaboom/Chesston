using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public interface IPieceFactory
    {
        Piece ProducePiece(string pieceInfo, ElementColor c, Square s);
    }
}

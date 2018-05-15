using ChessTonGame.Classes.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public class ClassicPieceFactory : IPieceFactory
    {
        public Piece ProducePiece(string pieceInfo, ElementColor c, Square s)
        {
            switch (pieceInfo)
            {
                case "R":
                    return new Rook(c, s);
                case "N":
                    return new Knight(c, s);
                case "B":
                    return new Bishop(c, s);
                case "Q":
                    return new Queen(c, s);
                case "K":
                    return new King(c, s);
                case " ":
                    return new Pawn(c, s);
                default:
                    throw new Exception($"Can't produce a piece based on {pieceInfo}");
            }
        }
    }
}

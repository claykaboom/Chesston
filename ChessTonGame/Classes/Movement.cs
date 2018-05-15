using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public class Movement
    {
        public override string ToString()
        {
            return $"{Peca}{CasaOrigem} to {CasaDestino} - movido por {Peca.Cor.ToString()}";
        }
        public Movement(Piece p, Square casaOrigem, Square casaDestino)//, Passo passo)
        {
            this.CasaOrigem = casaOrigem;
            this.CasaDestino = casaDestino;
            this.Peca = p;
            this.PecaAnterior = casaDestino.PecaAtual;
            //   this.PassoDado = passo;

        }

        public static Movement GetMovement(string MoveInfo, Board b, ElementColor c, IPieceFactory pFactory)
        {
            //'Bf8 to b4 - movido por Preta'
            string piece = MoveInfo.Substring(0, 1);
            string startPosCol = MoveInfo.Substring(1, 1);
            int startPosLine = Convert.ToInt32(MoveInfo.Substring(2, 1));
            string endPosCol = MoveInfo.Substring(7, 1);
            int endPosLine = Convert.ToInt32(MoveInfo.Substring(8, 1));

            int startCol = (int)(Convert.ToChar(startPosCol));

            int endCol = (int)(Convert.ToChar(endPosCol));
            Square sStart = b.Casas[8-startPosLine][  startCol - 97];
            Square sEnd = b.Casas[ 8-endPosLine][  endCol- 97];
            Movement m = new Movement(pFactory.ProducePiece(piece, c, sStart), sStart, sEnd);
            return m;
        }


        public Square CasaOrigem { get; private set; }
        public Square CasaDestino { get; private set; }
        public Piece Peca { get; private set; }
        public Piece PecaAnterior { get; private set; }
        //     public Passo PassoDado { get; private set; }
        public object MovementInfo { get; private set; }
        public void SetMovementInfo(Piece movementOwner, object info)
        {
            if (movementOwner == this.Peca)
            { this.MovementInfo = info; }
        }

    }
}

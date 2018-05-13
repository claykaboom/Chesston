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

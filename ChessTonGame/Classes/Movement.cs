using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public class Movement
    {

        public Movement(Peca p, Casa casaOrigem, Casa casaDestino)//, Passo passo)
        {
            this.CasaOrigem = casaOrigem;
            this.CasaDestino = casaDestino;
            this.Peca = p;
            this.PecaAnterior = casaDestino.PecaAtual;
            //   this.PassoDado = passo;

        }
        public Casa CasaOrigem { get; private set; }
        public Casa CasaDestino { get; private set; }
        public Peca Peca { get; private set; }
        public Peca PecaAnterior { get; private set; }
        //     public Passo PassoDado { get; private set; }
        public object MovementInfo { get; private set; }
        public void SetMovementInfo(Peca movementOwner, object info)
        {
            if (movementOwner == this.Peca)
            { this.MovementInfo = info; }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public class Movement
    {

        public Movement(Peca p, Casa casaOrigem, Casa casaDestino)
        {
            this.CasaOrigem = casaOrigem;
            this.CasaDestino = casaDestino;
            this.Peca = p;

        }
        public Casa CasaOrigem { get; private set; }
        public Casa CasaDestino { get; private set; }
        public Peca Peca { get; private set; }
    }
}

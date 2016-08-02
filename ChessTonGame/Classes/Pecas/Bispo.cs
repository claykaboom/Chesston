using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pecas
{
    public class Bispo : Peca
    {
        public Bispo(CorElemento cor, Casa c)
            : base(cor, c,false)
        { }
        public override bool EstaEmXeque
        {
            get { throw new NotImplementedException(); }
        }

        public override decimal ValorPontos
        {
            get { return 3; }
        }

        public override List<List<Passo>> getRotasPossiveis()
        {
            List<List<Passo>> rotas = new List<List<Passo>>();

            rotas.Add(new List<Passo>() {Passo.DiagonalDireitaFrenteIndefinido  });
            rotas.Add(new List<Passo>() {Passo.DiagonalDireitaTrasIndefinido  });
            rotas.Add(new List<Passo>() {Passo.DiagonalEsquerdaFrenteIndefinido  });
            rotas.Add(new List<Passo>() {Passo.DiagonalEsquerdaTrasIndefinido }); 

            return rotas;
        }

        public override System.Drawing.Image getImage()
        { 
            if (this.Cor == CorElemento.Branca)
            {
                return Resources.whiteBishop;
            }
            else
            {
                return Resources.blackBishop;
            }
        }

        public override bool FicaEmXequeNaCasa(Casa casa)
        {
            throw new NotImplementedException();
        }
    }
}

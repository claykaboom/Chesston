using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pecas
{
    public class Torre : Peca
    {
        public Torre()
        { }
        public Torre(CorElemento cor, Casa c)
            : base(cor, c, false)
        { }
 

        public override decimal ValorPontos
        {
            get { return 5; }
        }

        public override List<List<Passo>> getRotasPossiveis()
        {
            List<List<Passo>> rotas = new List<List<Passo>>();

            rotas.Add(new List<Passo>() { Passo.FrenteIndefinido });
            rotas.Add(new List<Passo>() { Passo.TrasIndefinido });
            rotas.Add(new List<Passo>() { Passo.DireitaIndefinido });
            rotas.Add(new List<Passo>() { Passo.EsquerdaIndefinido });

            return rotas;
        }

        public override System.Drawing.Image getImage()
        {
            if (this.Cor == CorElemento.Branca)
            {
                return Resources.whiteRook;
            }
            else
            {
                return Resources.blackRook;
            }
        }
         
    }
}

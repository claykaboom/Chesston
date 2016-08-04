using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pecas
{
    public class Torre : Peca
    {
        public Torre(CorElemento cor, Casa c)
            : base(cor, c, false)
        { }
        public override bool EstaEmXeque
        {
            get { throw new NotImplementedException(); }
        }

        public override decimal ValorPontos
        {
            get { return 5; }
        }

        public override List<List<Passo>> getRotasPossiveis()
        {
            List<List<Passo>> rotas = new List<List<Passo>>();

            rotas.Add(new List<Passo>() { Passo.FrenteIndefinido });
            rotas.Add(new List<Passo>() { Passo.TrasIndefinido });
            rotas.Add(new List<Passo>() { Passo.FrenteIndefinido });
            rotas.Add(new List<Passo>() { Passo.TrasIndefinido });

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

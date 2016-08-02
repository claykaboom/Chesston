using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pecas
{
    public class Cavalo : Peca
    {
        public Cavalo(CorElemento cor, Casa c)
            : base(cor, c,true)
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

            rotas.Add(new List<Passo>() {Passo.Frente, Passo.Frente, Passo.Direita });
            rotas.Add(new List<Passo>() {  Passo.Frente, Passo.Frente, Passo.Esquerda });
            rotas.Add(new List<Passo>() {  Passo.Tras, Passo.Tras, Passo.Direita });
            rotas.Add(new List<Passo>() {  Passo.Tras, Passo.Tras, Passo.Esquerda });

            rotas.Add(new List<Passo>() { Passo.Direita, Passo.Direita, Passo.Frente });
            rotas.Add(new List<Passo>() { Passo.Direita, Passo.Direita, Passo.Tras });
            rotas.Add(new List<Passo>() {  Passo.Esquerda, Passo.Esquerda, Passo.Frente });
            rotas.Add(new List<Passo>() {  Passo.Esquerda, Passo.Esquerda, Passo.Tras });

            return rotas;
        }

        public override System.Drawing.Image getImage()
        { 
            if (this.Cor == CorElemento.Branca)
            {
                return Resources.whiteKnight;
            }
            else
            {
                return Resources.blackKnight;
            }
        }

        public override bool FicaEmXequeNaCasa(Casa casa)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pecas
{
    public class Rei : Peca
    {
        public Rei(CorElemento cor, Casa c)
            : base(cor, c, false)
        {
            this.PieceMoved += Rei_PieceMoved;
            this._tabuleiro.PieceMoved += _tabuleiro_PieceMoved;
        }

        private void _tabuleiro_PieceMoved(Movement m)
        {
            if (m.Peca.Cor == this.Cor && this.EstaEmXeque())
            { 
                this._tabuleiro.UndoLastMovement();
            }

        }

        private void Rei_PieceMoved(Movement m)
        {
            if (this.EstaEmXeque())
            {
            //    this._tabuleiro.UndoLastMovement(); 
            }
        }

        public override decimal ValorPontos
        {
            get { return 5; }
        }

        public override List<List<Passo>> getRotasPossiveis()
        {
            List<List<Passo>> rotas = new List<List<Passo>>();
            var casaTeste = getCasaPorPassos(new List<Passo>() { Passo.Frente });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Passo>() { Passo.Frente });
            }
            casaTeste = getCasaPorPassos(new List<Passo>() { Passo.Tras });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Passo>() { Passo.Tras });
            }

            casaTeste = getCasaPorPassos(new List<Passo>() { Passo.Direita });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Passo>() { Passo.Direita });
            }


            casaTeste = getCasaPorPassos(new List<Passo>() { Passo.Esquerda });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Passo>() { Passo.Esquerda });
            }


            casaTeste = getCasaPorPassos(new List<Passo>() { Passo.DiagonalDireitaFrente });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Passo>() { Passo.DiagonalDireitaFrente });
            }

            casaTeste = getCasaPorPassos(new List<Passo>() { Passo.DiagonalDireitaFrente });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Passo>() { Passo.DiagonalDireitaFrente });
            }

            casaTeste = getCasaPorPassos(new List<Passo>() { Passo.DiagonalDireitaTras });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Passo>() { Passo.DiagonalDireitaTras });
            }

            casaTeste = getCasaPorPassos(new List<Passo>() { Passo.DiagonalEsquerdaTras });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Passo>() { Passo.DiagonalEsquerdaTras });
            }

            return rotas;
        }


        public override System.Drawing.Image getImage()
        {
            if (this.Cor == CorElemento.Branca)
            {
                return Resources.whiteKing;
            }
            else
            {
                return Resources.blackKing;
            }
        }

    }
}

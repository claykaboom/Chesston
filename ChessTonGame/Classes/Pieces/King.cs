using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pieces
{
    public class King : Piece
    {
        public King(ElementColor cor, Square c)
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

        public override List<List<Step>> getRotasPossiveis()
        {
            List<List<Step>> rotas = new List<List<Step>>();
            var casaTeste = getCasaPorPassos(new List<Step>() { Step.Front });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Step>() { Step.Front });
            }
            casaTeste = getCasaPorPassos(new List<Step>() { Step.Back });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Step>() { Step.Back });
            }

            casaTeste = getCasaPorPassos(new List<Step>() { Step.Right });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Step>() { Step.Right });
            }


            casaTeste = getCasaPorPassos(new List<Step>() { Step.Left });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Step>() { Step.Left });
            }


            casaTeste = getCasaPorPassos(new List<Step>() { Step.DiagonalRightFront });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Step>() { Step.DiagonalRightFront });
            }

            casaTeste = getCasaPorPassos(new List<Step>() { Step.DiagonalRightFront });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Step>() { Step.DiagonalRightFront });
            }

            casaTeste = getCasaPorPassos(new List<Step>() { Step.DiagonalRightBack });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Step>() { Step.DiagonalRightBack });
            }

            casaTeste = getCasaPorPassos(new List<Step>() { Step.DiagonalLeftBack });
            if (casaTeste != null && !this.FicaEmXequeNaCasa(casaTeste))
            {

                rotas.Add(new List<Step>() { Step.DiagonalLeftBack });
            }

            return rotas;
        }


        public override System.Drawing.Image getImage()
        {
            if (this.Cor == ElementColor.Branca)
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

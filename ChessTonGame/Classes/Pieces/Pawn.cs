using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pieces
{
    public class Pawn : Piece
    {

        public Pawn(ElementColor cor, Square casa)
            : base(cor, casa, false, true,true, false)
        {
            this.PieceMoved += Peao_PieceMoved;
            this._board.MovementUndone += _tabuleiro_MovementUndone;
        }

        private void _tabuleiro_MovementUndone(Movement m)
        {
            Tuple<string, Square, Piece> mInfo = null;
            if (m.MovementInfo != null)
            {
                mInfo = (Tuple<string, Square, Piece>)m.MovementInfo;

            }
            if (m.Peca == this && mInfo != null && mInfo.Item1 == "en pasant")
            {
                mInfo.Item2.PecaAtual = mInfo.Item3;
                this._pecasComidas.Remove(mInfo.Item3);
            }
        }

        private void Peao_PieceMoved(Movement m)
        {

            var casaDiagonalEsquerdaTras = this.getSquareBySteps(new List<Step> { Step.DiagonalLeftBack });
            var casaDiagonalDireitaTras = this.getSquareBySteps(new List<Step> { Step.DiagonalRightBack });

            if (m.PecaAnterior == null)
            {
                // foi en passant nos dois casos a seguir foi

                if (m.CasaOrigem == casaDiagonalEsquerdaTras || m.CasaOrigem == casaDiagonalDireitaTras)
                {
                    if (this.Cor == ElementColor.Branca)
                    {
                        if (this._board.BrancasEmbaixo)
                        {
                            if (this.CasaAtual.CasaInferior!=null && this.CasaAtual.CasaInferior.PecaAtual != null && this.CasaAtual.CasaInferior.PecaAtual.getMovements().Count == 1 && _board.Movimentos.IndexOf(this.CasaAtual.CasaInferior.PecaAtual.getMovements().First()) == _board.Movimentos.Count - 1)
                            {
                                m.SetMovementInfo(this, Tuple.Create<string, Square, Piece>("en pasant", this.CasaAtual.CasaInferior, this.CasaAtual.CasaInferior.PecaAtual));

                                this.Comer(this.CasaAtual.CasaInferior.PecaAtual);




                            }

                        }
                        else
                        {
                            if (this.CasaAtual.CasaSuperior != null && this.CasaAtual.CasaSuperior.PecaAtual != null && this.CasaAtual.CasaSuperior.PecaAtual.getMovements().Count == 1 && _board.Movimentos.IndexOf(this.CasaAtual.CasaSuperior.PecaAtual.getMovements().First()) == _board.Movimentos.Count - 1)
                            {
                                m.SetMovementInfo(this, Tuple.Create<string, Square, Piece>("en pasant", this.CasaAtual.CasaSuperior, this.CasaAtual.CasaSuperior.PecaAtual));
                                this.Comer(this.CasaAtual.CasaSuperior.PecaAtual);

                            }

                        }
                    }
                    else
                    {

                        if (this._board.BrancasEmbaixo)
                        {
                            if (this.CasaAtual.CasaSuperior != null && this.CasaAtual.CasaSuperior.PecaAtual != null && this.CasaAtual.CasaSuperior.PecaAtual.getMovements().Count == 1 && _board.Movimentos.IndexOf(this.CasaAtual.CasaSuperior.PecaAtual.getMovements().First()) == _board.Movimentos.Count - 1)
                            {
                                m.SetMovementInfo(this, Tuple.Create<string, Square, Piece>("en pasant", this.CasaAtual.CasaSuperior, this.CasaAtual.CasaSuperior.PecaAtual));
                                this.Comer(this.CasaAtual.CasaSuperior.PecaAtual);
                            }
                        }
                        else
                        {
                            if (this.CasaAtual.CasaInferior != null && this.CasaAtual.CasaInferior.PecaAtual != null && this.CasaAtual.CasaInferior.PecaAtual.getMovements().Count == 1 && _board.Movimentos.IndexOf(this.CasaAtual.CasaInferior.PecaAtual.getMovements().First()) == _board.Movimentos.Count - 1)
                            {
                                m.SetMovementInfo(this, Tuple.Create<string, Square, Piece>("en pasant", this.CasaAtual.CasaInferior, this.CasaAtual.CasaInferior.PecaAtual));
                                this.Comer(this.CasaAtual.CasaInferior.PecaAtual);
                            }
                        }

                    }
                }



            }
        }

        public override decimal ValueInPoints
        {
            get { return 1; }
        }


        public override List<List<Step>> getPossibleRoutes()
        {


            List<List<Step>> rotas = new List<List<Step>>();
            if (!this.JaMoveu())
            {
                List<Step> rotaPrimeiroMovimento = new List<Step>() { Step.Front, Step.Front };

                var CasaFrentePrimeiroMovimento = this.getCasasPorPassos(rotaPrimeiroMovimento).FirstOrDefault();
                //se não tem peca à frente , pode mover
                if (CasaFrentePrimeiroMovimento != null && CasaFrentePrimeiroMovimento.PecaAtual == null)
                {
                    rotas.Add(rotaPrimeiroMovimento);
                }

            }

            //regra para o en pasant

            //se tem casa a direita , que é um peao inimigo:
            //deve ter sido o primeiro movimento do outro peao e nao pode ter mais nenhum movimento apos aquele
            if (this.CasaAtual.CasaDireita != null 
                && this.CasaAtual.CasaDireita.PecaAtual is Pawn 
                && this.CasaAtual.CasaDireita.PecaAtual.Cor != this.Cor
                && this.CasaAtual.CasaDireita.PecaAtual.getMovements().Count == 1
                && _board.Movimentos.IndexOf(this.CasaAtual.CasaDireita.PecaAtual.getMovements().First()) == _board.Movimentos.Count - 1)
            {
                if (this.Cor == ElementColor.Branca)
                {
                    if (this._board.BrancasEmbaixo)
                    {
                        List<Step> rotaEnPasant = new List<Step>() { Step.DiagonalRightFront };

                        rotas.Add(rotaEnPasant);
                    }
                    else
                    {

                        List<Step> rotaEnPasant = new List<Step>() { Step.DiagonalLeftFront };

                        rotas.Add(rotaEnPasant);
                    }
                }
                else
                {

                    if (this._board.BrancasEmbaixo)
                    {
                        List<Step> rotaEnPasant = new List<Step>() { Step.DiagonalLeftFront };

                        rotas.Add(rotaEnPasant);
                    }
                    else
                    {

                        List<Step> rotaEnPasant = new List<Step>() { Step.DiagonalRightFront };

                        rotas.Add(rotaEnPasant);
                    }
                }

            }
            else if (this.CasaAtual.CasaEsquerda != null
                && this.CasaAtual.CasaEsquerda.PecaAtual is Pawn
                && this.CasaAtual.CasaEsquerda.PecaAtual.Cor != this.Cor
                && this.CasaAtual.CasaEsquerda.PecaAtual.getMovements().Count == 1 
                && _board.Movimentos.IndexOf(this.CasaAtual.CasaEsquerda.PecaAtual.getMovements().First()) == _board.Movimentos.Count - 1)
            {
                if (this.Cor == ElementColor.Branca)
                {
                    if (this._board.BrancasEmbaixo)
                    {
                        List<Step> rotaEnPasant = new List<Step>() { Step.DiagonalLeftFront };

                        rotas.Add(rotaEnPasant);
                    }
                    else
                    {

                        List<Step> rotaEnPasant = new List<Step>() { Step.DiagonalRightFront };

                        rotas.Add(rotaEnPasant);
                    }
                }
                else
                {

                    if (this._board.BrancasEmbaixo)
                    {
                        List<Step> rotaEnPasant = new List<Step>() { Step.DiagonalRightFront };

                        rotas.Add(rotaEnPasant);
                    }
                    else
                    {

                        List<Step> rotaEnPasant = new List<Step>() { Step.DiagonalLeftFront };

                        rotas.Add(rotaEnPasant);
                    }
                }

            }
            //fim en pasant
            var PassosComerDireita = new List<Step>() { Step.DiagonalRightFront };
            var PassosComerEsquerda = new List<Step>() { Step.DiagonalLeftFront };
            var CasaDiagonal = this.getCasasPorPassos(PassosComerDireita).FirstOrDefault();

            if (CasaDiagonal != null && CasaDiagonal.PecaAtual != null)
            {
                if (CasaDiagonal.PecaAtual.ehInimigaDe(this))
                {
                    rotas.Add(PassosComerDireita);

                }
            }

            CasaDiagonal = this.getCasasPorPassos(PassosComerEsquerda).FirstOrDefault();

            if (CasaDiagonal != null && CasaDiagonal.PecaAtual != null)
            {
                if (CasaDiagonal.PecaAtual.ehInimigaDe(this))
                {
                    rotas.Add(PassosComerEsquerda);

                }
            }

            var movimentoGeral = new List<Step>() { Step.Front };
            var CasaFrente = this.getCasasPorPassos(movimentoGeral).FirstOrDefault();

            //se não tem peca à frente , pode mover
            if (CasaFrente != null && CasaFrente.PecaAtual == null)
            {
                rotas.Add(movimentoGeral);
            }

            return rotas;
        }


        public override System.Drawing.Image getImage()
        {
            //Graphics g;
            //Bitmap bmp = new Bitmap(this.getTamanhoPeca(), this.getTamanhoPeca());
            //g = Graphics.FromImage(bmp);
            //if (this.Cor == CorElemento.Preta)
            //{
            //    g.FillEllipse(new SolidBrush(System.Drawing.Color.Black), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
            //    g.DrawEllipse(new Pen(new SolidBrush(Color.White), 1), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
            //}
            //else
            //{

            //    g.FillEllipse(new SolidBrush(System.Drawing.Color.White), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
            //    g.DrawEllipse(new Pen(new SolidBrush(Color.Black), 1), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
            //}

            //if (this.EstaSelecionada)
            //{
            //    g.DrawEllipse(new Pen(new SolidBrush(Color.Red), 2), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
            //}
            //return bmp;
            if (this.Cor == ElementColor.Branca)
            {
                return Resources.whitePawn;
            }
            else
            {
                return Resources.blackPawn;
            }
        }

        public override string ToString() => " ";
    }
}

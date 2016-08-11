using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pecas
{
    public class Peao : Peca
    {

        public Peao(CorElemento cor, Casa casa)
            : base(cor, casa, false)
        {
            this.PieceMoved += Peao_PieceMoved;
            this._tabuleiro.MovementUndone += _tabuleiro_MovementUndone;
        }

        private void _tabuleiro_MovementUndone(Movement m)
        {
            Tuple<string, Casa, Peca> mInfo = null;
            if (m.MovementInfo != null)
            {
                mInfo = (Tuple<string, Casa, Peca>)m.MovementInfo;

            }
            if (m.Peca == this && mInfo != null && mInfo.Item1 == "en pasant")
            {
                mInfo.Item2.PecaAtual = mInfo.Item3;
                this._pecasComidas.Remove(mInfo.Item3);
            }
        }

        private void Peao_PieceMoved(Movement m)
        {

            var casaDiagonalEsquerdaTras = this.getCasaPorPassos(new List<Passo> { Passo.DiagonalEsquerdaTras });
            var casaDiagonalDireitaTras = this.getCasaPorPassos(new List<Passo> { Passo.DiagonalDireitaTras });

            if (m.PecaAnterior == null)
            {
                // foi en passant nos dois casos a seguir foi

                if (m.CasaOrigem == casaDiagonalEsquerdaTras || m.CasaOrigem == casaDiagonalDireitaTras)
                {
                    if (this.Cor == CorElemento.Branca)
                    {
                        if (this._tabuleiro.BrancasEmbaixo)
                        {
                            if (this.CasaAtual.CasaInferior!=null && this.CasaAtual.CasaInferior.PecaAtual != null && this.CasaAtual.CasaInferior.PecaAtual.getMovements().Count == 1 && _tabuleiro.Movimentos.IndexOf(this.CasaAtual.CasaInferior.PecaAtual.getMovements().First()) == _tabuleiro.Movimentos.Count - 1)
                            {
                                m.SetMovementInfo(this, Tuple.Create<string, Casa, Peca>("en pasant", this.CasaAtual.CasaInferior, this.CasaAtual.CasaInferior.PecaAtual));

                                this.Comer(this.CasaAtual.CasaInferior.PecaAtual);




                            }

                        }
                        else
                        {
                            if (this.CasaAtual.CasaSuperior != null && this.CasaAtual.CasaSuperior.PecaAtual != null && this.CasaAtual.CasaSuperior.PecaAtual.getMovements().Count == 1 && _tabuleiro.Movimentos.IndexOf(this.CasaAtual.CasaSuperior.PecaAtual.getMovements().First()) == _tabuleiro.Movimentos.Count - 1)
                            {
                                m.SetMovementInfo(this, Tuple.Create<string, Casa, Peca>("en pasant", this.CasaAtual.CasaSuperior, this.CasaAtual.CasaSuperior.PecaAtual));
                                this.Comer(this.CasaAtual.CasaSuperior.PecaAtual);

                            }

                        }
                    }
                    else
                    {

                        if (this._tabuleiro.BrancasEmbaixo)
                        {
                            if (this.CasaAtual.CasaSuperior != null && this.CasaAtual.CasaSuperior.PecaAtual != null && this.CasaAtual.CasaSuperior.PecaAtual.getMovements().Count == 1 && _tabuleiro.Movimentos.IndexOf(this.CasaAtual.CasaSuperior.PecaAtual.getMovements().First()) == _tabuleiro.Movimentos.Count - 1)
                            {
                                m.SetMovementInfo(this, Tuple.Create<string, Casa, Peca>("en pasant", this.CasaAtual.CasaSuperior, this.CasaAtual.CasaSuperior.PecaAtual));
                                this.Comer(this.CasaAtual.CasaSuperior.PecaAtual);
                            }
                        }
                        else
                        {
                            if (this.CasaAtual.CasaInferior != null && this.CasaAtual.CasaInferior.PecaAtual != null && this.CasaAtual.CasaInferior.PecaAtual.getMovements().Count == 1 && _tabuleiro.Movimentos.IndexOf(this.CasaAtual.CasaInferior.PecaAtual.getMovements().First()) == _tabuleiro.Movimentos.Count - 1)
                            {
                                m.SetMovementInfo(this, Tuple.Create<string, Casa, Peca>("en pasant", this.CasaAtual.CasaInferior, this.CasaAtual.CasaInferior.PecaAtual));
                                this.Comer(this.CasaAtual.CasaInferior.PecaAtual);
                            }
                        }

                    }
                }



            }
        }

        public override decimal ValorPontos
        {
            get { return 1; }
        }


        public override List<List<Passo>> getRotasPossiveis()
        {


            List<List<Passo>> rotas = new List<List<Passo>>();
            if (!this.JaMoveu())
            {
                List<Passo> rotaPrimeiroMovimento = new List<Passo>() { Passo.Frente, Passo.Frente };

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
                && this.CasaAtual.CasaDireita.PecaAtual is Peao 
                && this.CasaAtual.CasaDireita.PecaAtual.Cor != this.Cor
                && this.CasaAtual.CasaDireita.PecaAtual.getMovements().Count == 1
                && _tabuleiro.Movimentos.IndexOf(this.CasaAtual.CasaDireita.PecaAtual.getMovements().First()) == _tabuleiro.Movimentos.Count - 1)
            {
                if (this.Cor == CorElemento.Branca)
                {
                    if (this._tabuleiro.BrancasEmbaixo)
                    {
                        List<Passo> rotaEnPasant = new List<Passo>() { Passo.DiagonalDireitaFrente };

                        rotas.Add(rotaEnPasant);
                    }
                    else
                    {

                        List<Passo> rotaEnPasant = new List<Passo>() { Passo.DiagonalEsquerdaFrente };

                        rotas.Add(rotaEnPasant);
                    }
                }
                else
                {

                    if (this._tabuleiro.BrancasEmbaixo)
                    {
                        List<Passo> rotaEnPasant = new List<Passo>() { Passo.DiagonalEsquerdaFrente };

                        rotas.Add(rotaEnPasant);
                    }
                    else
                    {

                        List<Passo> rotaEnPasant = new List<Passo>() { Passo.DiagonalDireitaFrente };

                        rotas.Add(rotaEnPasant);
                    }
                }

            }
            else if (this.CasaAtual.CasaEsquerda != null
                && this.CasaAtual.CasaEsquerda.PecaAtual is Peao
                && this.CasaAtual.CasaEsquerda.PecaAtual.Cor != this.Cor
                && this.CasaAtual.CasaEsquerda.PecaAtual.getMovements().Count == 1 
                && _tabuleiro.Movimentos.IndexOf(this.CasaAtual.CasaEsquerda.PecaAtual.getMovements().First()) == _tabuleiro.Movimentos.Count - 1)
            {
                if (this.Cor == CorElemento.Branca)
                {
                    if (this._tabuleiro.BrancasEmbaixo)
                    {
                        List<Passo> rotaEnPasant = new List<Passo>() { Passo.DiagonalEsquerdaFrente };

                        rotas.Add(rotaEnPasant);
                    }
                    else
                    {

                        List<Passo> rotaEnPasant = new List<Passo>() { Passo.DiagonalDireitaFrente };

                        rotas.Add(rotaEnPasant);
                    }
                }
                else
                {

                    if (this._tabuleiro.BrancasEmbaixo)
                    {
                        List<Passo> rotaEnPasant = new List<Passo>() { Passo.DiagonalDireitaFrente };

                        rotas.Add(rotaEnPasant);
                    }
                    else
                    {

                        List<Passo> rotaEnPasant = new List<Passo>() { Passo.DiagonalEsquerdaFrente };

                        rotas.Add(rotaEnPasant);
                    }
                }

            }
            //fim en pasant
            var PassosComerDireita = new List<Passo>() { Passo.DiagonalDireitaFrente };
            var PassosComerEsquerda = new List<Passo>() { Passo.DiagonalEsquerdaFrente };
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

            var movimentoGeral = new List<Passo>() { Passo.Frente };
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
            if (this.Cor == CorElemento.Branca)
            {
                return Resources.whitePawn;
            }
            else
            {
                return Resources.blackPawn;
            }
        }

    }
}

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
        }

        public override decimal ValorPontos
        {
            get { return 1; }
        }


        public override List<List<Passo>> getRotasPossiveis()
        {
            

            List<List<Passo>> rotas = new List<List<Passo>>();
            if (!this.JaMoveu)
            {
                List<Passo> rotaPrimeiroMovimento = new List<Passo>() { Passo.Frente, Passo.Frente };

                var CasaFrentePrimeiroMovimento = this.getCasasPorPassos(rotaPrimeiroMovimento).FirstOrDefault();
                //se não tem peca à frente , pode mover
                if (CasaFrentePrimeiroMovimento != null && CasaFrentePrimeiroMovimento.PecaAtual == null)
                {
                    rotas.Add(rotaPrimeiroMovimento); 
                }

            }

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

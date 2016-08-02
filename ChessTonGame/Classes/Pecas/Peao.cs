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
            : base(cor, casa)
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
                List<Passo> rota1 = new List<Passo>() { Passo.Frente, Passo.Frente };
                rotas.Add(rota1);
            }
            rotas.Add(new List<Passo>() { Passo.Frente });

            return rotas;
        }


        public override bool FicaEmXequeNaCasa(Casa casa)
        {
            throw new NotImplementedException();
        }

        public override System.Drawing.Image getImage()
        {
            Graphics g;
            Bitmap bmp = new Bitmap(this.getTamanhoPeca(), this.getTamanhoPeca());
            g = Graphics.FromImage(bmp);
            if (this.Cor == CorElemento.Preta)
            {
                g.FillEllipse(new SolidBrush(System.Drawing.Color.Black), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
                g.DrawEllipse(new Pen(new SolidBrush(Color.White), 1), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
            }
            else
            {

                g.FillEllipse(new SolidBrush(System.Drawing.Color.White), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
                g.DrawEllipse(new Pen(new SolidBrush(Color.Black), 1), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
            }

            if (this.EstaSelecionada)
            {
                g.DrawEllipse(new Pen(new SolidBrush(Color.Red), 2), 0, 0, this.getTamanhoPeca(), this.getTamanhoPeca());
            }
            return bmp;
        }

        public override bool EstaEmXeque
        {
            get { throw new NotImplementedException(); }
        }
    }
}

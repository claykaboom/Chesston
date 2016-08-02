using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ChessTonGame.Classes
{
    public class Tabuleiro
    {
        private Graphics g;
        internal int tamanhoCasa = 32;
        private Bitmap bmp;
        private Peca _pecaSelecionada = null;
        private List<Casa> _todasCasas;
        private List<List<Casa>> _casas;
        private bool _brancasEmbaixo = true;
        public List<List<Casa>> Casas
        {
            get
            {
                return _casas;
            }
        }
        public bool BrancasEmbaixo
        {
            get
            {
                return _brancasEmbaixo;
            }
        }
        public List<Casa> TodasCasas
        {
            get { return _todasCasas; }
        }

        public List<Peca> getTodasPecas()
        {
            return (from casa in this._todasCasas where casa.PecaAtual != null select casa.PecaAtual).ToList();
        }

        private Casa getCasa(int ixColuna, int ixLinha)
        {
            if (_casas.Count > 0 && ixLinha >= 0 && ixLinha <= _casas.Count - 1)
            {
                if (_casas[ixLinha].Count > 0 && ixColuna >= 0 && ixColuna <= _casas[ixLinha].Count - 1)
                {
                    return _casas[ixLinha][ixColuna];
                }
            }
            return null;
        }

        public Image DesenhaTabuleiro()
        {
            g.Clear(Color.White);

            for (int ixLinha = 0; ixLinha < _casas.Count; ixLinha++)
            {
                for (int ixColuna = 0; ixColuna < _casas[ixLinha].Count; ixColuna++)
                {
                    Casa casa = _casas[ixLinha][ixColuna];
                    g.FillRectangle(new SolidBrush(casa.Color), new Rectangle(casa.NumeroColuna * tamanhoCasa, casa.NumeroLinha * tamanhoCasa, tamanhoCasa, tamanhoCasa));
                    if (casa.PecaAtual != null)
                    {
                        g.DrawImage(casa.PecaAtual.getImage(), casa.NumeroColuna * tamanhoCasa, casa.NumeroLinha * tamanhoCasa);
                    }
                }
            }

            return bmp;
        }

        public List<Peca> PecasInimigasDe(Peca p)
        {
            CorElemento cor = CorElemento.Preta;
            if (p.Cor == CorElemento.Preta)
            {
                cor = CorElemento.Branca;
            }

            return (from peca in this.getTodasPecas() where peca.Cor == cor select peca).ToList();
        }

        public List<Peca> PecasAmigasDe(Peca p)
        {
            //retorna todas as peças amigas diferentes da própria peça
            return (from peca in this.getTodasPecas() where peca.Cor == p.Cor && peca != p select peca).ToList();
        }

        public void DeclaraXequeMate(CorElemento cor)
        {
            //disparar Evento de XequeMate para a cor informada
        }


        public Tabuleiro(int colunas, int linhas, bool brancasEmBaixo)
        {
            bmp = new Bitmap(colunas * tamanhoCasa, linhas * tamanhoCasa);
            g = Graphics.FromImage(bmp);
            CorElemento cor = CorElemento.Branca;
            _casas = new List<List<Casa>>();
            _todasCasas = new List<Casa>();
            this._brancasEmbaixo = brancasEmBaixo;
            for (int ixLinha = 0; ixLinha < linhas; ixLinha++)
            {
                List<Casa> listaColunasNaLinha = new List<Casa>();
                for (int ixColuna = 0; ixColuna < colunas; ixColuna++)
                {
                    Casa c = new Casa(cor, ixColuna, ixLinha, this);
                    if (cor == CorElemento.Branca)
                    {
                        cor = CorElemento.Preta;
                    }
                    else
                    {
                        cor = CorElemento.Branca;
                    }

                    //c.CasaSuperior = getCasa(ixColuna, ixLinha - 1);
                    //if (c.CasaSuperior != null)
                    //{
                    //    c.CasaSuperior.CasaInferior = c;
                    //}

                    //c.CasaInferior = getCasa(ixColuna, ixLinha + 1);
                    //if (c.CasaInferior != null)
                    //{
                    //    c.CasaInferior.CasaSuperior = c;
                    //}

                    //c.CasaEsquerda = getCasa(ixColuna - 1, ixLinha);
                    //if (c.CasaEsquerda != null)
                    //{
                    //    c.CasaEsquerda.CasaDireita = c;
                    //}

                    //c.CasaDireita = getCasa(ixColuna + 1, ixLinha);
                    //if (c.CasaDireita != null)
                    //{
                    //    c.CasaDireita.CasaEsquerda = c;
                    //}

                    listaColunasNaLinha.Add(c);
                    _todasCasas.Add(c);

                }


                _casas.Add(listaColunasNaLinha);
                if (cor == CorElemento.Preta)
                {
                    cor = new CorElemento();
                    cor = CorElemento.Branca;
                }
                else
                {
                    cor = new CorElemento();
                    cor = CorElemento.Preta;
                }

            }

            for (int ixLinha = 0; ixLinha < linhas; ixLinha++)
            {
                List<Casa> listaColunasNaLinha = new List<Casa>();
                for (int ixColuna = 0; ixColuna < colunas; ixColuna++)
                {
                    Casa c = getCasa(ixColuna, ixLinha);
                    c.CasaSuperior = getCasa(ixColuna, ixLinha - 1);
                    if (c.CasaSuperior != null)
                    {
                        c.CasaSuperior.CasaInferior = c;
                    }

                    c.CasaInferior = getCasa(ixColuna, ixLinha + 1);
                    if (c.CasaInferior != null)
                    {
                        c.CasaInferior.CasaSuperior = c;
                    }

                    c.CasaEsquerda = getCasa(ixColuna - 1, ixLinha);
                    if (c.CasaEsquerda != null)
                    {
                        c.CasaEsquerda.CasaDireita = c;
                    }

                    c.CasaDireita = getCasa(ixColuna + 1, ixLinha);
                    if (c.CasaDireita != null)
                    {
                        c.CasaDireita.CasaEsquerda = c;
                    }
                     

                }

 
            }


        }

        public void Click(int x, int y)
        {
            //let us find the spot corresponding to the position;
            int xIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(x / this.tamanhoCasa)));
            int yIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(y / this.tamanhoCasa)));
            Casa c = this.getCasa(xIndex, yIndex);
            if (this._pecaSelecionada == null)
            {
                this._pecaSelecionada = c.SelecionarPeca();
            }
            else
            {
                this._pecaSelecionada.MoverPara(c); //TODO: verificar se pode aqui fora ou la dentro?

                this._pecaSelecionada = null;
            }
        }
        internal void DeselecionarPecas()
        {
            this.getTodasPecas().ForEach(a => a.Deselecionar());
        }
    }
}

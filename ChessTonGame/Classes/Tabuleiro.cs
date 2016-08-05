using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using ChessTonGame.Classes.Pecas;

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
        private bool _highlightCheckedPieces = true;
        private ModoJogo _modoJogo = ModoJogo.AlternaTurnos;
        private CorElemento _vezDaCor = CorElemento.Branca;

        public Tabuleiro()
        { }
        public List<List<Casa>> Casas
        {
            get
            {
                return _casas;
            }
        }

        public CorElemento VezDaCor
        { get { return _vezDaCor; } }
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

        public Casa getCasa(int ixColuna, int ixLinha)
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
                    g.FillRectangle(new SolidBrush(casa.Color), new Rectangle(casa.ColumnIndex * tamanhoCasa, casa.LineIndex * tamanhoCasa, tamanhoCasa, tamanhoCasa));

                    if (casa.PecaAtual != null)
                    {
                        if (this._highlightCheckedPieces && casa.PecaAtual.EstaEmXeque())
                        {

                            g.FillRectangle(new SolidBrush(Color.PaleVioletRed), new Rectangle(casa.ColumnIndex * tamanhoCasa, casa.LineIndex * tamanhoCasa, tamanhoCasa, tamanhoCasa));
                        }
                        if (casa.PecaAtual.EstaSelecionada)
                        {
                            g.FillRectangle(new SolidBrush(Color.PaleGreen), new Rectangle(casa.ColumnIndex * tamanhoCasa, casa.LineIndex * tamanhoCasa, tamanhoCasa, tamanhoCasa));

                        }
                        g.DrawImage(casa.PecaAtual.getImage(), casa.ColumnIndex * tamanhoCasa, casa.LineIndex * tamanhoCasa);
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


        public Tabuleiro(int colunas, int linhas, bool brancasEmBaixo, ModoJogo modoJogo)
        {
            bmp = new Bitmap(colunas * tamanhoCasa, linhas * tamanhoCasa);
            g = Graphics.FromImage(bmp);
            CorElemento cor = CorElemento.Branca;
            _casas = new List<List<Casa>>();
            _todasCasas = new List<Casa>();
            _modoJogo = modoJogo;
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
                if (c!=null && c.ehVezDaPecaNaCasa())
                {
                    this._pecaSelecionada = c.SelecionarPeca();
                }
            }
            else
            {
                if (c != this._pecaSelecionada.CasaAtual && this._pecaSelecionada.PodeMoverPara(c))
                {
                    this._pecaSelecionada.MoverPara(c);
                    if (this._modoJogo == ModoJogo.AlternaTurnos)
                    {
                        if (this.VezDaCor == CorElemento.Preta)
                        {
                            this._vezDaCor = CorElemento.Branca;
                        }
                        else
                        {
                            this._vezDaCor = CorElemento.Preta;
                        }
                    }
                }
                DeselecionarPecas();
            }
        }
        internal void DeselecionarPecas()
        {
            this.getTodasPecas().ForEach(a => a.Deselecionar());

            this._pecaSelecionada = null;
        }

        public Tabuleiro getTabuleiroHipotetico()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType(),
               new[] {
                    typeof(Torre),
                    typeof(Cavalo),
                    typeof(Bispo),
                    typeof(Rainha),
                    typeof(Rei),
                    typeof(Peao ),
                    typeof(Casa),

                    });

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                String serializedBoard = textWriter.ToString();
                StringReader textReader = new StringReader(serializedBoard);
                return (Tabuleiro)xmlSerializer.Deserialize(textReader);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;
using System.IO;
using ChessTonGame.Classes.Pieces;
using ChessTonGame.Classes.Events;

namespace ChessTonGame.Classes
{
    public class Board //: ICloneable
    {
        private Graphics g;
        internal int tamanhoCasa = 32;
        private Bitmap bmp;
        private Piece _pecaSelecionada = null;
        //private List<Casa> _todasCasas;
        private List<List<Square>> _casas;
        private bool _brancasEmbaixo = true;
        private bool _highlightCheckedPieces = true;
        private GameMode _modoJogo = GameMode.ShiftTurns;
        private ElementColor _vezDaCor = ElementColor.Branca;

        public string UniqueId { get; set; }
        public List<List<Square>> Casas
        {
            get
            {
                return _casas;
            }
        }

        public ElementColor VezDaCor
        { get { return _vezDaCor; } }
        public bool BrancasEmbaixo
        {
            get
            {
                return _brancasEmbaixo;
            }
        }

        public Piece PecaSelecionada
        { get { return _pecaSelecionada; } }

        public IEnumerable<Square> TodasCasas()
        {
            foreach (var listasPositions in _casas)
            {
                foreach (var position in listasPositions)
                {
                    yield return position;
                }
            }

        }

        public List<Piece> getTodasPecas()
        {
            return (from casa in this.TodasCasas() where casa.PecaAtual != null select casa.PecaAtual).ToList();
        }

        public Square getCasa(int ixColuna, int ixLinha)
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
                    Square casa = _casas[ixLinha][ixColuna];
                    g.FillRectangle(new SolidBrush(casa.Color), new Rectangle(casa.ColumnIndex * tamanhoCasa, casa.LineIndex * tamanhoCasa, tamanhoCasa, tamanhoCasa));

                    if (casa.PecaAtual != null)
                    {
                        if (this._highlightCheckedPieces && casa.PecaAtual.IsInCheck())
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

        public List<Piece> PecasInimigasDe(Piece p)
        {
            ElementColor cor = ElementColor.Preta;
            if (p.Cor == ElementColor.Preta)
            {
                cor = ElementColor.Branca;
            }

            return (from peca in this.getTodasPecas() where peca.Cor == cor select peca).ToList();
        }

        public List<Piece> PecasAmigasDe(Piece p)
        {
            //retorna todas as peças amigas diferentes da própria peça
            return (from peca in this.getTodasPecas() where peca.Cor == p.Cor && peca != p select peca).ToList();
        }

        public void DeclaraXequeMate(ElementColor cor)
        {
            //disparar Evento de XequeMate para a cor informada
        }


        public Board(int colunas, int linhas, bool brancasEmBaixo, GameMode modoJogo)
        {
            bmp = new Bitmap(colunas * tamanhoCasa, linhas * tamanhoCasa);
            g = Graphics.FromImage(bmp);
            ElementColor cor = ElementColor.Branca;
            _casas = new List<List<Square>>();
            this.Movimentos = new Movements();
            this.Movimentos.OnMovementAdded += Movimentos_OnAdd;
            //    _todasCasas = new List<Casa>();
            _modoJogo = modoJogo;
            this.UniqueId = Guid.NewGuid().ToString();

            this._brancasEmbaixo = brancasEmBaixo;
            for (int ixLinha = 0; ixLinha < linhas; ixLinha++)
            {
                List<Square> listaColunasNaLinha = new List<Square>();
                for (int ixColuna = 0; ixColuna < colunas; ixColuna++)
                {
                    Square c = new Square(cor, ixColuna, ixLinha, this);
                    if (cor == ElementColor.Branca)
                    {
                        cor = ElementColor.Preta;
                    }
                    else
                    {
                        cor = ElementColor.Branca;
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
                    //  _todasCasas.Add(c);

                }


                _casas.Add(listaColunasNaLinha);
                if (cor == ElementColor.Preta)
                {
                    cor = new ElementColor();
                    cor = ElementColor.Branca;
                }
                else
                {
                    cor = new ElementColor();
                    cor = ElementColor.Preta;
                }

            }

            for (int ixLinha = 0; ixLinha < linhas; ixLinha++)
            {
                List<Square> listaColunasNaLinha = new List<Square>();
                for (int ixColuna = 0; ixColuna < colunas; ixColuna++)
                {
                    Square c = getCasa(ixColuna, ixLinha);
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

        private void Movimentos_OnAdd(Movement m)
        {
            if (PieceMoved != null)
            {
                PieceMoved(m);
            }
        }

        public void Click(int x, int y)
        {
            //let us find the spot corresponding to the position;
            int xIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(x / this.tamanhoCasa)));
            int yIndex = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(y / this.tamanhoCasa)));
            Square c = this.getCasa(xIndex, yIndex);
            if (this._pecaSelecionada == null)
            {
                if (c != null && c.ehVezDaPecaNaCasa())
                {
                    this._pecaSelecionada = c.SelecionarPeca();
                }
            }
            else
            {
                if (c != this._pecaSelecionada.CasaAtual && this._pecaSelecionada.PodeMoverPara(c))
                {
                    this._pecaSelecionada.MoverPara(c);
                    if (this._modoJogo == GameMode.ShiftTurns)
                    {
                        if (this.VezDaCor == ElementColor.Preta)
                        {
                            this._vezDaCor = ElementColor.Branca;
                        }
                        else
                        {
                            this._vezDaCor = ElementColor.Preta;
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

        public Movements Movimentos { get; set; }
        public event PieceMovedEventHandler PieceMoved;
        public event PieceMovedEventHandler MovementUndone;

        public void UndoLastMovement()
        {
            if (this.Movimentos != null && this.Movimentos.Count > 0)
            {
                var lastMovement = this.Movimentos.LastOrDefault();
                this.Movimentos?.Remove(lastMovement);
                //returning moved piece to its origianl position
                lastMovement.CasaOrigem.PecaAtual = lastMovement.Peca;
                lastMovement.Peca.CasaAtual = lastMovement.CasaOrigem;

                //returning captured pieces:
                lastMovement.CasaDestino.PecaAtual = lastMovement.PecaAnterior;
                if (lastMovement.PecaAnterior != null)
                {
                    lastMovement.PecaAnterior.CasaAtual = lastMovement.CasaDestino;
                    lastMovement.Peca.DevolverPecaComida(lastMovement.PecaAnterior);
                }

                if (this._modoJogo == GameMode.ShiftTurns)
                {
                    if (this.VezDaCor == ElementColor.Preta)
                    {
                        this._vezDaCor = ElementColor.Branca;
                    }
                    else
                    {
                        this._vezDaCor = ElementColor.Preta;
                    }
                }


                MovementUndone?.Invoke(lastMovement);

            }

        }

        //public Tabuleiro getTabuleiroHipotetico()
        //{
        //    Tabuleiro hipotetico = (Tabuleiro)this.Clone();
        //    var todasCasas = hipotetico.TodasCasas().ToList();
        //    foreach (var casa in todasCasas)
        //    {
        //        Casa newCasa = (Casa)casa.Clone();
        //        newCasa.setContainingBoard(this);
        //        for (int j = 0; j < hipotetico.Casas.Count; j++)
        //        {
        //            var listsPositions = hipotetico.Casas[j];
        //            for (int i = 0; i < listsPositions.Count; i++)
        //            {
        //                var position = listsPositions[i];
        //                if (position == casa)
        //                {
        //                    listsPositions[i] = newCasa;
        //                    if (newCasa.PecaAtual != null)
        //                    {
        //                        newCasa.PecaAtual = (Peca)newCasa.PecaAtual.Clone();
        //                        newCasa.PecaAtual.CasaAtual = newCasa;
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return hipotetico;
        //}

        //public object Clone()
        //{
        //    var clone = (Tabuleiro)this.MemberwiseClone();
        //    return clone;
        //}
    }
}

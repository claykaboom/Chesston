using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ChessTonGame.Classes
{
    public class Square : IComparable<Square>//, ICloneable
    {
        public Square()
        { }
        public Square(ElementColor c, int numeroColuna, int numeroLinha, Board tabuleiro)
        {
            this.cor = c;
            this.numeroColuna = numeroColuna;
            this.numeroLinha = numeroLinha;
            this.tabuleiro = tabuleiro;
            this.UniqueId = Guid.NewGuid().ToString();

        }

        public void setContainingBoard(Board t)
        {
            this.tabuleiro = t;
        }

        private Square _casaDireita;
        private Square casaEsquerda;
        private Square _casaSuperior;
        private Square casaInferior;
        private Board tabuleiro;
      //  private string nome;
        private ElementColor cor;
        private Piece pecaAtual;

        private int numeroLinha;
        private int numeroColuna;

        public string UniqueId { get; set; }
        public Piece PecaAtual
        {
            get
            {
                return pecaAtual;
            }
            set
            {
                pecaAtual = value;
            }
        }

        public Board Tabuleiro
        {
            get
            {
                return tabuleiro;
            }
        }

        public int LineIndex
        {
            get
            {
                return numeroLinha;
            }
        }

        public int ColumnIndex
        {
            get
            {
                return numeroColuna;
            }
        }


        public ElementColor Cor
        {
            get
            {
                return cor;
            }
            set
            {
                cor = value;
            }
        }

        public Color Color
        {
            get
            {
                if (this.cor == ElementColor.Branca)
                {
                    return Color.Moccasin;
                }
                return Color.LightGray;

            }
        }

        //public string Nome
        //{
        //    get
        //    {
        //        return nome;
        //    }
        //}


        public Square CasaDireita
        {
            get
            {
                return _casaDireita;
            }
            set
            {
                _casaDireita = value;
            }
        }

        public Square CasaEsquerda
        {
            get
            {
                return casaEsquerda;
            }
            set
            {
                casaEsquerda = value;
            }
        }

        public Square CasaSuperior
        {
            get
            {
                return _casaSuperior;
            }
            set
            {
                _casaSuperior = value;
            }
        }

        public Square CasaInferior
        {
            get
            {
                return casaInferior;
            }
            set
            {
                casaInferior = value;
            }
        }

        public bool ehVezDaPecaNaCasa()
        {
            return (this.pecaAtual != null && tabuleiro.VezDaCor == this.pecaAtual.Cor);

        }
        public Square CasaSuperiorEsquerda
        {
            get
            {
                if (_casaSuperior != null && _casaSuperior.CasaEsquerda != null)
                {
                    return _casaSuperior.CasaEsquerda;
                }
                return null;
            }
        }
        public Square CasaSuperiorDireita
        {
            get
            {

                if (_casaSuperior != null && _casaSuperior.CasaDireita != null)
                {
                    return _casaSuperior.CasaDireita;
                }
                return null;
            }
        }
        public Square CasaInferiorEsquerda
        {
            get
            {
                if (casaInferior != null && casaInferior.CasaEsquerda != null)
                {
                    return casaInferior.CasaEsquerda;
                }
                return null;
            }
        }
        public Square CasaInferiorDireita
        {
            get
            {

                if (casaInferior != null && casaInferior.CasaDireita != null)
                {
                    return casaInferior.CasaDireita;
                }
                return null;
            }
        }

        internal Piece SelecionarPeca()
        {
            this.tabuleiro.DeselecionarPecas();
            if (this.pecaAtual != null)
            {
                this.pecaAtual.Selecionar();
                return this.pecaAtual;
            }
            return null;
        }

        public int CompareTo(Square other)
        {
            if (other == this)
            {
                return 0;
            }
            return -1; //TODO: or +1 if there's need;
        }

        //public object Clone()
        //{
        //    var clone = (Casa)this.MemberwiseClone();
        //    return clone;
        //}
    }
}

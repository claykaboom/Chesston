using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ChessTonGame.Classes
{
    public class Casa:IComparable<Casa>
    {

        public Casa(CorElemento c, int numeroColuna, int numeroLinha, Tabuleiro tabuleiro)
        {
            this.cor = c;
            this.numeroColuna = numeroColuna;
            this.numeroLinha = numeroLinha;
            this.tabuleiro = tabuleiro;

        }

        private Casa _casaDireita;
        private Casa casaEsquerda;
        private Casa _casaSuperior;
        private Casa casaInferior; 
        private Tabuleiro tabuleiro;
        private string nome;
        private CorElemento cor;
        private Peca pecaAtual;

        private int numeroLinha;
        private int numeroColuna;

        public Peca PecaAtual
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

        public Tabuleiro Tabuleiro
        {
            get
            {
                return tabuleiro;
            }
        }

        public int NumeroLinha
        {
            get
            {
                return numeroLinha;
            }
        }

        public int NumeroColuna
        {
            get
            {
                return numeroColuna;
            }
        }


        public CorElemento Cor
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
                if (this.cor == CorElemento.Branca)
                {
                    return Color.White;
                }
                return Color.Black;

            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }
        }


        public Casa CasaDireita
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

        public Casa CasaEsquerda
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

        public Casa CasaSuperior
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

        public Casa CasaInferior
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


        public Casa CasaSuperiorEsquerda
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
        public Casa CasaSuperiorDireita
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
        public Casa CasaInferiorEsquerda
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
        public Casa CasaInferiorDireita
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

        internal Peca SelecionarPeca()
        {
            this.tabuleiro.DeselecionarPecas();
            if (this.pecaAtual != null)
            {
                this.pecaAtual.Selecionar();
                return this.pecaAtual;
            }
            return null;
        }
  
        public int CompareTo(Casa other)
        {
          if(other == this )
          {
              return 0;
          }
          return -1; //TODO: or +1 if there's need;
        }
    }
}

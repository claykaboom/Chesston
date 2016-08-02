using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public abstract class Peca
    {


        public Peca(CorElemento cor, Casa casaAtual)
        {
            this._cor = cor;
            this._casaAtual = casaAtual;

            this._tabuleiro = casaAtual.Tabuleiro;

            if (this._tabuleiro.BrancasEmbaixo)
            {
                if (cor == CorElemento.Branca)
                {
                    _perspectivaDeBaixo = true;
                }
                else
                {
                    _perspectivaDeBaixo = false;
                }
            }
            else
            {
                if (cor == CorElemento.Preta)
                {
                    _perspectivaDeBaixo = true;
                }
                else
                {
                    _perspectivaDeBaixo = false;
                }
            }

            if (casaAtual != null)
            {
                this._casaAtual.PecaAtual = this;
            }
        }

        private Casa _casaAtual;
        private CorElemento _cor;
        private Tabuleiro _tabuleiro;
        private List<Peca> _pecasComidas;
        private decimal _pontos = 0;
        private bool _estaSelecionada = false;
        private bool _jaMoveu = false;
        private bool _perspectivaDeBaixo = false;
        public abstract bool EstaEmXeque { get; }
        public abstract decimal ValorPontos { get; }

        public decimal Pontos
        {
            get
            {
                return _pontos;
            }
        }

        public bool EstaSelecionada
        {
            get
            {
                return _estaSelecionada;

            }

        }

        public List<Peca> QuemDeuXeque()
        {
            return (
                        from
                            peca
                        in
                            this._tabuleiro.PecasInimigasDe(this)
                        where
                            peca.PodeMoverPara(this._casaAtual)
                        select peca
                    ).ToList();
        }

        public abstract List<List<Passo>> getRotasPossiveis();


        public bool JaMoveu
        {
            get
            { return _jaMoveu; }
        }

        public List<Casa> getCasasPorRota()
        {
            List<Casa> returnTargets = new List<Casa>();
            foreach (var rota in this.getRotasPossiveis())
            {
                if (this._perspectivaDeBaixo)
                {
                    Casa casaAtualVerificacao = this.CasaAtual;
                    for (int i = 0; i < rota.Count; i++)
                    {
                        Passo passo = rota[i];
                        if (casaAtualVerificacao == null)
                        {
                            break;
                        }
                        switch (passo)
                        {
                            case Passo.Frente:
                                casaAtualVerificacao = casaAtualVerificacao.CasaSuperior;
                                //do not add now to the list
                                break;
                            case Passo.Tras:
                                casaAtualVerificacao = casaAtualVerificacao.CasaInferior;
                                //do not add now to the list
                                break;
                            case Passo.Direita:
                                casaAtualVerificacao = casaAtualVerificacao.CasaDireita;
                                //do not add now to the list
                                break;
                            case Passo.Esquerda:
                                casaAtualVerificacao = casaAtualVerificacao.CasaEsquerda;
                                //do not add now to the list
                                break;
                            case Passo.FrenteIndefinido:
                                //get all places ahead
                                while (casaAtualVerificacao != null)
                                {
                                    casaAtualVerificacao = casaAtualVerificacao.CasaSuperior;
                                    if (casaAtualVerificacao != null)
                                    {
                                        returnTargets.Add(casaAtualVerificacao);
                                    }
                                }
                                break;
                            case Passo.TrasIndefinido:
                                //get all places behind
                                while (casaAtualVerificacao != null)
                                {
                                    casaAtualVerificacao = casaAtualVerificacao.CasaInferior;
                                    if (casaAtualVerificacao != null)
                                    {
                                        returnTargets.Add(casaAtualVerificacao);
                                    }
                                }
                                break;
                            case Passo.DireitaIndefinido:
                                //get all places to the right
                                while (casaAtualVerificacao != null)
                                {
                                    casaAtualVerificacao = casaAtualVerificacao.CasaDireita;
                                    if (casaAtualVerificacao != null)
                                    {
                                        returnTargets.Add(casaAtualVerificacao);
                                    }
                                }
                                break;
                            case Passo.EsquerdaIndefinido:
                                //get all places to the right
                                while (casaAtualVerificacao != null)
                                {
                                    casaAtualVerificacao = casaAtualVerificacao.CasaEsquerda;
                                    if (casaAtualVerificacao != null)
                                    {
                                        returnTargets.Add(casaAtualVerificacao);
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        if (i == rota.Count() - 1)
                        {
                            //adicionamos se nao foi nulo, pq nos casos de movimentos indefinidos, vai retornar nulo e eles ja foram adicionados na lista
                            if (casaAtualVerificacao != null)
                            {
                                returnTargets.Add(casaAtualVerificacao);
                            }
                        }

                    }

                }
                else //outra perspectiva, tabuleiro virado para essa peça / another perspective, board turned for this piece
                {

                    Casa casaAtualVerificacao = this.CasaAtual;
                    for (int i = 0; i < rota.Count; i++)
                    {
                        Passo passo = rota[i];
                        if (casaAtualVerificacao == null)
                        {
                            break;
                        }
                        switch (passo)
                        {
                            case Passo.Frente:
                                casaAtualVerificacao = casaAtualVerificacao.CasaInferior;
                                break;
                            case Passo.Tras:
                                casaAtualVerificacao = casaAtualVerificacao.CasaSuperior;
                                break;
                            case Passo.Direita:
                                casaAtualVerificacao = casaAtualVerificacao.CasaEsquerda;
                                break;
                            case Passo.Esquerda:
                                casaAtualVerificacao = casaAtualVerificacao.CasaDireita;
                                break;
                            case Passo.FrenteIndefinido:
                                //get all places ahead
                                while (casaAtualVerificacao != null)
                                {
                                    casaAtualVerificacao = casaAtualVerificacao.CasaInferior;
                                    if (casaAtualVerificacao != null)
                                    {
                                        returnTargets.Add(casaAtualVerificacao);
                                    }
                                }
                                break;
                            case Passo.TrasIndefinido:
                                //get all places behind
                                while (casaAtualVerificacao != null)
                                {
                                    casaAtualVerificacao = casaAtualVerificacao.CasaSuperior;
                                    if (casaAtualVerificacao != null)
                                    {
                                        returnTargets.Add(casaAtualVerificacao);
                                    }
                                }
                                break;
                            case Passo.DireitaIndefinido:
                                //get all places to the left
                                while (casaAtualVerificacao != null)
                                {
                                    casaAtualVerificacao = casaAtualVerificacao.CasaEsquerda;
                                    if (casaAtualVerificacao != null)
                                    {
                                        returnTargets.Add(casaAtualVerificacao);
                                    }
                                }
                                break;
                            case Passo.EsquerdaIndefinido:
                                //get all places to the right
                                while (casaAtualVerificacao != null)
                                {
                                    casaAtualVerificacao = casaAtualVerificacao.CasaDireita;
                                    if (casaAtualVerificacao != null)
                                    {
                                        returnTargets.Add(casaAtualVerificacao);
                                    }
                                }
                                break;
                            default:
                                break;
                        }

                        if (i == rota.Count() - 1)
                        {
                            //adicionamos se nao foi nulo, pq nos casos de movimentos indefinidos, vai retornar nulo e eles ja foram adicionados na lista
                            if (casaAtualVerificacao != null)
                            {
                                returnTargets.Add(casaAtualVerificacao);
                            }
                        }

                    }

                }
            }
            return returnTargets.ToList().Distinct().ToList();

        }

        public bool PodeMoverPara(Casa casa)
        {
            if (casa == this.CasaAtual)
            {
                return false;
            }
            return ((from casaDestino in this.getCasasPorRota() where casaDestino == casa select casaDestino).FirstOrDefault() != null);
        }


        public abstract Image getImage();
        public int getTamanhoPeca()
        {
            return this._tabuleiro.tamanhoCasa;
        }

        public List<Casa> getCasasPossiveis()
        {
            return
                (
                    from
                        casa
                    in
                        this._tabuleiro.TodasCasas
                    where
                        (this.PodeMoverPara(casa) == true)
                    select casa
                 ).ToList();
        }

        public abstract bool FicaEmXequeNaCasa(Casa casa);
        public bool FicaEmXequeSePecaEstiverNaCasa(Peca peca, Casa casa)
        {
            //este provavelmente vai ser um dos mais dificeis metodos de se construir
            return false;
        }

        public void MoverPara(Casa casa)
        {
            if (this.PodeMoverPara(casa))
            {
                if (casa.PecaAtual != null) // está cheia
                {
                    this.Comer(casa.PecaAtual);
                }
                this.CasaAtual.PecaAtual = null;
                this._casaAtual = casa;
                casa.PecaAtual = this;
                this._jaMoveu = true;
                this._tabuleiro.DeselecionarPecas();
            }

        }

        public List<Peca> PecasQuePodemSalvarDoXeque()
        {
            List<Peca> pecasSalvadoras = new List<Peca>();
            if (this.EstaEmXeque)
            {
                List<Peca> pecasAmigas = this._tabuleiro.PecasAmigasDe(this);

            }

            return pecasSalvadoras;
        }

        public void Selecionar()
        {
            this._estaSelecionada = true;
        }


        public void Deselecionar()
        { this._estaSelecionada = false; }
        public void Comer(Peca p)
        {
            if (_pecasComidas == null)
                _pecasComidas = new List<Peca>();
            this._pontos = p.ValorPontos;
            this._pecasComidas.Add(p);
        }

        public Casa CasaAtual
        {
            get
            {
                return _casaAtual;
            }
            set
            {
                _casaAtual = value;
            }
        }

        public CorElemento Cor
        {
            get
            {
                return _cor;
            }
        }
    }
}

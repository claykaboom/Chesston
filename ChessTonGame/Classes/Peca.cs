using ChessTonGame.Classes.Events;
using ChessTonGame.Classes.Pecas;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public abstract class Peca //: ICloneable
    {

        public Peca(CorElemento cor, Casa casaAtual, bool pulaOutrasPecas)
        {
            this._cor = cor;
            this._casaAtual = casaAtual;
            this._pulaOutrasPecas = pulaOutrasPecas;
            this._tabuleiro = casaAtual.Tabuleiro;
            this.UniqueId = Guid.NewGuid().ToString();
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

        public string UniqueId { get; set; }
        private Casa _casaAtual;
        private CorElemento _cor;
        protected Tabuleiro _tabuleiro;
        protected List<Peca> _pecasComidas;
        private bool _estaSelecionada = false;
        private bool _jaMoveu = false;
        private bool _perspectivaDeBaixo = false;
        public bool EstaEmXeque()
        {
            return QuemDeuXeque().Count > 0;

        }
        public abstract decimal ValorPontos { get; }
        private bool _pulaOutrasPecas;

        public bool PulaOutrasPecas
        {
            get { return _pulaOutrasPecas; }
            set { _pulaOutrasPecas = value; }
        }

        public decimal getPontos()
        {
            if (this._pecasComidas == null)
                return 0;
            return this._pecasComidas.Sum(p => p.ValorPontos);
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

        public event PieceMovedEventHandler PieceMoved;

        public bool JaMoveu()
        {
            return (from moves in _tabuleiro.Movimentos where moves.Peca == this select moves).Count() > 0;
        }

        public bool ehInimigaDe(Peca p)
        {
            return p.Cor != this.Cor;
        }

        private Casa getCasaByPasso(Casa casaAtualVerificacao, Passo passo)
        {
            Casa returningPosition = null;
            if (this._perspectivaDeBaixo)
            {
                switch (passo)
                {
                    case Passo.Frente:
                        returningPosition = casaAtualVerificacao.CasaSuperior;

                        break;
                    case Passo.Tras:
                        returningPosition = casaAtualVerificacao.CasaInferior;
                        break;
                    case Passo.Direita:
                        returningPosition = casaAtualVerificacao.CasaDireita;
                        break;
                    case Passo.Esquerda:
                        returningPosition = casaAtualVerificacao.CasaEsquerda;
                        break;

                    /////DIAGONALS


                    case Passo.DiagonalDireitaFrente:
                        returningPosition = casaAtualVerificacao.CasaSuperiorDireita;

                        break;
                    case Passo.DiagonalEsquerdaFrente:
                        returningPosition = casaAtualVerificacao.CasaSuperiorEsquerda;

                        break;
                    case Passo.DiagonalDireitaTras:
                        returningPosition = casaAtualVerificacao.CasaInferiorDireita;

                        break;
                    case Passo.DiagonalEsquerdaTras:
                        returningPosition = casaAtualVerificacao.CasaInferiorEsquerda;
                        break;

                }

            }
            else //outra perspectiva, tabuleiro virado para essa peça / another perspective, board turned for this piece
            {
                switch (passo)
                {
                    case Passo.Frente:
                        returningPosition = casaAtualVerificacao.CasaInferior;
                        break;
                    case Passo.Tras:
                        returningPosition = casaAtualVerificacao.CasaSuperior;
                        break;
                    case Passo.Direita:
                        returningPosition = casaAtualVerificacao.CasaEsquerda;
                        break;
                    case Passo.Esquerda:
                        returningPosition = casaAtualVerificacao.CasaDireita;
                        break;
                    /////DIAGONALS 
                    case Passo.DiagonalDireitaFrente:
                        returningPosition = casaAtualVerificacao.CasaInferiorEsquerda;
                        break;
                    case Passo.DiagonalEsquerdaFrente:
                        returningPosition = casaAtualVerificacao.CasaInferiorDireita;
                        break;
                    case Passo.DiagonalDireitaTras:
                        returningPosition = casaAtualVerificacao.CasaSuperiorEsquerda;
                        break;
                    case Passo.DiagonalEsquerdaTras:
                        returningPosition = casaAtualVerificacao.CasaSuperiorDireita;
                        break;
                }

            }

            if (returningPosition != null && returningPosition.PecaAtual != null && returningPosition.PecaAtual.Cor == this.Cor && !this.PulaOutrasPecas)
            {
                return null;
            }
            return returningPosition; //no previous case
        }

        public List<Casa> getCasasPorPassos(List<Passo> passosPossiveis)
        {
            return getCasasPorRota(new List<List<Passo>>() { passosPossiveis });
        }


        public Casa getCasaPorPassos(List<Passo> passosPossiveis)
        {
            return getCasasPorPassos(passosPossiveis).FirstOrDefault();
        }

        public List<Casa> getCasasPorRota(List<List<Passo>> rotasPossiveis)
        {

            List<Casa> returnTargets = new List<Casa>();
            foreach (var rota in rotasPossiveis)
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


                        case Passo.FrenteIndefinido:
                            //get all places ahead
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Passo.Frente);
                            break;
                        case Passo.TrasIndefinido:
                            //get all places behind 
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Passo.Tras);
                            break;
                        case Passo.DireitaIndefinido:
                            //get all places to the right
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Passo.Direita);
                            break;
                        case Passo.EsquerdaIndefinido:
                            //get all places to the right

                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Passo.Esquerda);
                            break;
                        /////DIAGONALS 

                        case Passo.DiagonalDireitaFrenteIndefinido:
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Passo.DiagonalDireitaFrente);
                            break;
                        case Passo.DiagonalEsquerdaFrenteIndefinido:
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Passo.DiagonalEsquerdaFrente);
                            break;
                        case Passo.DiagonalDireitaTrasIndefinido:

                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Passo.DiagonalDireitaTras);
                            break;
                        case Passo.DiagonalEsquerdaTrasIndefinido:

                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Passo.DiagonalEsquerdaTras);
                            break;

                        default:
                            casaAtualVerificacao = getCasaByPasso(casaAtualVerificacao, passo);
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

            return returnTargets.ToList().Distinct().ToList();

        }


        private Casa getCasasNaRota(List<Casa> returnTargets, Casa casaAtualVerificacao, Passo p)
        {
            while (casaAtualVerificacao != null)
            {
                casaAtualVerificacao = getCasaByPasso(casaAtualVerificacao, p);
                if (casaAtualVerificacao != null)
                {
                    if (casaAtualVerificacao.PecaAtual != null
                        && !this.PulaOutrasPecas) //só pode comer se for de cor diferente
                    {
                        if (casaAtualVerificacao.PecaAtual.ehInimigaDe(this))
                        {
                            returnTargets.Add(casaAtualVerificacao);
                        }
                        casaAtualVerificacao = null;
                        break;//we add and exit
                    }
                    else
                    {
                        returnTargets.Add(casaAtualVerificacao);
                    }
                }
            }

            return casaAtualVerificacao;
        }

        public bool PodeMoverPara(Casa casa)
        {
            if (casa == this.CasaAtual)
            {
                return false;
            }
            return ((from casaDestino in this.getCasasPorRota(this.getRotasPossiveis()) where casaDestino == casa select casaDestino).FirstOrDefault() != null);
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
                        this._tabuleiro.TodasCasas()
                    where
                        (this.PodeMoverPara(casa) == true)
                    select casa
                 ).ToList();
        }

        public bool FicaEmXequeNaCasa(Casa casa)
        {

            return false;
            //  // para essa verificacao vou ter q ter uma nova instancia de tabuleiro, exatamente igual
            //  var cloneBoard = this._tabuleiro.getTabuleiroHipotetico();
            //  // faco operacoes hipoteticas ali
            //  var minhaCasaReal = this.CasaAtual;
            //  var minhaCasaHipotetica = cloneBoard.getCasa(minhaCasaReal.ColumnIndex, minhaCasaReal.LineIndex);
            //  var casaHipoteticaDestino = cloneBoard.getCasa(casa.ColumnIndex, casa.LineIndex);
            //  var minhaVersaoHipotetica = minhaCasaHipotetica.PecaAtual;
            ////  if (!(this is Rei) && minhaVersaoHipotetica.PodeMoverPara(casaHipoteticaDestino))
            //  {
            //      minhaVersaoHipotetica.MoverPara(casaHipoteticaDestino);
            //      return minhaVersaoHipotetica.EstaEmXeque();
            //  }
        }

        public void MoverPara(Casa casaDestino)
        {
            if (this.PodeMoverPara(casaDestino))
            {
                var m = new Movement(this, this.CasaAtual, casaDestino);
                if (casaDestino.PecaAtual != null) // está cheia
                {
                    this.Comer(casaDestino.PecaAtual);
                }

                this.CasaAtual.PecaAtual = null;
                this._casaAtual = casaDestino;
                casaDestino.PecaAtual = this;
                this._jaMoveu = true;


                if (PieceMoved != null)
                {
                    PieceMoved(m);
                }
                this._tabuleiro.Movimentos.Add(m);
            }

        }

        public List<Movement> getMovements()
        {
            return this._tabuleiro.Movimentos.Where(m => m.Peca == this).ToList();
        }

        public List<Peca> PecasQuePodemSalvarDoXeque()
        {
            List<Peca> pecasSalvadoras = new List<Peca>();
            if (this.EstaEmXeque())
            {
                List<Peca> pecasAmigas = this._tabuleiro.PecasAmigasDe(this).OrderBy(p => p.ValorPontos).ToList(); // we try to protect the current piece first with the lowest value pieces
                //lets perform every possible movement for every piece
                foreach (var piece in pecasAmigas)
                {
                    //fazemos todos os possiveis movimentos
                    foreach (var casa in piece.getCasasPossiveis())
                    {
                        piece.MoverPara(casa);
                        if (!this.EstaEmXeque())
                        {
                            pecasSalvadoras.Add(piece);

                        }
                        _tabuleiro.UndoLastMovement();
                    }
                }
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
            p.CasaAtual.PecaAtual = null;
            this._pecasComidas.Add(p);
        }

        public void DevolverPecaComida(Peca p)
        {
            if (_pecasComidas != null)
            {
                this._pecasComidas.Remove(p);
            }

        }

        //public object Clone()
        //{
        //    var clone = (Peca)this.MemberwiseClone();
        //    return clone;
        //}


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

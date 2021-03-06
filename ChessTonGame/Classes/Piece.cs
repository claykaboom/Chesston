﻿using ChessTonGame.Classes.Events;
using ChessTonGame.Classes.Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public abstract class Piece //: ICloneable
    {

        public Piece(ElementColor cor, Square casaAtual, bool pulaOutrasPecas, bool canMoveToCheckPosition, bool canBeInCheckAfterFriendlyMove, bool isGameOverIfCantBeSavedFromCheck)
        {
            this._cor = cor;
            this._casaAtual = casaAtual;
            this._pulaOutrasPecas = pulaOutrasPecas;
            this._board = casaAtual.Tabuleiro;
            this._canMoveToCheckPosition = canMoveToCheckPosition;
            this._canBeInCheckAfterFriendlyMove = canBeInCheckAfterFriendlyMove;
            this._isGameOverIfCantBeSavedFromCheck = isGameOverIfCantBeSavedFromCheck;
            this.UniqueId = Guid.NewGuid().ToString();


            this._board.PieceStartedMoving += _board_PieceMoved;


            if (this._board.BrancasEmbaixo)
            {
                if (cor == ElementColor.Branca)
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
                if (cor == ElementColor.Preta)
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

        private void _board_PieceMoved(Movement m)
        {
            if (m.Peca.Cor == this.Cor)//it is a friendly move
            {
                if (!this._canBeInCheckAfterFriendlyMove && this.IsInCheck())
                {
                    //last move should be aborted due to this pieces restriction.
                    this._board.UndoLastMovement();
                }

            }
            if (_isGameOverIfCantBeSavedFromCheck && this.IsInCheck() && this.PecasQuePodemSalvarDoXeque().Count() == 0)
            {
                _board.DeclaraXequeMate(this.Cor);
            }
        }

        public string UniqueId { get; set; }
        private Square _casaAtual;
        private ElementColor _cor;
        protected Board _board;
        protected List<Piece> _pecasComidas;
        private bool _estaSelecionada = false;
        private bool _jaMoveu = false; //TODO: Maybe it can be computed so as to have a clear record of moves based on move records.
        private bool _perspectivaDeBaixo = false;
        private bool _canMoveToCheckPosition = true;
        private bool _canBeInCheckAfterFriendlyMove = true;
        private bool _isGameOverIfCantBeSavedFromCheck = false;
        public bool IsInCheck()
        {
            return QuemDeuXeque().Count > 0;

        }
        public abstract decimal ValueInPoints { get; }
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
            return this._pecasComidas.Sum(p => p.ValueInPoints);
        }

        public bool EstaSelecionada
        {
            get
            {
                return _estaSelecionada;

            }

        }

        public List<Piece> QuemDeuXeque()
        {
            return (
                        from
                            peca
                        in
                            this._board.PecasInimigasDe(this)
                        where
                            peca.PodeMoverPara(this._casaAtual)
                        select peca
                    ).ToList();
        }

        public abstract List<List<Step>> getPossibleRoutes();

        public event PieceMovedEventHandler PieceMoved;

        public bool JaMoveu()
        {
            return (from moves in _board.Movimentos where moves.Peca == this select moves).Count() > 0;
        }

        public bool ehInimigaDe(Piece p)
        {
            return p.Cor != this.Cor;
        }

        private Square getCasaByPasso(Square casaAtualVerificacao, Step passo)
        {
            Square returningPosition = null;
            if (this._perspectivaDeBaixo)
            {
                switch (passo)
                {
                    case Step.Front:
                        returningPosition = casaAtualVerificacao.CasaSuperior;

                        break;
                    case Step.Back:
                        returningPosition = casaAtualVerificacao.CasaInferior;
                        break;
                    case Step.Right:
                        returningPosition = casaAtualVerificacao.CasaDireita;
                        break;
                    case Step.Left:
                        returningPosition = casaAtualVerificacao.CasaEsquerda;
                        break;

                    /////DIAGONALS


                    case Step.DiagonalRightFront:
                        returningPosition = casaAtualVerificacao.CasaSuperiorDireita;

                        break;
                    case Step.DiagonalLeftFront:
                        returningPosition = casaAtualVerificacao.CasaSuperiorEsquerda;

                        break;
                    case Step.DiagonalRightBack:
                        returningPosition = casaAtualVerificacao.CasaInferiorDireita;

                        break;
                    case Step.DiagonalLeftBack:
                        returningPosition = casaAtualVerificacao.CasaInferiorEsquerda;
                        break;

                }

            }
            else //outra perspectiva, tabuleiro virado para essa peça / another perspective, board turned for this piece
            {
                switch (passo)
                {
                    case Step.Front:
                        returningPosition = casaAtualVerificacao.CasaInferior;
                        break;
                    case Step.Back:
                        returningPosition = casaAtualVerificacao.CasaSuperior;
                        break;
                    case Step.Right:
                        returningPosition = casaAtualVerificacao.CasaEsquerda;
                        break;
                    case Step.Left:
                        returningPosition = casaAtualVerificacao.CasaDireita;
                        break;
                    /////DIAGONALS 
                    case Step.DiagonalRightFront:
                        returningPosition = casaAtualVerificacao.CasaInferiorEsquerda;
                        break;
                    case Step.DiagonalLeftFront:
                        returningPosition = casaAtualVerificacao.CasaInferiorDireita;
                        break;
                    case Step.DiagonalRightBack:
                        returningPosition = casaAtualVerificacao.CasaSuperiorEsquerda;
                        break;
                    case Step.DiagonalLeftBack:
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

        public List<Square> getCasasPorPassos(List<Step> passosPossiveis)
        {
            return getCasasPorRota(new List<List<Step>>() { passosPossiveis });
        }


        public Square getSquareBySteps(List<Step> passosPossiveis)
        {
            return getCasasPorPassos(passosPossiveis).FirstOrDefault();
        }

        public List<Square> getCasasPorRota(List<List<Step>> rotasPossiveis)
        {

            List<Square> returnTargets = new List<Square>();
            foreach (var rota in rotasPossiveis)
            {
                Square casaAtualVerificacao = this.CasaAtual;
                for (int i = 0; i < rota.Count; i++)
                {
                    Step passo = rota[i];
                    if (casaAtualVerificacao == null)
                    {
                        break;
                    }
                    switch (passo)
                    {


                        case Step.FrontUndefined:
                            //get all places ahead
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Step.Front);
                            break;
                        case Step.BackUndefined:
                            //get all places behind 
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Step.Back);
                            break;
                        case Step.RightUndefined:
                            //get all places to the right
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Step.Right);
                            break;
                        case Step.LeftUndefined:
                            //get all places to the left

                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Step.Left);
                            break;
                        /////DIAGONALS 

                        case Step.DiagonalRighFrontUndefined:
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Step.DiagonalRightFront);
                            break;
                        case Step.DiagonalLeftFrontUndefined:
                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Step.DiagonalLeftFront);
                            break;
                        case Step.DiagonalRightBackUndefined:

                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Step.DiagonalRightBack);
                            break;
                        case Step.DiagonalLeftBackUndefined:

                            casaAtualVerificacao = getCasasNaRota(returnTargets, casaAtualVerificacao, Step.DiagonalLeftBack);
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


        private Square getCasasNaRota(List<Square> returnTargets, Square casaAtualVerificacao, Step p)
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

        public bool PodeMoverPara(Square casa)
        {
            if (casa == this.CasaAtual)
            {
                return false;
            }
            if (this._canMoveToCheckPosition)
            {
                return (
                    from casaDestino
                        in this.getCasasPorRota(this.getPossibleRoutes())
                    where casaDestino == casa

                    select casaDestino
                        ).FirstOrDefault() != null
                        ;
            }
            else
            {



                return (
                   from casaDestino
                       in this.getCasasPorRota(this.getPossibleRoutes())
                   where casaDestino == casa
                   && (
                        from
                            peca
                        in
                            this._board.PecasInimigasDe(this)
                        where
                            peca.PodeMoverPara(casaDestino)
                        select peca
                    ).ToList().Count == 0
                   select casaDestino
                       ).FirstOrDefault() != null
                       ;
            }
        }


        public abstract Image getImage();
        public int getTamanhoPeca()
        {
            return this._board.tamanhoCasa;
        }

        public List<Square> getCasasPossiveis()
        {
            return
                (
                    from
                        casa
                    in
                        this._board.TodasCasas()
                    where
                        (this.PodeMoverPara(casa) == true)
                    select casa
                 ).ToList();
        }

        public ElementColor getCorInimiga()
        {
            ElementColor cor = ElementColor.Preta;
            if (this.Cor == ElementColor.Preta)
            {
                cor = ElementColor.Branca;
            }

            return cor;
        }



        public Movement MoverPara(Square casaDestino, bool registerMove)
        {
            if (!registerMove)
            {
                if (casaDestino.PecaAtual == null || (casaDestino.PecaAtual != null && casaDestino.PecaAtual.Cor != this.Cor))
                {

                    var m = new Movement(this, this.CasaAtual, casaDestino);
                    this.CasaAtual.PecaAtual = null;
                    this._casaAtual = casaDestino;
                    casaDestino.PecaAtual = this;
                    return m;
                }
            }
            else
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

                    this._board.Movimentos.Add(m);
                    if (PieceMoved != null)
                    {
                        PieceMoved(m);
                    }

                    return m;
                }
            }
            return null;
        }

        public List<Movement> getMovements()
        {
            return this._board.Movimentos.Where(m => m.Peca == this).ToList();
        }

        public List<Piece> PecasQuePodemSalvarDoXeque()
        {
            List<Piece> pecasSalvadoras = new List<Piece>();
            if (this.IsInCheck())
            {
                List<Piece> pecasAmigas = this._board.PecasAmigasDe(this).OrderBy(p => p.ValueInPoints).ToList(); // we try to protect the current piece first with the lowest value pieces

                //maybe it can save itself, so we add this piece as a friend of self:
                pecasAmigas.Add(this);

                //lets perform every possible movement for every piece
                foreach (var piece in pecasAmigas)
                {
                    //fazemos todos os possiveis movimentos
                    foreach (var casa in piece.getCasasPossiveis())
                    {
                        var currentPosition = piece.CasaAtual;

                        var m = piece.MoverPara(casa, false);
                        if (m != null) // if move took place
                        {
                            if (!this.IsInCheck())
                            {
                                pecasSalvadoras.Add(piece);

                            }


                            _board.UndoMove(m);
                        }
                    }
                }
            }

            return pecasSalvadoras.Distinct().ToList();
        }

        public void Selecionar()
        {
            this._estaSelecionada = true;
        }


        public void Deselecionar()
        { this._estaSelecionada = false; }
        public void Comer(Piece p)
        {
            if (_pecasComidas == null)
                _pecasComidas = new List<Piece>();
            p.CasaAtual.PecaAtual = null;
            this._pecasComidas.Add(p);
        }

        public void DevolverPecaComida(Piece p)
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


        public Square CasaAtual
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

        public ElementColor Cor
        {
            get
            {
                return _cor;
            }
        }

        public override string ToString()
        {
            return "|";
        }
    }
}

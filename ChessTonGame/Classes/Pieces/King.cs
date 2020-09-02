using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pieces
{
    public class King : Piece
    {
        public King(ElementColor cor, Square c)
            : base(cor, c, false, false, false, true)
        {
            this.PieceMoved += Rei_PieceMoved;
            this._board.PieceMoved += _board_PieceMoved;
            this._board.MovementUndone += _board_MovementUndone;
        }

        private void _board_MovementUndone(Movement m)
        {
            Tuple<string, Square, Piece> mInfo = null;
            if (m.MovementInfo != null)
            {
                mInfo = (Tuple<string, Square, Piece>)m.MovementInfo;

            }
            if (m.Peca == this && mInfo != null && ( mInfo.Item1 == "O-O" || mInfo.Item1 == "O-O-O" ))
            {
                var rook = mInfo.Item3;
                rook.CasaAtual.PecaAtual = null;

                mInfo.Item2.PecaAtual = rook;
                rook.CasaAtual = mInfo.Item2;
                mInfo.Item2.PecaAtual = rook;
            }
        }

        private void _board_PieceMoved(Movement m)
        {
            if (m.Peca.Cor == this.Cor && this.IsInCheck())
            {
                //  this._board.UndoLastMovement();
            }

        }

        private void Rei_PieceMoved(Movement m)
        {
            if (this.IsInCheck())
            {

                this._board.UndoLastMovement();
            }
            if ((m.CasaOrigem.getHorizontalDistanceTo(m.CasaDestino)) == 2) //roque curto
            { //finalizando o roque curto 
                var rookToTheRight = m.CasaDestino.CasaDireita.PecaAtual;
                rookToTheRight.CasaAtual = m.CasaDestino.CasaEsquerda;
                m.CasaDestino.CasaEsquerda.PecaAtual = rookToTheRight;
                m.CasaDestino.CasaDireita.PecaAtual = null;
                m.SetMovementInfo(this, new Tuple<string, Square, Piece>("O-O", m.CasaDestino.CasaDireita, rookToTheRight));

            }
            else if ((m.CasaOrigem.getHorizontalDistanceTo(m.CasaDestino)) == -2) //roque longo
            { //finalizando o roque curto 
                var rookToTheLeft = m.CasaDestino.CasaEsquerda.CasaEsquerda.PecaAtual;
                rookToTheLeft.CasaAtual = m.CasaDestino.CasaDireita;
                m.CasaDestino.CasaDireita.PecaAtual = rookToTheLeft;
                m.CasaDestino.CasaEsquerda.CasaEsquerda.PecaAtual = null;
                m.SetMovementInfo(this, new Tuple<string, Square, Piece>("O-O-O", m.CasaDestino.CasaEsquerda.CasaEsquerda, rookToTheLeft));

            }
        }


        public override decimal ValueInPoints
        {
            get { return 5; }
        }

        public override List<List<Step>> getPossibleRoutes()
        {
            List<List<Step>> routes = new List<List<Step>>();
            var squareTest = getSquareBySteps(new List<Step>() { Step.Front });
            if (squareTest != null)
            {

                routes.Add(new List<Step>() { Step.Front });
            }
            squareTest = getSquareBySteps(new List<Step>() { Step.Back });
            if (squareTest != null)
            {

                routes.Add(new List<Step>() { Step.Back });
            }

            squareTest = getSquareBySteps(new List<Step>() { Step.Right });
            if (squareTest != null)
            {

                routes.Add(new List<Step>() { Step.Right });
                
                //ROQUE CURTO
                if ((this.Cor == ElementColor.Branca && this._board.BrancasEmbaixo) ||
                    (this.Cor == ElementColor.Preta && !this._board.BrancasEmbaixo)
                    )
                {
                    //se a torre à direita ainda não se moveu e esta peça tb não
                    if (!this.JaMoveu())
                    {

                        var pieceThreeToTheRight = this.CasaAtual.CasaDireita.CasaDireita.CasaDireita.PecaAtual;
                        var isARookThatDidntMove = pieceThreeToTheRight != null && !pieceThreeToTheRight.ehInimigaDe(this) && !pieceThreeToTheRight.JaMoveu();
                        if (isARookThatDidntMove)
                        {
                            //se não tem nenhuma peça inimiga que pode entrar naquela posição:
                            if (squareTest.getPecasDaCorQuePodemMoverPara(this.getCorInimiga(), "K").Count == 0)
                            {
                                squareTest = getSquareBySteps(new List<Step>() { Step.Right, Step.Right });
                                if (squareTest != null)
                                {
                                    //se não tem nenhuma peça inimiga que pode entrar naquela posição:
                                    if (squareTest.getPecasDaCorQuePodemMoverPara(this.getCorInimiga(), "K").Count == 0)
                                    {

                                        routes.Add(new List<Step>() { Step.Right, Step.Right });
                                    }
                                }

                            }
                        }

                    }
                }

                //ROQUE LONGO
                if ((this.Cor == ElementColor.Branca && !this._board.BrancasEmbaixo) ||
                  (this.Cor == ElementColor.Preta && this._board.BrancasEmbaixo)
                  )
                {
                    //se a torre à direita ainda não se moveu e esta peça tb não
                    if (!this.JaMoveu())
                    {

                        var pieceFourToTheLeft = this.CasaAtual.CasaEsquerda.CasaEsquerda.CasaEsquerda.CasaEsquerda.PecaAtual;
                        var isARookThatDidntMove = pieceFourToTheLeft != null && !pieceFourToTheLeft.ehInimigaDe(this) && !pieceFourToTheLeft.JaMoveu();
                        if (isARookThatDidntMove)
                        {
                            //se não tem nenhuma peça inimiga que pode entrar naquela posição:
                            if (squareTest.getPecasDaCorQuePodemMoverPara(this.getCorInimiga(), "K").Count == 0)
                            {
                                squareTest = getSquareBySteps(new List<Step>() { Step.Right, Step.Right });
                                if (squareTest != null)
                                {
                                    //se não tem nenhuma peça inimiga que pode entrar naquela posição:
                                    if (squareTest.getPecasDaCorQuePodemMoverPara(this.getCorInimiga(), "K").Count == 0)
                                    {

                                        routes.Add(new List<Step>() { Step.Right, Step.Right });
                                    }
                                }

                            }
                        }

                    }
                }


            }

            squareTest = getSquareBySteps(new List<Step>() { Step.Left });
            if (squareTest != null)
            {

                routes.Add(new List<Step>() { Step.Left });

                //ROQUE CURTO
                if ((this.Cor == ElementColor.Branca && !this._board.BrancasEmbaixo) ||
                    (this.Cor == ElementColor.Preta && this._board.BrancasEmbaixo)
                    )
                {
                    //se a torre à esquerda ainda não se moveu e esta peça tb não
                    if (!this.JaMoveu())
                    {

                        var pieceThreeToTheRight = this.CasaAtual.CasaDireita.CasaDireita.CasaDireita.PecaAtual;
                        var isARookThatDidntMove = pieceThreeToTheRight != null && !pieceThreeToTheRight.ehInimigaDe(this) && !pieceThreeToTheRight.JaMoveu();
                        if (isARookThatDidntMove)
                        {
                            //se não tem nenhuma peça inimiga que pode entrar naquela posição:
                            if (squareTest.getPecasDaCorQuePodemMoverPara(this.getCorInimiga(), "K").Count == 0)
                            {
                                squareTest = getSquareBySteps(new List<Step>() { Step.Left, Step.Left });
                                if (squareTest != null)
                                {
                                    //se não tem nenhuma peça inimiga que pode entrar naquela posição:
                                    if (squareTest.getPecasDaCorQuePodemMoverPara(this.getCorInimiga(), "K").Count == 0)
                                    {

                                        routes.Add(new List<Step>() { Step.Left, Step.Left });
                                    }
                                }

                            }
                        }

                    }
                }

                //ROQUE LONGO
                if ((this.Cor == ElementColor.Branca && this._board.BrancasEmbaixo) ||
                    (this.Cor == ElementColor.Preta && !this._board.BrancasEmbaixo)
                    )
                {
                    //se a torre à esquerda ainda não se moveu e esta peça tb não
                    if (!this.JaMoveu())
                    {

                        var pieceFourToTheLeft = this.CasaAtual.CasaEsquerda.CasaEsquerda.CasaEsquerda.CasaEsquerda.PecaAtual;
                        var isARookThatDidntMove = pieceFourToTheLeft != null && !pieceFourToTheLeft.ehInimigaDe(this) && !pieceFourToTheLeft.JaMoveu();
                        if (isARookThatDidntMove)
                        {
                            //se não tem nenhuma peça inimiga que pode entrar naquela posição:
                            if (squareTest.getPecasDaCorQuePodemMoverPara(this.getCorInimiga(), "K").Count == 0)
                            {
                                squareTest = getSquareBySteps(new List<Step>() { Step.Left, Step.Left });
                                if (squareTest != null)
                                {
                                    //se não tem nenhuma peça inimiga que pode entrar naquela posição:
                                    if (squareTest.getPecasDaCorQuePodemMoverPara(this.getCorInimiga(), "K").Count == 0)
                                    {

                                        routes.Add(new List<Step>() { Step.Left, Step.Left });
                                    }
                                }

                            }
                        }

                    }
                }
            }


            squareTest = getSquareBySteps(new List<Step>() { Step.DiagonalRightFront });
            if (squareTest != null)
            {

                routes.Add(new List<Step>() { Step.DiagonalRightFront });
            }

            squareTest = getSquareBySteps(new List<Step>() { Step.DiagonalLeftFront });
            if (squareTest != null)
            {

                routes.Add(new List<Step>() { Step.DiagonalLeftFront });
            }

            squareTest = getSquareBySteps(new List<Step>() { Step.DiagonalRightBack });
            if (squareTest != null)
            {

                routes.Add(new List<Step>() { Step.DiagonalRightBack });
            }

            squareTest = getSquareBySteps(new List<Step>() { Step.DiagonalLeftBack });
            if (squareTest != null)
            {

                routes.Add(new List<Step>() { Step.DiagonalLeftBack });
            }

            return routes;
        }




        public override System.Drawing.Image getImage()
        {
            if (this.Cor == ElementColor.Branca)
            {
                return Resources.whiteKing;
            }
            else
            {
                return Resources.blackKing;
            }
        }

        public override string ToString() => "K";
    }
}

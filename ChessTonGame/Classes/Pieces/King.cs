using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pieces
{
    public class King : Piece
    {
        public King(ElementColor cor, Square c)
            : base(cor, c, false, false,false,true)
        {
            this.PieceMoved += Rei_PieceMoved;
            this._board.PieceMoved += _board_PieceMoved;
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
            //    this._tabuleiro.UndoLastMovement(); 
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
            if (squareTest != null  )
            {

                routes.Add(new List<Step>() { Step.Front });
            }
            squareTest = getSquareBySteps(new List<Step>() { Step.Back });
            if (squareTest != null  )
            {

                routes.Add(new List<Step>() { Step.Back });
            }

            squareTest = getSquareBySteps(new List<Step>() { Step.Right });
            if (squareTest != null  )
            {

                routes.Add(new List<Step>() { Step.Right });
            }


            squareTest = getSquareBySteps(new List<Step>() { Step.Left });
            if (squareTest != null  )
            {

                routes.Add(new List<Step>() { Step.Left });
            }


            squareTest = getSquareBySteps(new List<Step>() { Step.DiagonalRightFront });
            if (squareTest != null  )
            {

                routes.Add(new List<Step>() { Step.DiagonalRightFront });
            }

            squareTest = getSquareBySteps(new List<Step>() { Step.DiagonalLeftFront });
            if (squareTest != null  )
            {

                routes.Add(new List<Step>() { Step.DiagonalLeftFront });
            }

            squareTest = getSquareBySteps(new List<Step>() { Step.DiagonalRightBack });
            if (squareTest != null  )
            {

                routes.Add(new List<Step>() { Step.DiagonalRightBack });
            }

            squareTest = getSquareBySteps(new List<Step>() { Step.DiagonalLeftBack });
            if (squareTest != null  )
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

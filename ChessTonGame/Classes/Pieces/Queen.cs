using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pieces
{
    public class Queen : Piece
    { 
        public Queen(ElementColor cor, Square c)
            : base(cor, c,false)
        { }
 
        public override decimal ValueInPoints
        {
            get { return 9; }
        }

        public override List<List<Step>> getPossibleRoutes()
        {
            List<List<Step>> rotas = new List<List<Step>>();

            rotas.Add(new List<Step>() {Step.DiagonalRighFrontUndefined  });
            rotas.Add(new List<Step>() {Step.DiagonalRightBackUndefined  });
            rotas.Add(new List<Step>() {Step.DiagonalLeftFrontUndefined  });
            rotas.Add(new List<Step>() {Step.DiagonalLeftBackUndefined });
             
            rotas.Add(new List<Step>() { Step.FrontUndefined });
            rotas.Add(new List<Step>() { Step.BackUndefined });
            rotas.Add(new List<Step>() { Step.RightUndefined });
            rotas.Add(new List<Step>() { Step.LeftUndefined });


            return rotas;
        }

        public override System.Drawing.Image getImage()
        { 
            if (this.Cor == ElementColor.Branca)
            {
                return Resources.whiteQueen;
            }
            else
            {
                return Resources.blackQueen;
            }
        }
         
    }
}

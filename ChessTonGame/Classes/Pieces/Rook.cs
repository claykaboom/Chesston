using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pieces
{
    public class Rook : Piece
    { 
        public Rook(ElementColor cor, Square c)
            : base(cor, c, false, true, true, false)
        { }
 

        public override decimal ValueInPoints
        {
            get { return 5; }
        }

        public override List<List<Step>> getPossibleRoutes()
        {
            List<List<Step>> rotas = new List<List<Step>>();

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
                return Resources.whiteRook;
            }
            else
            {
                return Resources.blackRook;
            }
        }

        public override string ToString() => "R";
    }
}

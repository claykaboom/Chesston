using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pieces
{
    public class Knight : Piece
    { 
        public Knight(ElementColor cor, Square c)
            : base(cor, c, true)
        { }

        public override decimal ValueInPoints
        {
            get { return 3; }
        }

        public override List<List<Step>> getPossibleRoutes()
        {
            List<List<Step>> rotas = new List<List<Step>>();

            rotas.Add(new List<Step>() { Step.Front, Step.Front, Step.Right });
            rotas.Add(new List<Step>() { Step.Front, Step.Front, Step.Left });
            rotas.Add(new List<Step>() { Step.Back, Step.Back, Step.Right });
            rotas.Add(new List<Step>() { Step.Back, Step.Back, Step.Left });

            rotas.Add(new List<Step>() { Step.Right, Step.Right, Step.Front });
            rotas.Add(new List<Step>() { Step.Right, Step.Right, Step.Back });
            rotas.Add(new List<Step>() { Step.Left, Step.Left, Step.Front });
            rotas.Add(new List<Step>() { Step.Left, Step.Left, Step.Back });

            return rotas;
        }

        public override System.Drawing.Image getImage()
        {
            if (this.Cor == ElementColor.Branca)
            {
                return Resources.whiteKnight;
            }
            else
            {
                return Resources.blackKnight;
            }
        }

        public override string ToString() => "N";
    }
}

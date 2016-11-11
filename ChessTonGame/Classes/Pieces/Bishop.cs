using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pieces
{
    public class Bishop : Piece
    { 
        public Bishop(ElementColor cor, Square c)
            : base(cor, c,false)
        { }
 

        public override decimal ValorPontos
        {
            get { return 3; }
        }

        public override List<List<Step>> getRotasPossiveis()
        {
            List<List<Step>> rotas = new List<List<Step>>();

            rotas.Add(new List<Step>() {Step.DiagonalRighFrontUndefined  });
            rotas.Add(new List<Step>() {Step.DiagonalRightBackUndefined  });
            rotas.Add(new List<Step>() {Step.DiagonalLeftFrontUndefined  });
            rotas.Add(new List<Step>() {Step.DiagonalLeftBackUndefined }); 

            return rotas;
        }

        public override System.Drawing.Image getImage()
        { 
            if (this.Cor == ElementColor.Branca)
            {
                return Resources.whiteBishop;
            }
            else
            {
                return Resources.blackBishop;
            }
        }
         
    }
}

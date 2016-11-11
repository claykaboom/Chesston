using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public interface IPiece
    {
        bool EstaEmXeque();

        List<IPiece> QuemDeuXeque();

        bool PodeMoverPara(Square casa);

        List<Square> CasasPossiveis();

        bool FicaEmXequeNaCasa(Square casa);

        void MoverPara(Square casa);

        Square CasaAtual();

        Image getImage();
       

    }
}

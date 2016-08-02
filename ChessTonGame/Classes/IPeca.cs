using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes
{
    public interface IPeca
    {
        bool EstaEmXeque();

        List<IPeca> QuemDeuXeque();

        bool PodeMoverPara(Casa casa);

        List<Casa> CasasPossiveis();

        bool FicaEmXequeNaCasa(Casa casa);

        void MoverPara(Casa casa);

        Casa CasaAtual();

        Image getImage();
       

    }
}

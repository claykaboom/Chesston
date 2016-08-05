﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessTonGame.Classes.Pecas
{
    public class Bispo : Peca
    {
        public Bispo()
        { }
        public Bispo(CorElemento cor, Casa c)
            : base(cor, c,false)
        { }
 

        public override decimal ValorPontos
        {
            get { return 3; }
        }

        public override List<List<Passo>> getRotasPossiveis()
        {
            List<List<Passo>> rotas = new List<List<Passo>>();

            rotas.Add(new List<Passo>() {Passo.DiagonalDireitaFrenteIndefinido  });
            rotas.Add(new List<Passo>() {Passo.DiagonalDireitaTrasIndefinido  });
            rotas.Add(new List<Passo>() {Passo.DiagonalEsquerdaFrenteIndefinido  });
            rotas.Add(new List<Passo>() {Passo.DiagonalEsquerdaTrasIndefinido }); 

            return rotas;
        }

        public override System.Drawing.Image getImage()
        { 
            if (this.Cor == CorElemento.Branca)
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

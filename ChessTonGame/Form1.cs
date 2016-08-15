﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChessTonGame.Classes;
using ChessTonGame.Classes.Pecas;

namespace ChessTonGame
{
    public partial class ChessTon : Form
    {
        Tabuleiro board = new Tabuleiro(8, 8, true, ModoJogo.AlternaTurnos);
        public ChessTon()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Peca p = new Peao(CorElemento.Preta, board.Casas[1][0]);
            p = new Peao(CorElemento.Preta, board.Casas[1][1]);
            p = new Peao(CorElemento.Preta, board.Casas[1][2]);
            p = new Peao(CorElemento.Preta, board.Casas[1][3]);
            p = new Peao(CorElemento.Preta, board.Casas[1][4]);
            p = new Peao(CorElemento.Preta, board.Casas[1][5]);
            p = new Peao(CorElemento.Preta, board.Casas[1][6]);
            p = new Peao(CorElemento.Preta, board.Casas[1][7]);
            p = new Peao(CorElemento.Preta, board.Casas[1][1]);

            p = new Cavalo(CorElemento.Preta, board.Casas[0][1]);
            p = new Cavalo(CorElemento.Preta, board.Casas[0][6]);


            p = new Bispo(CorElemento.Preta, board.Casas[0][2]);
            p = new Bispo(CorElemento.Preta, board.Casas[0][5]);

            p = new Torre(CorElemento.Preta, board.Casas[0][0]);
            p = new Torre(CorElemento.Preta, board.Casas[0][7]);

            p = new Rei(CorElemento.Preta, board.Casas[0][4]);

            p = new Rainha(CorElemento.Preta, board.Casas[0][3]);

            p = new Peao(CorElemento.Branca, board.Casas[6][0]);
            p = new Peao(CorElemento.Branca, board.Casas[6][1]);
            p = new Peao(CorElemento.Branca, board.Casas[6][2]);
            p = new Peao(CorElemento.Branca, board.Casas[6][3]);
            p = new Peao(CorElemento.Branca, board.Casas[6][4]);
            p = new Peao(CorElemento.Branca, board.Casas[6][5]);
            p = new Peao(CorElemento.Branca, board.Casas[6][6]);
            p = new Peao(CorElemento.Branca, board.Casas[6][7]);


            p = new Cavalo(CorElemento.Branca, board.Casas[7][1]);
            p = new Cavalo(CorElemento.Branca, board.Casas[7][6]);

            p = new Bispo(CorElemento.Branca, board.Casas[7][2]);
            p = new Bispo(CorElemento.Branca, board.Casas[7][5]);


            p = new Torre(CorElemento.Branca, board.Casas[7][0]);
            p = new Torre(CorElemento.Branca, board.Casas[7][7]);

            p = new Rainha(CorElemento.Branca, board.Casas[7][3]);
            p = new Rei(CorElemento.Branca, board.Casas[7][4]);


            pbBoard.Image = board.DesenhaTabuleiro();
        }

        private void btnDesenhaTabuleiro_Click(object sender, EventArgs e)
        {
          
        }

        private void pbBoard_Click(object sender, EventArgs e)
        {

        }

        private void pbBoard_MouseClick(object sender, MouseEventArgs e)
        {
            board.Click(e.X, e.Y);
            pbBoard.Image = board.DesenhaTabuleiro();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            board.UndoLastMovement();

            pbBoard.Image = board.DesenhaTabuleiro();
        }

        private void btnSavingPieces_Click(object sender, EventArgs e)
        {
            var savingPieces = board.PecaSelecionada.PecasQuePodemSalvarDoXeque();
        }
    }
}

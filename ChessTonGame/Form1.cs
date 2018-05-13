using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ChessTonGame.Classes;
using ChessTonGame.Classes.Pieces;

namespace ChessTonGame
{
    public partial class ChessTon : Form
    {
        Board board = new Board(8, 8, true, GameMode.ShiftTurns);

        private List<Movement> redoMoves = new List<Movement>();


        public ChessTon()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            board.PieceMoved += Board_PieceMoved;
            board.MovementUndone += Board_PieceMoved;
            board.MovementUndone += Board_MovementUndone;

          Piece p = new Pawn(ElementColor.Preta, board.Casas[1][0]);
            p = new Pawn(ElementColor.Preta, board.Casas[1][1]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Preta, board.Casas[1][2]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Preta, board.Casas[1][3]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Preta, board.Casas[1][4]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Preta, board.Casas[1][5]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Preta, board.Casas[1][6]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Preta, board.Casas[1][7]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Preta, board.Casas[1][1]);
            p.PieceMoved += P_PieceMoved;

            p = new Knight(ElementColor.Preta, board.Casas[0][1]);
            p.PieceMoved += P_PieceMoved;
            p = new Knight(ElementColor.Preta, board.Casas[0][6]);
            p.PieceMoved += P_PieceMoved;


            p = new Bishop(ElementColor.Preta, board.Casas[0][2]);
            p.PieceMoved += P_PieceMoved;
            p = new Bishop(ElementColor.Preta, board.Casas[0][5]);
            p.PieceMoved += P_PieceMoved;

            p = new Rook(ElementColor.Preta, board.Casas[0][0]);
            p.PieceMoved += P_PieceMoved;
            p = new Rook(ElementColor.Preta, board.Casas[0][7]);
            p.PieceMoved += P_PieceMoved;

            p = new King(ElementColor.Preta, board.Casas[0][4]);
            p.PieceMoved += P_PieceMoved;

            p = new Queen(ElementColor.Preta, board.Casas[0][3]);
            p.PieceMoved += P_PieceMoved;

            p = new Pawn(ElementColor.Branca, board.Casas[6][0]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Branca, board.Casas[6][1]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Branca, board.Casas[6][2]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Branca, board.Casas[6][3]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Branca, board.Casas[6][4]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Branca, board.Casas[6][5]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Branca, board.Casas[6][6]);
            p.PieceMoved += P_PieceMoved;
            p = new Pawn(ElementColor.Branca, board.Casas[6][7]);
            p.PieceMoved += P_PieceMoved;


            p = new Knight(ElementColor.Branca, board.Casas[7][1]);
            p.PieceMoved += P_PieceMoved;
            p = new Knight(ElementColor.Branca, board.Casas[7][6]);
            p.PieceMoved += P_PieceMoved;

            p = new Bishop(ElementColor.Branca, board.Casas[7][2]);
            p.PieceMoved += P_PieceMoved;
            p = new Bishop(ElementColor.Branca, board.Casas[7][5]);
            p.PieceMoved += P_PieceMoved;


            p = new Rook(ElementColor.Branca, board.Casas[7][0]);
            p.PieceMoved += P_PieceMoved;
            p = new Rook(ElementColor.Branca, board.Casas[7][7]);
            p.PieceMoved += P_PieceMoved;

            p = new Queen(ElementColor.Branca, board.Casas[7][3]);
            p.PieceMoved += P_PieceMoved;
            p = new King(ElementColor.Branca, board.Casas[7][4]);
            p.PieceMoved += P_PieceMoved;


            pbBoard.Image = board.DesenhaTabuleiro();
        }
        private void Board_MovementUndone(Movement m)
        {
            redoMoves.Add(m);
        }

        private void Board_PieceMoved(Movement m)
        {
            txtMoveHistory.Text = board.Movimentos.ToString();
        }

        private void P_PieceMoved(Movement m)
        { 
            m.CasaOrigem.TemporaryColor = null;
            pbBoard.Image = board.DesenhaTabuleiro();
            redoMoves.Clear();
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
            board.Casas.ForEach(c => c.ForEach( c1 => c1.TemporaryColor = null));

            if (board.PecaSelecionada != null)
            {
                
                var savingPieces = board.PecaSelecionada.PecasQuePodemSalvarDoXeque();
                savingPieces.ForEach(p => p.CasaAtual.TemporaryColor = Color.Yellow );
                MessageBox.Show($"There are {savingPieces.Count} pieces able to save this one.");
            }
            else
            {
                MessageBox.Show("Please, select a piece");
            }
            pbBoard.Image = board.DesenhaTabuleiro();
        }

        private void btnReDo_Click(object sender, EventArgs e)
        {
            if (redoMoves.Count > 0)
            {
                var m = redoMoves[redoMoves.Count - 1];
                redoMoves.Remove(m);
                m.Peca.MoverPara(m.CasaDestino, true);
            }
        }
    }
}

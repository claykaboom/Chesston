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
using ChessTonGame.IA;

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


            ResetBoard();

        }

        private void ResetBoard()
        {

            board = new Board(8, 8, true, GameMode.ShiftTurns);


            board.PieceStartedMoving += Board_PieceMoved;
            board.MovementUndone += Board_PieceMoved;
            board.MovementUndone += Board_MovementUndone;
            board.PieceSelected += Board_PieceSelected;
            board.CheckMate += Board_CheckMate;
            Piece p = null;


            p = new Pawn(ElementColor.Preta, board.Casas[1][0]);
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


            txtMoveHistory.Text = board.Movimentos.ToString();
        }

        private void Board_CheckMate(ElementColor matedColor)
        {
            MessageBox.Show("Check Mate!");
        }

        private void Board_PieceSelected(Piece p)
        {
            ClearTemporaryColors();
        }

        private void Board_MovementUndone(Movement m)
        {
            redoMoves.Add(m);
        }

        private void Board_PieceMoved(Movement m)
        {
            ClearTemporaryColors();
            txtMoveHistory.Text = board.Movimentos.ToString();
        }

        private void ClearTemporaryColors()
        {
            board.Casas.ForEach(c => c.ForEach(c1 => c1.TemporaryColor = null));
        }

        private void P_PieceMoved(Movement m)
        {
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

            if (board.PecaSelecionada != null)
            {

                var savingPieces = board.PecaSelecionada.PecasQuePodemSalvarDoXeque();
                savingPieces.ForEach(p => p.CasaAtual.TemporaryColor = Color.Yellow);
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
                pbBoard.Image = board.DesenhaTabuleiro();
            }
        }

        private void btnResetBoard_Click(object sender, EventArgs e)
        {
            ResetBoard();
            pbBoard.Image = board.DesenhaTabuleiro();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var movesString = txtMoveHistory.Lines;
            ResetBoard();
            ClassicPieceFactory cpf = new ClassicPieceFactory();
            foreach (var item in movesString)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    var m = Movement.GetMovement(item, board, board.VezDaCor, cpf);//TODO: Piece info is irrelevant in this case
                    board.SelectPieceInTheSquare(m.CasaOrigem);
                    board.Move(m.CasaDestino);
                }
            }
            pbBoard.Image = board.DesenhaTabuleiro();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RandomPlayer rp = new RandomPlayer(ElementColor.Preta, board);
            rp.notify += Rp_notify;
        }

        private void Rp_notify()
        {
            pbBoard.Image = board.DesenhaTabuleiro();
        }
    }
}

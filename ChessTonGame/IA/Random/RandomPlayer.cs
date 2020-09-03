using ChessTonGame.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChessTonGame.IA
{
    public class RandomPlayer
    {
        private ElementColor _cor; private Board _board;
        private Random rnd = new Random();
        private bool isMated = false;
        public delegate void PlayerNotifyEventHandler();
        public event PlayerNotifyEventHandler notify;

        private BackgroundWorker backgroundWorker1;
        private int _interval = 500;

        public RandomPlayer(ElementColor cor, Board b, int IntervalToWait)
        {
            b.PieceFinishedMoving += B_PieceMoved;
            b.CheckMate += B_CheckMate;
            this._interval = IntervalToWait;
            this._cor = cor;
            this._board = b;
            this.backgroundWorker1 =new BackgroundWorker(); 
            this.backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
        }

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            Move();
        }

        private void B_CheckMate(ElementColor matedColor)
        {
            if (matedColor == _cor)
            {
                this.isMated = true;
                MessageBox.Show("Player diz: Obrigado !");
            }
        }

        public Movement Move()
        {
            if (_board.VezDaCor == this._cor && !isMated)
            {
                System.Threading.Thread.Sleep(_interval);
                var listaPecas = this._board.getTodasPecas().Where(p => p.Cor == _cor && p.getCasasPossiveis().Count > 0).ToList();

                var pecaToMove = listaPecas[rnd.Next(0, listaPecas.Count - 1)];





                var possibleMoves = pecaToMove.getCasasPossiveis();
                var moveToMove = possibleMoves[rnd.Next(0, possibleMoves.Count - 1)];
                _board.SelectPieceInTheSquare(pecaToMove.CasaAtual);
                if (notify != null)
                    notify();
                var m = _board.Move(moveToMove);
                if (notify != null)
                    notify();
                return m;
            }
            return null;
        }

        private void B_PieceMoved(Movement m)
        {
            if (m.Peca.getCorInimiga() == this._cor && !isMated)
            {
                backgroundWorker1.RunWorkerAsync();
            }
        }
    }
}

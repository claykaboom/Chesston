namespace ChessTonGame
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbBoard = new System.Windows.Forms.PictureBox();
            this.btnDesenhaTabuleiro = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBoard
            // 
            this.pbBoard.Location = new System.Drawing.Point(88, 27);
            this.pbBoard.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pbBoard.Name = "pbBoard";
            this.pbBoard.Size = new System.Drawing.Size(641, 444);
            this.pbBoard.TabIndex = 0;
            this.pbBoard.TabStop = false;
            this.pbBoard.Click += new System.EventHandler(this.pbBoard_Click);
            this.pbBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbBoard_MouseClick);
            // 
            // btnDesenhaTabuleiro
            // 
            this.btnDesenhaTabuleiro.Location = new System.Drawing.Point(787, 27);
            this.btnDesenhaTabuleiro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDesenhaTabuleiro.Name = "btnDesenhaTabuleiro";
            this.btnDesenhaTabuleiro.Size = new System.Drawing.Size(216, 28);
            this.btnDesenhaTabuleiro.TabIndex = 1;
            this.btnDesenhaTabuleiro.Text = "Desenha Tabuleiro";
            this.btnDesenhaTabuleiro.UseVisualStyleBackColor = true;
            this.btnDesenhaTabuleiro.Click += new System.EventHandler(this.btnDesenhaTabuleiro_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 594);
            this.Controls.Add(this.btnDesenhaTabuleiro);
            this.Controls.Add(this.pbBoard);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBoard;
        private System.Windows.Forms.Button btnDesenhaTabuleiro;
    }
}


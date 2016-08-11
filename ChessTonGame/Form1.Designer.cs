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
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // pbBoard
            // 
            this.pbBoard.Location = new System.Drawing.Point(66, 22);
            this.pbBoard.Name = "pbBoard";
            this.pbBoard.Size = new System.Drawing.Size(481, 361);
            this.pbBoard.TabIndex = 0;
            this.pbBoard.TabStop = false;
            this.pbBoard.Click += new System.EventHandler(this.pbBoard_Click);
            this.pbBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbBoard_MouseClick);
            // 
            // btnDesenhaTabuleiro
            // 
            this.btnDesenhaTabuleiro.Location = new System.Drawing.Point(590, 22);
            this.btnDesenhaTabuleiro.Name = "btnDesenhaTabuleiro";
            this.btnDesenhaTabuleiro.Size = new System.Drawing.Size(162, 23);
            this.btnDesenhaTabuleiro.TabIndex = 1;
            this.btnDesenhaTabuleiro.Text = "Desenha Tabuleiro";
            this.btnDesenhaTabuleiro.UseVisualStyleBackColor = true;
            this.btnDesenhaTabuleiro.Click += new System.EventHandler(this.btnDesenhaTabuleiro_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(603, 111);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Undo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 483);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnDesenhaTabuleiro);
            this.Controls.Add(this.pbBoard);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBoard;
        private System.Windows.Forms.Button btnDesenhaTabuleiro;
        private System.Windows.Forms.Button button1;
    }
}


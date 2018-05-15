namespace ChessTonGame
{
    partial class ChessTon
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnSavingPieces = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMoveHistory = new System.Windows.Forms.TextBox();
            this.btnReDo = new System.Windows.Forms.Button();
            this.btnResetBoard = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbBoard
            // 
            this.pbBoard.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbBoard.Location = new System.Drawing.Point(0, 0);
            this.pbBoard.Name = "pbBoard";
            this.pbBoard.Size = new System.Drawing.Size(481, 496);
            this.pbBoard.TabIndex = 0;
            this.pbBoard.TabStop = false;
            this.pbBoard.Click += new System.EventHandler(this.pbBoard_Click);
            this.pbBoard.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbBoard_MouseClick);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(306, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Undo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSavingPieces
            // 
            this.btnSavingPieces.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSavingPieces.Location = new System.Drawing.Point(0, 46);
            this.btnSavingPieces.Name = "btnSavingPieces";
            this.btnSavingPieces.Size = new System.Drawing.Size(306, 40);
            this.btnSavingPieces.TabIndex = 3;
            this.btnSavingPieces.Text = "Who can help the current piece?";
            this.btnSavingPieces.UseVisualStyleBackColor = true;
            this.btnSavingPieces.Click += new System.EventHandler(this.btnSavingPieces_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.btnResetBoard);
            this.panel1.Controls.Add(this.txtMoveHistory);
            this.panel1.Controls.Add(this.btnSavingPieces);
            this.panel1.Controls.Add(this.btnReDo);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(481, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(306, 496);
            this.panel1.TabIndex = 4;
            // 
            // txtMoveHistory
            // 
            this.txtMoveHistory.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMoveHistory.Location = new System.Drawing.Point(0, 86);
            this.txtMoveHistory.Multiline = true;
            this.txtMoveHistory.Name = "txtMoveHistory";
            this.txtMoveHistory.Size = new System.Drawing.Size(306, 228);
            this.txtMoveHistory.TabIndex = 4;
            // 
            // btnReDo
            // 
            this.btnReDo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnReDo.Location = new System.Drawing.Point(0, 23);
            this.btnReDo.Name = "btnReDo";
            this.btnReDo.Size = new System.Drawing.Size(306, 23);
            this.btnReDo.TabIndex = 5;
            this.btnReDo.Text = "ReDo";
            this.btnReDo.UseVisualStyleBackColor = true;
            this.btnReDo.Click += new System.EventHandler(this.btnReDo_Click);
            // 
            // btnResetBoard
            // 
            this.btnResetBoard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnResetBoard.Location = new System.Drawing.Point(0, 314);
            this.btnResetBoard.Name = "btnResetBoard";
            this.btnResetBoard.Size = new System.Drawing.Size(306, 23);
            this.btnResetBoard.TabIndex = 6;
            this.btnResetBoard.Text = "Reset Board";
            this.btnResetBoard.UseVisualStyleBackColor = true;
            this.btnResetBoard.Click += new System.EventHandler(this.btnResetBoard_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(96, 363);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChessTon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 496);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pbBoard);
            this.Name = "ChessTon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ChessTon";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBoard;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSavingPieces;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMoveHistory;
        private System.Windows.Forms.Button btnReDo;
        private System.Windows.Forms.Button btnResetBoard;
        private System.Windows.Forms.Button button2;
    }
}


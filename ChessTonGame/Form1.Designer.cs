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
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbBoard
            // 
            this.pbBoard.Dock = System.Windows.Forms.DockStyle.Left;
            this.pbBoard.Location = new System.Drawing.Point(0, 0);
            this.pbBoard.Name = "pbBoard";
            this.pbBoard.Size = new System.Drawing.Size(481, 387);
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
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Undo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnSavingPieces
            // 
            this.btnSavingPieces.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSavingPieces.Location = new System.Drawing.Point(0, 23);
            this.btnSavingPieces.Name = "btnSavingPieces";
            this.btnSavingPieces.Size = new System.Drawing.Size(126, 40);
            this.btnSavingPieces.TabIndex = 3;
            this.btnSavingPieces.Text = "Who can help the current piece?";
            this.btnSavingPieces.UseVisualStyleBackColor = true;
            this.btnSavingPieces.Click += new System.EventHandler(this.btnSavingPieces_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSavingPieces);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(481, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(126, 387);
            this.panel1.TabIndex = 4;
            // 
            // ChessTon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 387);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pbBoard);
            this.Name = "ChessTon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ChessTon";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbBoard)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbBoard;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnSavingPieces;
        private System.Windows.Forms.Panel panel1;
    }
}


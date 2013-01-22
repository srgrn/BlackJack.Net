namespace WinClient
{
    partial class GameChooser
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
            this.btn_newGame = new System.Windows.Forms.Button();
            this.btn_join = new System.Windows.Forms.Button();
            this.lstb_games = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btn_newGame
            // 
            this.btn_newGame.Location = new System.Drawing.Point(198, 301);
            this.btn_newGame.Name = "btn_newGame";
            this.btn_newGame.Size = new System.Drawing.Size(122, 49);
            this.btn_newGame.TabIndex = 0;
            this.btn_newGame.Text = "Create New Game";
            this.btn_newGame.UseVisualStyleBackColor = true;
            this.btn_newGame.Click += new System.EventHandler(this.btn_newGame_Click);
            // 
            // btn_join
            // 
            this.btn_join.Location = new System.Drawing.Point(198, 162);
            this.btn_join.Name = "btn_join";
            this.btn_join.Size = new System.Drawing.Size(122, 49);
            this.btn_join.TabIndex = 1;
            this.btn_join.Text = "Join Game";
            this.btn_join.UseVisualStyleBackColor = true;
            this.btn_join.Click += new System.EventHandler(this.btn_join_Click);
            // 
            // lstb_games
            // 
            this.lstb_games.FormattingEnabled = true;
            this.lstb_games.ItemHeight = 20;
            this.lstb_games.Location = new System.Drawing.Point(102, 12);
            this.lstb_games.Name = "lstb_games";
            this.lstb_games.Size = new System.Drawing.Size(328, 144);
            this.lstb_games.TabIndex = 2;
            this.lstb_games.DoubleClick += new System.EventHandler(this.btn_join_Click);
            // 
            // GameChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 580);
            this.Controls.Add(this.lstb_games);
            this.Controls.Add(this.btn_join);
            this.Controls.Add(this.btn_newGame);
            this.Name = "GameChooser";
            this.Text = "GameChooser";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameChooser_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_newGame;
        private System.Windows.Forms.Button btn_join;
        private System.Windows.Forms.ListBox lstb_games;
    }
}
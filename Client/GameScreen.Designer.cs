namespace WinClient
{
    partial class GameScreen
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
            this.txt_chat = new System.Windows.Forms.TextBox();
            this.txt_input = new System.Windows.Forms.TextBox();
            this.btn_say = new System.Windows.Forms.Button();
            this.grpDealer = new System.Windows.Forms.GroupBox();
            this.pb_DealerCard1 = new System.Windows.Forms.PictureBox();
            this.pb_DealerCard2 = new System.Windows.Forms.PictureBox();
            this.pb_DealerCard3 = new System.Windows.Forms.PictureBox();
            this.pb_DealerCard4 = new System.Windows.Forms.PictureBox();
            this.pb_DealerCard5 = new System.Windows.Forms.PictureBox();
            this.pb_DealerCard6 = new System.Windows.Forms.PictureBox();
            this.lblCardsCount2 = new System.Windows.Forms.Label();
            this.lblCardsCount1 = new System.Windows.Forms.Label();
            this.grpPlayer2 = new System.Windows.Forms.GroupBox();
            this.pb_Player2Card1 = new System.Windows.Forms.PictureBox();
            this.pb_Player2Card2 = new System.Windows.Forms.PictureBox();
            this.pb_Player2Card3 = new System.Windows.Forms.PictureBox();
            this.pb_Player2Card4 = new System.Windows.Forms.PictureBox();
            this.pb_Player2Card5 = new System.Windows.Forms.PictureBox();
            this.pb_Player2Card6 = new System.Windows.Forms.PictureBox();
            this.grpPlayer1 = new System.Windows.Forms.GroupBox();
            this.pb_Player1Card1 = new System.Windows.Forms.PictureBox();
            this.pb_Player1Card2 = new System.Windows.Forms.PictureBox();
            this.pb_Player1Card3 = new System.Windows.Forms.PictureBox();
            this.pb_Player1Card4 = new System.Windows.Forms.PictureBox();
            this.pb_Player1Card5 = new System.Windows.Forms.PictureBox();
            this.pb_Player1Card6 = new System.Windows.Forms.PictureBox();
            this.btn_deal = new System.Windows.Forms.Button();
            this.btn_hit = new System.Windows.Forms.Button();
            this.btn_stand = new System.Windows.Forms.Button();
            this.txt_bet = new System.Windows.Forms.TextBox();
            this.btn_bet = new System.Windows.Forms.Button();
            this.grpDealer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard6)).BeginInit();
            this.grpPlayer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card6)).BeginInit();
            this.grpPlayer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card6)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_chat
            // 
            this.txt_chat.Location = new System.Drawing.Point(13, 13);
            this.txt_chat.Multiline = true;
            this.txt_chat.Name = "txt_chat";
            this.txt_chat.ReadOnly = true;
            this.txt_chat.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txt_chat.Size = new System.Drawing.Size(339, 495);
            this.txt_chat.TabIndex = 0;
            // 
            // txt_input
            // 
            this.txt_input.Location = new System.Drawing.Point(13, 515);
            this.txt_input.Name = "txt_input";
            this.txt_input.Size = new System.Drawing.Size(339, 26);
            this.txt_input.TabIndex = 1;
            this.txt_input.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_input_KeyPress);
            // 
            // btn_say
            // 
            this.btn_say.Location = new System.Drawing.Point(126, 547);
            this.btn_say.Name = "btn_say";
            this.btn_say.Size = new System.Drawing.Size(102, 46);
            this.btn_say.TabIndex = 2;
            this.btn_say.Text = "Say";
            this.btn_say.UseVisualStyleBackColor = true;
            this.btn_say.Click += new System.EventHandler(this.btn_say_Click);
            // 
            // grpDealer
            // 
            this.grpDealer.Controls.Add(this.pb_DealerCard1);
            this.grpDealer.Controls.Add(this.pb_DealerCard2);
            this.grpDealer.Controls.Add(this.pb_DealerCard3);
            this.grpDealer.Controls.Add(this.pb_DealerCard4);
            this.grpDealer.Controls.Add(this.pb_DealerCard5);
            this.grpDealer.Controls.Add(this.pb_DealerCard6);
            this.grpDealer.Location = new System.Drawing.Point(378, 14);
            this.grpDealer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDealer.Name = "grpDealer";
            this.grpDealer.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpDealer.Size = new System.Drawing.Size(288, 192);
            this.grpDealer.TabIndex = 19;
            this.grpDealer.TabStop = false;
            this.grpDealer.Tag = "0";
            this.grpDealer.Text = "Dealer";
            // 
            // pb_DealerCard1
            // 
            this.pb_DealerCard1.Location = new System.Drawing.Point(8, 29);
            this.pb_DealerCard1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_DealerCard1.Name = "pb_DealerCard1";
            this.pb_DealerCard1.Size = new System.Drawing.Size(112, 146);
            this.pb_DealerCard1.TabIndex = 11;
            this.pb_DealerCard1.TabStop = false;
            // 
            // pb_DealerCard2
            // 
            this.pb_DealerCard2.Location = new System.Drawing.Point(38, 29);
            this.pb_DealerCard2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_DealerCard2.Name = "pb_DealerCard2";
            this.pb_DealerCard2.Size = new System.Drawing.Size(112, 146);
            this.pb_DealerCard2.TabIndex = 15;
            this.pb_DealerCard2.TabStop = false;
            this.pb_DealerCard2.Tag = "2";
            // 
            // pb_DealerCard3
            // 
            this.pb_DealerCard3.Location = new System.Drawing.Point(68, 29);
            this.pb_DealerCard3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_DealerCard3.Name = "pb_DealerCard3";
            this.pb_DealerCard3.Size = new System.Drawing.Size(112, 146);
            this.pb_DealerCard3.TabIndex = 16;
            this.pb_DealerCard3.TabStop = false;
            // 
            // pb_DealerCard4
            // 
            this.pb_DealerCard4.Location = new System.Drawing.Point(98, 29);
            this.pb_DealerCard4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_DealerCard4.Name = "pb_DealerCard4";
            this.pb_DealerCard4.Size = new System.Drawing.Size(112, 146);
            this.pb_DealerCard4.TabIndex = 14;
            this.pb_DealerCard4.TabStop = false;
            // 
            // pb_DealerCard5
            // 
            this.pb_DealerCard5.Location = new System.Drawing.Point(128, 29);
            this.pb_DealerCard5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_DealerCard5.Name = "pb_DealerCard5";
            this.pb_DealerCard5.Size = new System.Drawing.Size(112, 146);
            this.pb_DealerCard5.TabIndex = 12;
            this.pb_DealerCard5.TabStop = false;
            // 
            // pb_DealerCard6
            // 
            this.pb_DealerCard6.Location = new System.Drawing.Point(158, 29);
            this.pb_DealerCard6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_DealerCard6.Name = "pb_DealerCard6";
            this.pb_DealerCard6.Size = new System.Drawing.Size(112, 146);
            this.pb_DealerCard6.TabIndex = 13;
            this.pb_DealerCard6.TabStop = false;
            // 
            // lblCardsCount2
            // 
            this.lblCardsCount2.AutoSize = true;
            this.lblCardsCount2.Location = new System.Drawing.Point(962, 531);
            this.lblCardsCount2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardsCount2.Name = "lblCardsCount2";
            this.lblCardsCount2.Size = new System.Drawing.Size(18, 20);
            this.lblCardsCount2.TabIndex = 28;
            this.lblCardsCount2.Text = "0";
            // 
            // lblCardsCount1
            // 
            this.lblCardsCount1.AutoSize = true;
            this.lblCardsCount1.Location = new System.Drawing.Point(652, 531);
            this.lblCardsCount1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCardsCount1.Name = "lblCardsCount1";
            this.lblCardsCount1.Size = new System.Drawing.Size(18, 20);
            this.lblCardsCount1.TabIndex = 27;
            this.lblCardsCount1.Text = "0";
            // 
            // grpPlayer2
            // 
            this.grpPlayer2.Controls.Add(this.pb_Player2Card1);
            this.grpPlayer2.Controls.Add(this.pb_Player2Card2);
            this.grpPlayer2.Controls.Add(this.pb_Player2Card3);
            this.grpPlayer2.Controls.Add(this.pb_Player2Card4);
            this.grpPlayer2.Controls.Add(this.pb_Player2Card5);
            this.grpPlayer2.Controls.Add(this.pb_Player2Card6);
            this.grpPlayer2.Location = new System.Drawing.Point(726, 339);
            this.grpPlayer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpPlayer2.Name = "grpPlayer2";
            this.grpPlayer2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpPlayer2.Size = new System.Drawing.Size(288, 188);
            this.grpPlayer2.TabIndex = 25;
            this.grpPlayer2.TabStop = false;
            this.grpPlayer2.Tag = "2";
            this.grpPlayer2.Text = "Player 2";
            // 
            // pb_Player2Card1
            // 
            this.pb_Player2Card1.Location = new System.Drawing.Point(9, 23);
            this.pb_Player2Card1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player2Card1.Name = "pb_Player2Card1";
            this.pb_Player2Card1.Size = new System.Drawing.Size(112, 146);
            this.pb_Player2Card1.TabIndex = 9;
            this.pb_Player2Card1.TabStop = false;
            // 
            // pb_Player2Card2
            // 
            this.pb_Player2Card2.Location = new System.Drawing.Point(39, 23);
            this.pb_Player2Card2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player2Card2.Name = "pb_Player2Card2";
            this.pb_Player2Card2.Size = new System.Drawing.Size(112, 146);
            this.pb_Player2Card2.TabIndex = 10;
            this.pb_Player2Card2.TabStop = false;
            // 
            // pb_Player2Card3
            // 
            this.pb_Player2Card3.Location = new System.Drawing.Point(69, 23);
            this.pb_Player2Card3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player2Card3.Name = "pb_Player2Card3";
            this.pb_Player2Card3.Size = new System.Drawing.Size(112, 146);
            this.pb_Player2Card3.TabIndex = 10;
            this.pb_Player2Card3.TabStop = false;
            // 
            // pb_Player2Card4
            // 
            this.pb_Player2Card4.Location = new System.Drawing.Point(99, 23);
            this.pb_Player2Card4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player2Card4.Name = "pb_Player2Card4";
            this.pb_Player2Card4.Size = new System.Drawing.Size(112, 146);
            this.pb_Player2Card4.TabIndex = 10;
            this.pb_Player2Card4.TabStop = false;
            // 
            // pb_Player2Card5
            // 
            this.pb_Player2Card5.Location = new System.Drawing.Point(129, 23);
            this.pb_Player2Card5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player2Card5.Name = "pb_Player2Card5";
            this.pb_Player2Card5.Size = new System.Drawing.Size(112, 146);
            this.pb_Player2Card5.TabIndex = 10;
            this.pb_Player2Card5.TabStop = false;
            // 
            // pb_Player2Card6
            // 
            this.pb_Player2Card6.Location = new System.Drawing.Point(159, 23);
            this.pb_Player2Card6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player2Card6.Name = "pb_Player2Card6";
            this.pb_Player2Card6.Size = new System.Drawing.Size(112, 146);
            this.pb_Player2Card6.TabIndex = 10;
            this.pb_Player2Card6.TabStop = false;
            // 
            // grpPlayer1
            // 
            this.grpPlayer1.Controls.Add(this.pb_Player1Card1);
            this.grpPlayer1.Controls.Add(this.pb_Player1Card2);
            this.grpPlayer1.Controls.Add(this.pb_Player1Card3);
            this.grpPlayer1.Controls.Add(this.pb_Player1Card4);
            this.grpPlayer1.Controls.Add(this.pb_Player1Card5);
            this.grpPlayer1.Controls.Add(this.pb_Player1Card6);
            this.grpPlayer1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grpPlayer1.Location = new System.Drawing.Point(416, 339);
            this.grpPlayer1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpPlayer1.Name = "grpPlayer1";
            this.grpPlayer1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.grpPlayer1.Size = new System.Drawing.Size(288, 188);
            this.grpPlayer1.TabIndex = 26;
            this.grpPlayer1.TabStop = false;
            this.grpPlayer1.Tag = "1";
            this.grpPlayer1.Text = "Player 1";
            // 
            // pb_Player1Card1
            // 
            this.pb_Player1Card1.Location = new System.Drawing.Point(9, 23);
            this.pb_Player1Card1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player1Card1.Name = "pb_Player1Card1";
            this.pb_Player1Card1.Size = new System.Drawing.Size(112, 146);
            this.pb_Player1Card1.TabIndex = 9;
            this.pb_Player1Card1.TabStop = false;
            // 
            // pb_Player1Card2
            // 
            this.pb_Player1Card2.Location = new System.Drawing.Point(39, 23);
            this.pb_Player1Card2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player1Card2.Name = "pb_Player1Card2";
            this.pb_Player1Card2.Size = new System.Drawing.Size(112, 146);
            this.pb_Player1Card2.TabIndex = 10;
            this.pb_Player1Card2.TabStop = false;
            // 
            // pb_Player1Card3
            // 
            this.pb_Player1Card3.Location = new System.Drawing.Point(69, 23);
            this.pb_Player1Card3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player1Card3.Name = "pb_Player1Card3";
            this.pb_Player1Card3.Size = new System.Drawing.Size(112, 146);
            this.pb_Player1Card3.TabIndex = 10;
            this.pb_Player1Card3.TabStop = false;
            // 
            // pb_Player1Card4
            // 
            this.pb_Player1Card4.Location = new System.Drawing.Point(99, 23);
            this.pb_Player1Card4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player1Card4.Name = "pb_Player1Card4";
            this.pb_Player1Card4.Size = new System.Drawing.Size(112, 146);
            this.pb_Player1Card4.TabIndex = 10;
            this.pb_Player1Card4.TabStop = false;
            // 
            // pb_Player1Card5
            // 
            this.pb_Player1Card5.Location = new System.Drawing.Point(129, 23);
            this.pb_Player1Card5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player1Card5.Name = "pb_Player1Card5";
            this.pb_Player1Card5.Size = new System.Drawing.Size(112, 146);
            this.pb_Player1Card5.TabIndex = 10;
            this.pb_Player1Card5.TabStop = false;
            // 
            // pb_Player1Card6
            // 
            this.pb_Player1Card6.Location = new System.Drawing.Point(159, 23);
            this.pb_Player1Card6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pb_Player1Card6.Name = "pb_Player1Card6";
            this.pb_Player1Card6.Size = new System.Drawing.Size(112, 146);
            this.pb_Player1Card6.TabIndex = 10;
            this.pb_Player1Card6.TabStop = false;
            // 
            // btn_deal
            // 
            this.btn_deal.Enabled = false;
            this.btn_deal.Location = new System.Drawing.Point(1039, 43);
            this.btn_deal.Name = "btn_deal";
            this.btn_deal.Size = new System.Drawing.Size(102, 46);
            this.btn_deal.TabIndex = 29;
            this.btn_deal.Text = "Deal";
            this.btn_deal.UseVisualStyleBackColor = true;
            this.btn_deal.Click += new System.EventHandler(this.btn_deal_Click);
            // 
            // btn_hit
            // 
            this.btn_hit.Enabled = false;
            this.btn_hit.Location = new System.Drawing.Point(1039, 179);
            this.btn_hit.Name = "btn_hit";
            this.btn_hit.Size = new System.Drawing.Size(102, 46);
            this.btn_hit.TabIndex = 30;
            this.btn_hit.Text = "Hit";
            this.btn_hit.UseVisualStyleBackColor = true;
            this.btn_hit.Click += new System.EventHandler(this.btn_hit_Click);
            // 
            // btn_stand
            // 
            this.btn_stand.Enabled = false;
            this.btn_stand.Location = new System.Drawing.Point(1039, 231);
            this.btn_stand.Name = "btn_stand";
            this.btn_stand.Size = new System.Drawing.Size(102, 46);
            this.btn_stand.TabIndex = 31;
            this.btn_stand.Text = "Stand";
            this.btn_stand.UseVisualStyleBackColor = true;
            this.btn_stand.Click += new System.EventHandler(this.btn_stand_Click);
            // 
            // txt_bet
            // 
            this.txt_bet.Location = new System.Drawing.Point(1039, 95);
            this.txt_bet.Name = "txt_bet";
            this.txt_bet.Size = new System.Drawing.Size(102, 26);
            this.txt_bet.TabIndex = 32;
            // 
            // btn_bet
            // 
            this.btn_bet.Location = new System.Drawing.Point(1039, 127);
            this.btn_bet.Name = "btn_bet";
            this.btn_bet.Size = new System.Drawing.Size(102, 46);
            this.btn_bet.TabIndex = 33;
            this.btn_bet.Text = "Bet";
            this.btn_bet.UseVisualStyleBackColor = true;
            this.btn_bet.Click += new System.EventHandler(this.btn_bet_Click);
            // 
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 636);
            this.Controls.Add(this.btn_bet);
            this.Controls.Add(this.txt_bet);
            this.Controls.Add(this.btn_stand);
            this.Controls.Add(this.btn_hit);
            this.Controls.Add(this.btn_deal);
            this.Controls.Add(this.lblCardsCount2);
            this.Controls.Add(this.lblCardsCount1);
            this.Controls.Add(this.grpPlayer2);
            this.Controls.Add(this.grpPlayer1);
            this.Controls.Add(this.grpDealer);
            this.Controls.Add(this.btn_say);
            this.Controls.Add(this.txt_input);
            this.Controls.Add(this.txt_chat);
            this.Name = "GameScreen";
            this.Text = "GameScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GameScreen_FormClosing);
            this.grpDealer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_DealerCard6)).EndInit();
            this.grpPlayer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player2Card6)).EndInit();
            this.grpPlayer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Player1Card6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_chat;
        private System.Windows.Forms.TextBox txt_input;
        private System.Windows.Forms.Button btn_say;
        private System.Windows.Forms.GroupBox grpDealer;
        private System.Windows.Forms.PictureBox pb_DealerCard1;
        private System.Windows.Forms.PictureBox pb_DealerCard2;
        private System.Windows.Forms.PictureBox pb_DealerCard3;
        private System.Windows.Forms.PictureBox pb_DealerCard4;
        private System.Windows.Forms.PictureBox pb_DealerCard5;
        private System.Windows.Forms.PictureBox pb_DealerCard6;
        private System.Windows.Forms.Label lblCardsCount2;
        private System.Windows.Forms.Label lblCardsCount1;
        private System.Windows.Forms.GroupBox grpPlayer2;
        private System.Windows.Forms.PictureBox pb_Player2Card1;
        private System.Windows.Forms.PictureBox pb_Player2Card2;
        private System.Windows.Forms.PictureBox pb_Player2Card3;
        private System.Windows.Forms.PictureBox pb_Player2Card4;
        private System.Windows.Forms.PictureBox pb_Player2Card5;
        private System.Windows.Forms.PictureBox pb_Player2Card6;
        private System.Windows.Forms.GroupBox grpPlayer1;
        private System.Windows.Forms.PictureBox pb_Player1Card1;
        private System.Windows.Forms.PictureBox pb_Player1Card2;
        private System.Windows.Forms.PictureBox pb_Player1Card3;
        private System.Windows.Forms.PictureBox pb_Player1Card4;
        private System.Windows.Forms.PictureBox pb_Player1Card5;
        private System.Windows.Forms.PictureBox pb_Player1Card6;
        private System.Windows.Forms.Button btn_deal;
        private System.Windows.Forms.Button btn_hit;
        private System.Windows.Forms.Button btn_stand;
        private System.Windows.Forms.TextBox txt_bet;
        private System.Windows.Forms.Button btn_bet;
    }
}
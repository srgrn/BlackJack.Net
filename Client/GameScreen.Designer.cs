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
            // GameScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1342, 636);
            this.Controls.Add(this.btn_say);
            this.Controls.Add(this.txt_input);
            this.Controls.Add(this.txt_chat);
            this.Name = "GameScreen";
            this.Text = "GameScreen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_chat;
        private System.Windows.Forms.TextBox txt_input;
        private System.Windows.Forms.Button btn_say;
    }
}
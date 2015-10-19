namespace WindowsGame1
{
    partial class GameForm
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
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Cheater = new System.Windows.Forms.Button();
            this.Accept = new System.Windows.Forms.Button();
            this.Cyan = new System.Windows.Forms.Button();
            this.Pink = new System.Windows.Forms.Button();
            this.YellowBlack = new System.Windows.Forms.Button();
            this.Green = new System.Windows.Forms.Button();
            this.Blue = new System.Windows.Forms.Button();
            this.RedWhite = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.outputChat = new System.Windows.Forms.ListBox();
            this.inputText = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.myGame2 = new WindowsGame1.MyGame();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Send);
            this.splitContainer1.Panel1.Controls.Add(this.Cheater);
            this.splitContainer1.Panel1.Controls.Add(this.inputText);
            this.splitContainer1.Panel1.Controls.Add(this.YellowBlack);
            this.splitContainer1.Panel1.Controls.Add(this.outputChat);
            this.splitContainer1.Panel1.Controls.Add(this.Pink);
            this.splitContainer1.Panel1.Controls.Add(this.Green);
            this.splitContainer1.Panel1.Controls.Add(this.Blue);
            this.splitContainer1.Panel1.Controls.Add(this.Cyan);
            this.splitContainer1.Panel1.Controls.Add(this.Accept);
            this.splitContainer1.Panel1.Controls.Add(this.RedWhite);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.myGame2);
            this.splitContainer1.Size = new System.Drawing.Size(610, 573);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // Cheater
            // 
            this.Cheater.Location = new System.Drawing.Point(255, 402);
            this.Cheater.Name = "Cheater";
            this.Cheater.Size = new System.Drawing.Size(75, 75);
            this.Cheater.TabIndex = 7;
            this.Cheater.Text = "CHEATER!";
            this.Cheater.UseVisualStyleBackColor = true;
            // 
            // Accept
            // 
            this.Accept.Location = new System.Drawing.Point(255, 483);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 75);
            this.Accept.TabIndex = 6;
            this.Accept.Text = "Accept";
            this.Accept.UseVisualStyleBackColor = true;
            // 
            // Cyan
            // 
            this.Cyan.BackColor = System.Drawing.Color.Aqua;
            this.Cyan.Location = new System.Drawing.Point(174, 483);
            this.Cyan.Name = "Cyan";
            this.Cyan.Size = new System.Drawing.Size(75, 75);
            this.Cyan.TabIndex = 5;
            this.Cyan.UseVisualStyleBackColor = false;
            // 
            // Pink
            // 
            this.Pink.BackColor = System.Drawing.Color.Fuchsia;
            this.Pink.Location = new System.Drawing.Point(93, 483);
            this.Pink.Name = "Pink";
            this.Pink.Size = new System.Drawing.Size(75, 75);
            this.Pink.TabIndex = 4;
            this.Pink.UseVisualStyleBackColor = false;
            // 
            // YellowBlack
            // 
            this.YellowBlack.BackColor = System.Drawing.Color.Yellow;
            this.YellowBlack.Location = new System.Drawing.Point(12, 483);
            this.YellowBlack.Name = "YellowBlack";
            this.YellowBlack.Size = new System.Drawing.Size(75, 75);
            this.YellowBlack.TabIndex = 3;
            this.YellowBlack.UseVisualStyleBackColor = false;
            // 
            // Green
            // 
            this.Green.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Green.Enabled = false;
            this.Green.Location = new System.Drawing.Point(174, 402);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(75, 75);
            this.Green.TabIndex = 2;
            this.Green.UseVisualStyleBackColor = false;
            // 
            // Blue
            // 
            this.Blue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Blue.Location = new System.Drawing.Point(93, 402);
            this.Blue.Name = "Blue";
            this.Blue.Size = new System.Drawing.Size(75, 75);
            this.Blue.TabIndex = 1;
            this.Blue.UseVisualStyleBackColor = false;
            // 
            // RedWhite
            // 
            this.RedWhite.BackColor = System.Drawing.Color.Red;
            this.RedWhite.Location = new System.Drawing.Point(12, 402);
            this.RedWhite.Name = "RedWhite";
            this.RedWhite.Size = new System.Drawing.Size(75, 75);
            this.RedWhite.TabIndex = 0;
            this.RedWhite.UseVisualStyleBackColor = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // outputChat
            // 
            this.outputChat.FormattingEnabled = true;
            this.outputChat.Location = new System.Drawing.Point(12, 12);
            this.outputChat.Name = "outputChat";
            this.outputChat.Size = new System.Drawing.Size(318, 329);
            this.outputChat.TabIndex = 0;
            // 
            // inputText
            // 
            this.inputText.Location = new System.Drawing.Point(12, 352);
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(237, 20);
            this.inputText.TabIndex = 1;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(255, 352);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 23);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // myGame2
            // 
            this.myGame2.Location = new System.Drawing.Point(3, 3);
            this.myGame2.Name = "myGame2";
            this.myGame2.Size = new System.Drawing.Size(262, 567);
            this.myGame2.TabIndex = 0;
            this.myGame2.Text = "myGame2";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 573);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "GameForm";
            this.Text = "Mastermind";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Cyan;
        private System.Windows.Forms.Button Pink;
        private System.Windows.Forms.Button YellowBlack;
        private System.Windows.Forms.Button Green;
        private System.Windows.Forms.Button Blue;
        private System.Windows.Forms.Button RedWhite;
        private System.Windows.Forms.Button Cheater;
        private MyGame myGame1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.TextBox inputText;
        private System.Windows.Forms.ListBox outputChat;
        private MyGame myGame2;
    }
}


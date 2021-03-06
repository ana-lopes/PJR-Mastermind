﻿namespace WindowsGame1
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Send = new System.Windows.Forms.Button();
            this.CheaterAutoCorrect = new System.Windows.Forms.Button();
            this.inputText = new System.Windows.Forms.TextBox();
            this.YellowBlack = new System.Windows.Forms.Button();
            this.outputChat = new System.Windows.Forms.ListBox();
            this.Pink = new System.Windows.Forms.Button();
            this.Green = new System.Windows.Forms.Button();
            this.Blue = new System.Windows.Forms.Button();
            this.Cyan = new System.Windows.Forms.Button();
            this.Accept = new System.Windows.Forms.Button();
            this.RedWhite = new System.Windows.Forms.Button();
            this.Undo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(448, 433);
            this.Send.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(100, 28);
            this.Send.TabIndex = 12;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // CheaterAutoCorrect
            // 
            this.CheaterAutoCorrect.Enabled = false;
            this.CheaterAutoCorrect.Location = new System.Drawing.Point(340, 495);
            this.CheaterAutoCorrect.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CheaterAutoCorrect.Name = "CheaterAutoCorrect";
            this.CheaterAutoCorrect.Size = new System.Drawing.Size(100, 92);
            this.CheaterAutoCorrect.TabIndex = 18;
            this.CheaterAutoCorrect.Text = "CHEATER!";
            this.CheaterAutoCorrect.UseVisualStyleBackColor = true;
            this.CheaterAutoCorrect.Click += new System.EventHandler(this.CheaterAutoCorrect_Click);
            // 
            // inputText
            // 
            this.inputText.AcceptsTab = true;
            this.inputText.Location = new System.Drawing.Point(16, 433);
            this.inputText.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.inputText.Name = "inputText";
            this.inputText.Size = new System.Drawing.Size(424, 22);
            this.inputText.TabIndex = 10;
            // 
            // YellowBlack
            // 
            this.YellowBlack.BackColor = System.Drawing.Color.Yellow;
            this.YellowBlack.Enabled = false;
            this.YellowBlack.Location = new System.Drawing.Point(16, 594);
            this.YellowBlack.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.YellowBlack.Name = "YellowBlack";
            this.YellowBlack.Size = new System.Drawing.Size(100, 92);
            this.YellowBlack.TabIndex = 14;
            this.YellowBlack.UseVisualStyleBackColor = false;
            this.YellowBlack.Click += new System.EventHandler(this.YellowBlack_Click);
            // 
            // outputChat
            // 
            this.outputChat.FormattingEnabled = true;
            this.outputChat.HorizontalScrollbar = true;
            this.outputChat.ItemHeight = 16;
            this.outputChat.Location = new System.Drawing.Point(16, 15);
            this.outputChat.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.outputChat.Name = "outputChat";
            this.outputChat.Size = new System.Drawing.Size(532, 404);
            this.outputChat.TabIndex = 8;
            // 
            // Pink
            // 
            this.Pink.BackColor = System.Drawing.Color.Fuchsia;
            this.Pink.Enabled = false;
            this.Pink.Location = new System.Drawing.Point(124, 594);
            this.Pink.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Pink.Name = "Pink";
            this.Pink.Size = new System.Drawing.Size(100, 92);
            this.Pink.TabIndex = 15;
            this.Pink.UseVisualStyleBackColor = false;
            this.Pink.Click += new System.EventHandler(this.Pink_Click);
            // 
            // Green
            // 
            this.Green.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Green.Enabled = false;
            this.Green.Location = new System.Drawing.Point(232, 495);
            this.Green.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(100, 92);
            this.Green.TabIndex = 13;
            this.Green.UseVisualStyleBackColor = false;
            this.Green.Click += new System.EventHandler(this.Green_Click);
            // 
            // Blue
            // 
            this.Blue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Blue.Enabled = false;
            this.Blue.Location = new System.Drawing.Point(124, 495);
            this.Blue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Blue.Name = "Blue";
            this.Blue.Size = new System.Drawing.Size(100, 92);
            this.Blue.TabIndex = 11;
            this.Blue.UseVisualStyleBackColor = false;
            this.Blue.Click += new System.EventHandler(this.Blue_Click);
            // 
            // Cyan
            // 
            this.Cyan.BackColor = System.Drawing.Color.Aqua;
            this.Cyan.Enabled = false;
            this.Cyan.Location = new System.Drawing.Point(232, 594);
            this.Cyan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Cyan.Name = "Cyan";
            this.Cyan.Size = new System.Drawing.Size(100, 92);
            this.Cyan.TabIndex = 16;
            this.Cyan.UseVisualStyleBackColor = false;
            this.Cyan.Click += new System.EventHandler(this.Cyan_Click);
            // 
            // Accept
            // 
            this.Accept.Enabled = false;
            this.Accept.Location = new System.Drawing.Point(340, 594);
            this.Accept.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(100, 92);
            this.Accept.TabIndex = 17;
            this.Accept.Text = "Accept";
            this.Accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // RedWhite
            // 
            this.RedWhite.BackColor = System.Drawing.Color.Red;
            this.RedWhite.Enabled = false;
            this.RedWhite.Location = new System.Drawing.Point(16, 495);
            this.RedWhite.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.RedWhite.Name = "RedWhite";
            this.RedWhite.Size = new System.Drawing.Size(100, 92);
            this.RedWhite.TabIndex = 9;
            this.RedWhite.UseVisualStyleBackColor = false;
            this.RedWhite.Click += new System.EventHandler(this.RedWhite_Click);
            // 
            // Undo
            // 
            this.Undo.Enabled = false;
            this.Undo.Location = new System.Drawing.Point(448, 495);
            this.Undo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Undo.Name = "Undo";
            this.Undo.Size = new System.Drawing.Size(100, 92);
            this.Undo.TabIndex = 20;
            this.Undo.Text = "Undo";
            this.Undo.UseVisualStyleBackColor = true;
            this.Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LavenderBlush;
            this.ClientSize = new System.Drawing.Size(920, 705);
            this.Controls.Add(this.Undo);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.CheaterAutoCorrect);
            this.Controls.Add(this.inputText);
            this.Controls.Add(this.YellowBlack);
            this.Controls.Add(this.outputChat);
            this.Controls.Add(this.Pink);
            this.Controls.Add(this.Green);
            this.Controls.Add(this.Blue);
            this.Controls.Add(this.Cyan);
            this.Controls.Add(this.Accept);
            this.Controls.Add(this.RedWhite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "GameForm";
            this.Text = "Mastermind";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.Button CheaterAutoCorrect;
        private System.Windows.Forms.TextBox inputText;
        private System.Windows.Forms.Button YellowBlack;
        private System.Windows.Forms.ListBox outputChat;
        private System.Windows.Forms.Button Pink;
        private System.Windows.Forms.Button Green;
        private System.Windows.Forms.Button Blue;
        private System.Windows.Forms.Button Cyan;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button RedWhite;
        private System.Windows.Forms.Button Undo;
    }
}


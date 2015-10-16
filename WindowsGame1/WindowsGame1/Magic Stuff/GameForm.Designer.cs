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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Send = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.Cheater = new System.Windows.Forms.Button();
            this.Accept = new System.Windows.Forms.Button();
            this.Cyan = new System.Windows.Forms.Button();
            this.Pink = new System.Windows.Forms.Button();
            this.YellowBlack = new System.Windows.Forms.Button();
            this.Green = new System.Windows.Forms.Button();
            this.Blue = new System.Windows.Forms.Button();
            this.RedWhite = new System.Windows.Forms.Button();
            this.myGame1 = new WindowsGame1.MyGame();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.myGame1);
            this.splitContainer1.Size = new System.Drawing.Size(610, 573);
            this.splitContainer1.SplitterDistance = 338;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.Send);
            this.splitContainer2.Panel1.Controls.Add(this.textBox1);
            this.splitContainer2.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Cheater);
            this.splitContainer2.Panel2.Controls.Add(this.Accept);
            this.splitContainer2.Panel2.Controls.Add(this.Cyan);
            this.splitContainer2.Panel2.Controls.Add(this.Pink);
            this.splitContainer2.Panel2.Controls.Add(this.YellowBlack);
            this.splitContainer2.Panel2.Controls.Add(this.Green);
            this.splitContainer2.Panel2.Controls.Add(this.Blue);
            this.splitContainer2.Panel2.Controls.Add(this.RedWhite);
            this.splitContainer2.Size = new System.Drawing.Size(338, 573);
            this.splitContainer2.SplitterDistance = 389;
            this.splitContainer2.TabIndex = 0;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(255, 352);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(75, 23);
            this.Send.TabIndex = 2;
            this.Send.Text = "Send";
            this.Send.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 352);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(237, 20);
            this.textBox1.TabIndex = 1;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(318, 329);
            this.listBox1.TabIndex = 0;
            // 
            // Cheater
            // 
            this.Cheater.Location = new System.Drawing.Point(255, 14);
            this.Cheater.Name = "Cheater";
            this.Cheater.Size = new System.Drawing.Size(75, 75);
            this.Cheater.TabIndex = 7;
            this.Cheater.Text = "CHEATER!";
            this.Cheater.UseVisualStyleBackColor = true;
            // 
            // Accept
            // 
            this.Accept.Location = new System.Drawing.Point(255, 95);
            this.Accept.Name = "Accept";
            this.Accept.Size = new System.Drawing.Size(75, 75);
            this.Accept.TabIndex = 6;
            this.Accept.Text = "Accept";
            this.Accept.UseVisualStyleBackColor = true;
            // 
            // Cyan
            // 
            this.Cyan.BackColor = System.Drawing.Color.Aqua;
            this.Cyan.Location = new System.Drawing.Point(174, 95);
            this.Cyan.Name = "Cyan";
            this.Cyan.Size = new System.Drawing.Size(75, 75);
            this.Cyan.TabIndex = 5;
            this.Cyan.UseVisualStyleBackColor = false;
            // 
            // Pink
            // 
            this.Pink.BackColor = System.Drawing.Color.Fuchsia;
            this.Pink.Location = new System.Drawing.Point(93, 95);
            this.Pink.Name = "Pink";
            this.Pink.Size = new System.Drawing.Size(75, 75);
            this.Pink.TabIndex = 4;
            this.Pink.UseVisualStyleBackColor = false;
            // 
            // YellowBlack
            // 
            this.YellowBlack.BackColor = System.Drawing.Color.Yellow;
            this.YellowBlack.Location = new System.Drawing.Point(12, 95);
            this.YellowBlack.Name = "YellowBlack";
            this.YellowBlack.Size = new System.Drawing.Size(75, 75);
            this.YellowBlack.TabIndex = 3;
            this.YellowBlack.UseVisualStyleBackColor = false;
            // 
            // Green
            // 
            this.Green.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Green.Enabled = false;
            this.Green.Location = new System.Drawing.Point(174, 14);
            this.Green.Name = "Green";
            this.Green.Size = new System.Drawing.Size(75, 75);
            this.Green.TabIndex = 2;
            this.Green.UseVisualStyleBackColor = false;
            // 
            // Blue
            // 
            this.Blue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.Blue.Location = new System.Drawing.Point(93, 14);
            this.Blue.Name = "Blue";
            this.Blue.Size = new System.Drawing.Size(75, 75);
            this.Blue.TabIndex = 1;
            this.Blue.UseVisualStyleBackColor = false;
            // 
            // RedWhite
            // 
            this.RedWhite.BackColor = System.Drawing.Color.Red;
            this.RedWhite.Location = new System.Drawing.Point(12, 14);
            this.RedWhite.Name = "RedWhite";
            this.RedWhite.Size = new System.Drawing.Size(75, 75);
            this.RedWhite.TabIndex = 0;
            this.RedWhite.UseVisualStyleBackColor = false;
            // 
            // myGame1
            // 
            this.myGame1.Location = new System.Drawing.Point(4, 4);
            this.myGame1.Name = "myGame1";
            this.myGame1.Size = new System.Drawing.Size(261, 566);
            this.myGame1.TabIndex = 0;
            this.myGame1.Text = "myGame1";
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
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Button Send;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button Accept;
        private System.Windows.Forms.Button Cyan;
        private System.Windows.Forms.Button Pink;
        private System.Windows.Forms.Button YellowBlack;
        private System.Windows.Forms.Button Green;
        private System.Windows.Forms.Button Blue;
        private System.Windows.Forms.Button RedWhite;
        private System.Windows.Forms.Button Cheater;
        private MyGame myGame1;
    }
}


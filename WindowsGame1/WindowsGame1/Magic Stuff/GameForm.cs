#region File Description
//-----------------------------------------------------------------------------
// MainForm.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using System.Drawing;
#endregion

namespace WindowsGame1
{
    enum PlayerType { Challenger, Challenged}
    public partial class GameForm : Form
    {
        public static byte messageByte = (byte)'M', renameByte = (byte)'N';
        public static string startString = "S", playString = "P", stopPlayingString = "B";
        public static string messageString = "M", loginAprovedString = "O", errorString = "E";

        static public GameForm instance;

        public TcpClient client;
        int jogada = 1;
        PlayerType playerType;
        public NetworkStream stream;
        private Queue<string> recievingMessages;
        private Queue<Action> actions;
        private Form form;

        public GameForm(TcpClient client, Form form)
        {
            instance = this;
            InitializeComponent();

            this.form = form;
            this.client = client;
            this.stream = client.GetStream();

            //chat
            this.recievingMessages = new Queue<string>();
            actions = new Queue<Action>();
            Task thread = new Task(readData, recievingMessages);
            thread.Start();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
        }

        private void Send_Click(object sender, EventArgs e)
        {
            string msg = inputText.Text;

            //enviar
            byte[] buf = Encoding.ASCII.GetBytes(msg);
            stream.WriteByte((byte)messageByte);
            stream.Write(buf, 0, buf.Length);
            stream.WriteByte((byte)'\n');

            //limpar textbox
            inputText.Text = "";
        }

        //redes
        private void readData<T>(T queue)
        {
            /*Queue<string> q = queue as Queue<string>;*/
            
            while(true)
            {
                string msg = "";
                try
                {
                    do
                    {
                        byte[] buffer = new byte[1024];
                        int nrBytes = stream.Read(buffer, 0, 1024);
                        msg += Encoding.ASCII.GetString(buffer, 0, nrBytes);
                    } while (!msg.Contains("\n"));

                    DecodeMessage(msg);
                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR while trying to read data: \n" + e.ToString());
                }
            }
        }

        private void Layout1() //challenger
        {            
            RedWhite.BackColor = Color.White;
            YellowBlack.BackColor = Color.Black;
            Blue.BackColor = Color.Gray;
            Pink.BackColor = Color.Gray;
            Green.BackColor = Color.Gray;
            Cyan.BackColor = Color.Gray;
            
            CheaterAutoCorrect.Text = "AutoCorrect";
        }

        private void Layout2() //challenged
        {
            RedWhite.BackColor = Color.Red;
            YellowBlack.BackColor = Color.Yellow;
            Blue.BackColor = Color.Blue;
            Pink.BackColor = Color.Pink;
            Green.BackColor = Color.Green;
            Cyan.BackColor = Color.Cyan;

            CheaterAutoCorrect.Text = "CHEATER!";
        }

        private void Play()
        {
            RedWhite.Enabled = true;
            YellowBlack.Enabled = true;
            Blue.Enabled = true;
            Pink.Enabled = true;
            Green.Enabled = true;
            Cyan.Enabled = true;
            CheaterAutoCorrect.Enabled = true;
            Accept.Enabled = true;
        }

        private void StopPlaying()
        {
            RedWhite.Enabled = false;
            YellowBlack.Enabled = false;
            Blue.Enabled = false;
            Pink.Enabled = false;
            Green.Enabled = false;
            Cyan.Enabled = false;
            CheaterAutoCorrect.Enabled = false;
            Accept.Enabled = false;
        }

        public void DecodeMessage(string msg)
        {
            string msg2 = msg.Substring(0, msg.IndexOfAny(new char[] { '\n' }) + 1);

            if (msg2.StartsWith(messageString))
            {
                recievingMessages.Enqueue(msg2.Substring(1, msg2.Length - 2));
            }
            else if (msg2.StartsWith(startString))
            {
                if (msg2.StartsWith(startString + "1"))
                {
                    playerType = PlayerType.Challenger;
                    actions.Enqueue(Layout2);
                }
                else if (msg2.StartsWith(startString + "2"))
                {
                    playerType = PlayerType.Challenged;
                    actions.Enqueue(Layout2);
                }
                else
                    MessageBox.Show("ERROR while trying to read data");
            }
            else if (msg2.StartsWith(playString))
                actions.Enqueue(Play);
            else if (msg2.StartsWith(stopPlayingString))
                actions.Enqueue(StopPlaying);

            if (msg.Length != msg2.Length)
                DecodeMessage(msg.Substring(msg.IndexOfAny(new char[] {'\n' }) + 1, msg.Length - msg2.Length));
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while (recievingMessages.Count > 0)
            {
                outputChat.Items.Add(recievingMessages.Dequeue());
            }

            while (actions.Count > 0)
                actions.Dequeue()();
        }

        protected override void OnClosed(EventArgs e)
        {
            stream.WriteByte((byte)'L');
            
            this.Hide();
            form.Show();
            this.Close();    

            base.OnClosed(e);
        }

        private void RedWhite_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                {

                }
            }

        }

        private void Blue_Click(object sender, EventArgs e)
        {

        }

        private void Green_Click(object sender, EventArgs e)
        {

        }

        private void YellowBlack_Click(object sender, EventArgs e)
        {

        }

        private void Pink_Click(object sender, EventArgs e)
        {

        }

        private void Cyan_Click(object sender, EventArgs e)
        {

        }

        private void CheaterAutoCorrect_Click(object sender, EventArgs e)
        {

        }

        private void Accept_Click(object sender, EventArgs e)
        {

        }
    }
}

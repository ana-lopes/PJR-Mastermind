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
        public const byte messageByte = (byte)'M', renameByte = (byte)'N', logoutByte = (byte)'L';
        public const byte firsSequenceByte = (byte)'A', guessByte = (byte)'C', correctionByte = (byte)'D';
        public const byte cheaterByte = (byte)'H';
        public const string startString = "S", playString = "P", stopPlayingString = "B";
        public const string messageString = "M", loginAprovedString = "O", errorString = "E";
        public const string guessString = "C", correctionString = "D";
        public const string victoryString = "V", defeatString = "Z", restartString = "R";

        static public GameForm instance;
        private Form form;

        public TcpClient client;
        public NetworkStream stream;

        private Queue<string> recievingMessages;
        private Queue<Action> actions;
        private string mensagem;

        List<string> sequence = new List<string>();

        int jogada;
        PlayerType playerType;

        public GameForm(TcpClient client, Form form)
        {
            instance = this;
            InitializeComponent();

            this.form = form;
            this.client = client;
            this.stream = client.GetStream();
            Jogada = 0;
            mensagem = "";

            //chat
            this.recievingMessages = new Queue<string>();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            actions = new Queue<Action>();
            Task thread = new Task(readData, recievingMessages);
            thread.Start();
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

        //redes
        private void readData<T>(T queue)
        {            
            while(true)
            {
                try
                {
                    while (!mensagem.Contains("\n"))
                    {
                        byte[] buffer = new byte[1024];
                        int nrBytes = stream.Read(buffer, 0, 1024);
                        mensagem += Encoding.ASCII.GetString(buffer, 0, nrBytes);
                    }

                    DecodeMessage();
                }
                catch (Exception e)
                {
                    MessageBox.Show("ERROR while trying to read data: \n" + e.ToString());
                    actions.Enqueue(this.Close);
                }
            }
        }
        
        private void Send_Click(object sender, EventArgs e)
        {
            string text = inputText.Text;

            if (text != "")
            {
                //enviar
                byte[] buf = Encoding.ASCII.GetBytes(text);
                stream.WriteByte((byte)messageByte);
                stream.Write(buf, 0, buf.Length);
                stream.WriteByte((byte)'\n');

                //limpar textbox
                inputText.Text = "";
            }
        }
        
        public void DecodeMessage()
        {
            string firstMessage = mensagem.Substring(0, mensagem.IndexOfAny(new char[] { '\n' }) + 1);

            Console.WriteLine(playerType.ToString() + firstMessage);

            if (firstMessage.StartsWith(messageString))
            {
                recievingMessages.Enqueue(firstMessage.Substring(1, firstMessage.Length - 2));
            }
            else if (firstMessage.StartsWith(startString))
            {
                if (firstMessage.StartsWith(startString + "1"))
                {
                    playerType = PlayerType.Challenger;
                    actions.Enqueue(Layout2);
                    actions.Enqueue(MyGame.instance.Start1);
                    Jogada = 0;
                }
                else if (firstMessage.StartsWith(startString + "2"))
                {
                    playerType = PlayerType.Challenged;
                    actions.Enqueue(Layout2);
                    actions.Enqueue(MyGame.instance.Start2);
                    Jogada = 0;
                }
                else
                    MessageBox.Show("ERROR while trying to read data");
            }
            else if (firstMessage.StartsWith(playString))
                actions.Enqueue(Play);

            else if (firstMessage.StartsWith(stopPlayingString))
                actions.Enqueue(StopPlaying);

            else if (firstMessage.StartsWith(guessString))
            {
                string colorString = mensagem.Substring(1, mensagem.IndexOfAny(new char[] { '\n' }));
                MainMenu.instance.tabuleiro.filas[10 - jogada].PorCor((ColorName)Enum.Parse(typeof(ColorName), colorString));
            }
            else if (firstMessage.StartsWith(correctionString))
            {
                string colorString = mensagem.Substring(1, mensagem.IndexOfAny(new char[] { '\n' }));
                MainMenu.instance.tabuleiro.filas[10 - jogada].PorCorrecao((ColorName)Enum.Parse(typeof(ColorName), colorString));
            }
            else if (firstMessage.StartsWith(victoryString))
            {
                actions.Enqueue(StopPlaying);
                MessageBox.Show("Victory"); 
                MainMenu.instance.tabuleiro.Reset();
                Jogada = 0;
            }
            else if (firstMessage.StartsWith(defeatString))
            {
                actions.Enqueue(StopPlaying);
                MessageBox.Show("Defeat");
                MainMenu.instance.tabuleiro.Reset();
                Jogada = 0;
            }
            else if (firstMessage.StartsWith(restartString))
            {
                actions.Enqueue(StopPlaying);
                sequence.Clear();
                MyGame.instance.Restart();
            }

            if (mensagem.Length != firstMessage.Length)
                mensagem = mensagem.Substring(mensagem.IndexOfAny(new char[] { '\n' }) + 1, mensagem.Length - firstMessage.Length);
            else
                mensagem = "";
        }

        private void Layout1() //challenger
        {            
            RedWhite.BackColor = Color.White;
            YellowBlack.BackColor = Color.Black;
            Blue.Visible = false;
            Pink.Visible = false;
            Green.Visible = false;
            Cyan.Visible = false;
            
            CheaterAutoCorrect.Text = "AutoCorrect";
        }

        private void Layout2() //challenged
        {
            RedWhite.BackColor = Color.Red;
            YellowBlack.BackColor = Color.Yellow;
            Blue.Visible = true;
            Pink.Visible = true;
            Green.Visible = true;
            Cyan.Visible = true;

            CheaterAutoCorrect.Text = "CHEATER!";
            Jogada = 0;
        }

        private void Play()
        {
            Jogada = jogada + 1;
            if (jogada != 1)
                CheaterAutoCorrect.Enabled = true;
            RedWhite.Enabled = true;
            YellowBlack.Enabled = true;
            Blue.Enabled = true;
            Pink.Enabled = true;
            Green.Enabled = true;
            Cyan.Enabled = true;
            Accept.Enabled = true;
            Undo.Enabled = true;
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
            Undo.Enabled = false;
        }

        private void RedWhite_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                    MainMenu.instance.tabuleiro.sequencia.PorCor(Microsoft.Xna.Framework.Color.Red);
                else
                    MainMenu.instance.tabuleiro.filas[10 - jogada + 1].PorCorrecao(Microsoft.Xna.Framework.Color.White);
            }
            else
                MainMenu.instance.tabuleiro.filas[10 - jogada].PorCor(Microsoft.Xna.Framework.Color.Red);
        }

        private void Blue_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                    MainMenu.instance.tabuleiro.sequencia.PorCor(Microsoft.Xna.Framework.Color.Blue);
            }
            else
                MainMenu.instance.tabuleiro.filas[10 - jogada].PorCor(Microsoft.Xna.Framework.Color.Blue);
        }

        private void Green_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                    MainMenu.instance.tabuleiro.sequencia.PorCor(Microsoft.Xna.Framework.Color.Green);
            }
            else
                MainMenu.instance.tabuleiro.filas[10 - jogada].PorCor(Microsoft.Xna.Framework.Color.Green);
        }

        private void YellowBlack_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                    MainMenu.instance.tabuleiro.sequencia.PorCor(Microsoft.Xna.Framework.Color.Yellow);
                else
                    MainMenu.instance.tabuleiro.filas[10 - jogada + 1].PorCorrecao(Microsoft.Xna.Framework.Color.Black);
            }
            else
                MainMenu.instance.tabuleiro.filas[10 - jogada].PorCor(Microsoft.Xna.Framework.Color.Yellow);
        }

        private void Pink_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                {
                    MainMenu.instance.tabuleiro.sequencia.PorCor(Microsoft.Xna.Framework.Color.Pink);
                }
            }
            else
            {
                MainMenu.instance.tabuleiro.filas[10 - jogada].PorCor(Microsoft.Xna.Framework.Color.Pink);
            }
        }

        private void Cyan_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                {
                    MainMenu.instance.tabuleiro.sequencia.PorCor(Microsoft.Xna.Framework.Color.Cyan);
                }
            }
            else
            {
                MainMenu.instance.tabuleiro.filas[10 - jogada].PorCor(Microsoft.Xna.Framework.Color.Cyan);
            }
        }

        private void CheaterAutoCorrect_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                int pretas = 0, brancas = 0;
                List<string> list = new List<string>();
                for (int i = 0; i<4; i++)
                {
                    list.Add(MainMenu.instance.tabuleiro.filas[10 - jogada + 1].GetColorSequence(i));
                    MainMenu.instance.tabuleiro.filas[10 - jogada + 1].TirarCorrecao();
                }
                foreach (string s in sequence)
                {
                    foreach (string s2 in list)
                    {
                        if (s == s2)
                        {
                            if (sequence.IndexOf(s) == list.IndexOf(s2))
                                pretas++;
                            else
                                brancas++;
                        }
                    }
                }
                for(int i = 0; i < pretas; i++)
                {
                    MainMenu.instance.tabuleiro.filas[10 - jogada + 1].PorCorrecao(ColorName.Preto);
                }
                for (int i = 0; i < brancas; i++)
                {
                    MainMenu.instance.tabuleiro.filas[10 - jogada + 1].PorCorrecao(ColorName.Branco);
                }
            }
            else
            {
                stream.WriteByte(cheaterByte);
                stream.WriteByte((byte)'\n');
            }
        }

        private void Accept_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                {
                    if (MainMenu.instance.tabuleiro.sequencia.indexCor == 4)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            stream.WriteByte(firsSequenceByte);
                            string colors = MainMenu.instance.tabuleiro.sequencia.GetColorSequence(i);
                            byte[] bfr = Encoding.ASCII.GetBytes(colors);
                            stream.Write(bfr, 0, bfr.Length);
                            stream.WriteByte((byte)'\n');
                            sequence.Add(colors);
                        }
                        Layout1();
                    }
                    else
                        MessageBox.Show("Please write a sequence");
                }
                else
                {
                    for (int i = 0; i < 4; i++)
                    {
                        stream.WriteByte(correctionByte);
                        string colors = MainMenu.instance.tabuleiro.filas[10 - jogada + 1].GetCorrectionSequence(i);
                        byte[] bfr = Encoding.ASCII.GetBytes(colors);
                        stream.Write(bfr, 0, bfr.Length);
                        stream.WriteByte((byte)'\n');
                    }
                }
            }
            else
            {
                if (MainMenu.instance.tabuleiro.filas[10 - jogada].indexCor == 4)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        stream.WriteByte(guessByte);
                        string colors = MainMenu.instance.tabuleiro.filas[10-jogada].GetColorSequence(i);
                        byte[] bfr = Encoding.ASCII.GetBytes(colors);
                        stream.Write(bfr, 0, bfr.Length);
                        stream.WriteByte((byte)'\n');
                    }
                }
                else 
                {
                    MessageBox.Show("Please write a sequence");
                }
            }
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            if (playerType == PlayerType.Challenger)
            {
                if (jogada == 1)
                    MainMenu.instance.tabuleiro.sequencia.TirarCor();
                else
                    MainMenu.instance.tabuleiro.filas[10 - jogada + 1].TirarCorrecao();
            }
            else
            {
                MainMenu.instance.tabuleiro.filas[10 - jogada].TirarCor();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            stream.WriteByte(logoutByte);
            stream.WriteByte((byte)'\n');
            this.Hide();
            base.OnClosed(e);
            form.Show();
        }

        private int Jogada
        {
            set 
            {
                jogada = value;
                Console.WriteLine("jogada: " + jogada);
            }
        }
    }
}

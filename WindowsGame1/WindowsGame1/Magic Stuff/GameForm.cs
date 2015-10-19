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
#endregion

namespace WindowsGame1
{
    public partial class GameForm : Form
    {
        static public GameForm instance;
        private SplitContainer splitContainer;

        public TcpClient client;
        public NetworkStream stream;
        private Queue<string> recievingMessages;
        private Form form;
        private bool closing;

        public GameForm(TcpClient client, Form form)
        {
            instance = this;
            InitializeComponent();
            splitContainer = splitContainer1;

            this.form = form;
            this.client = client;
            this.stream = client.GetStream();

            //chat
            this.recievingMessages = new Queue<string>();

            Task thread = new Task(readData, recievingMessages);
            thread.Start();
        }

        public SplitContainer SplitContainer
        {
            get { return splitContainer; }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }

        private void Send_Click(object sender, EventArgs e)
        {
            string msg = inputText.Text;

            //enviar
            byte[] buf = Encoding.ASCII.GetBytes(msg);
            stream.WriteByte((byte)'M');
            stream.Write(buf, 0, buf.Length);
            stream.WriteByte((byte)'\n');

            //limpar textbox
            inputText.Text = "";
        }

        //redes
        private void readData<T>(T queue)
        {
            Queue<string> q = queue as Queue<string>;
            
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

                    if (msg.StartsWith("M"))
                    {
                        q.Enqueue(msg.Substring(1, msg.Length - 2));
                    }
                }
                catch
                {

                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while (recievingMessages.Count > 0)
            {
                outputChat.Items.Add(recievingMessages.Dequeue());
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            if(!closing)
            {
                //closing = true;
                stream.WriteByte((byte)'L');
                stream.WriteByte((byte)'\n');

                this.Hide();
                form.Show();
                this.Close();                
            }
            base.OnClosed(e);
        }
    }
}

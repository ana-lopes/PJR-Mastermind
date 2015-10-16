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
                        msg += Encoding.ASCII.GetString(buffer, 0, 1024);
                    } while (!msg.StartsWith("M"));

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

    }
}

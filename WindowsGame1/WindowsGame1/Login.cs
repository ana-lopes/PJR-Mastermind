using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace WindowsGame1
{
    public partial class Login : Form
    {
        TcpClient client;
        NetworkStream stream;

        public Login()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string host = IPTextBox.Text;
            string name = NickTextBox.Text;
            int nrBytes = 0;

            if(host == "" || name == "")
            {
                MessageBox.Show("Please fill Host and/or Username.");
                return;
            }

            try
            {
                client = new TcpClient(host, 8888);
            }
            catch(Exception)
            {
                MessageBox.Show("Error connecting to server.");
                return;
            }

            stream = client.GetStream();

            //enviar pedido de autenticação (nome)
            stream.WriteByte((byte)'N');
            byte[] bfr = Encoding.ASCII.GetBytes(name);
            stream.Write(bfr, 0, bfr.Length);
            stream.WriteByte((byte)'\n');

            //receber resposta
            string msg = "";
            do
            {
                nrBytes = stream.Read(bfr, 0, bfr.Length);
                msg += Encoding.ASCII.GetString(bfr, 0, nrBytes);
            } while (!msg.Contains("\n"));

            if(msg.StartsWith("O"))
            {
                new GameForm(client, this).Show();
                this.Hide();
            }

            else
            {
                MessageBox.Show("Server dont like you! " + msg);
                stream.Close();
                client.Close();
            }
        }
    }
}

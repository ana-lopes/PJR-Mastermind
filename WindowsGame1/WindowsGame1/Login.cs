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
            int port; //TODO: try parse direito cuz errors no if 
            string name = NickTextBox.Text;
            int nrBytes = 0;

            if(host == "" || name == "" || portTextBox.Text == "0" || !Int32.TryParse(portTextBox.Text, out port))
            {
                MessageBox.Show("Please fill Host and/or Username.");
                return;
            }

            try
            {
                client = new TcpClient(host, port);
            }
            catch(Exception)
            {
                MessageBox.Show("Error connecting to server.");
                return;
            }

            stream = client.GetStream();

            //enviar pedido de autenticação (nome)
            stream.WriteByte(GameForm.renameByte);
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

            if(msg.StartsWith(GameForm.loginAprovedString))
            {
                new GameForm(client, this).Show();
                this.Hide();
            }
            else
            { 
                if (msg.StartsWith(GameForm.errorString + "0"))
                    MessageBox.Show("That name is already in use!");
                else if (msg.StartsWith(GameForm.errorString + "1"))
                    MessageBox.Show("Server Full!");                    
                else
                    MessageBox.Show("Server dont like you!");
                stream.Close();
                client.Close();
            }
        }
    }
}

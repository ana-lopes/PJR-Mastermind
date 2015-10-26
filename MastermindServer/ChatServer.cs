using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MastermindServer
{
    class ChatServer
    {
        public static Dictionary<string, ChatClient> clients;
        TcpListener listener;
        public Queue<string> messageQueue; // mensagens a enviar
        public const int maxNumberPlayers = 2; 
        string nameP1 = "", nameP2 = "";
        bool allThreadsBusy = true;

        public ChatServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            clients = new Dictionary<string, ChatClient>();
            messageQueue = new Queue<string>();

            //  listener.Server.SendTimeout = 500;
            //  listener.Server.ReceiveTimeout = 500;

            listener.Start();
        }

        public void Run()
        {
            while (true)
            {
                if (allThreadsBusy && clients.Count<maxNumberPlayers)
                {
                    allThreadsBusy = false;
                    createMyThread();
                }
                // se temos mensagens para enviar, enviar uma...
                if (messageQueue.Count > 0)
                {
                    // converter mensagem para bytes
                    string msg = messageQueue.Dequeue();
                    byte[] msgBuffer = Encoding.ASCII.GetBytes(msg);

                    // para cada cliente, enviar-lhe mensagem
                    foreach (var client in clients.Values.ToList())
                        client.SendMessage(msgBuffer);
                }
            }
        }

        public void StartGame()
        {
            int player = 1;
            nameP1 = nameP2 = "";
            foreach (KeyValuePair<string, ChatClient> pair in clients)
            {                
                pair.Value.Start(player == 1 ? (byte)'1' : (byte)'2');
                if (nameP1 == "")
                    nameP1 = pair.Key;
                else
                    nameP2 = pair.Key;
                Console.WriteLine("Start send to " + pair.Key);
                player++;
            }
            messageQueue.Enqueue(nameP1 + " is setting the sequence. " + nameP2 + " please stand by");
            clients[nameP1].Play();
            clients[nameP2].StopPlaying();
        }

        public bool Rename(string oldName, string newName)
        {
            if (clients.ContainsKey(newName))
                return false;
            else
            {
                clients.Add(newName, clients[oldName]);
                clients.Remove(oldName);
                return true;
            }
        }        

        async void createMyThread()
        {
            Console.WriteLine("Awaiting");
            TcpClient client =
                await listener.AcceptTcpClientAsync();
            allThreadsBusy = true;

            Console.WriteLine("Connected from " +
                client.Client.RemoteEndPoint.ToString());

            ChatClient chatClient = new ChatClient(client, this);
            clients.Add(client.Client.RemoteEndPoint.ToString(),
                chatClient);
            chatClient.Run();
        }

        public int NumberClients
        {
            get { return clients.Count; }
        }
    }
}

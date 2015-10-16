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
                if (allThreadsBusy)
                {
                    allThreadsBusy = false;
                    createMyThread();
                }
            }
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
    }
}

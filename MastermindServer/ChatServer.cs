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
        TcpListener listener;
        public const int maxNumberPlayers = 2;
        bool allThreadsBusy = true;
        public Dictionary<string, ChatClient> clients;

        public Queue<string> messageQueue; // mensagens a enviar
        string nameP1, nameP2;

        public List<string> firstSequence;
        public List<string> guessSequence;
        public List<string> correctionSequence;
        public int brancas/*lugar errado*/, pretas/*lugar certo*/;
        public bool challengerIsCheater, invertido = false;
        public int jogada;

        public ChatServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
            clients = new Dictionary<string, ChatClient>();
            messageQueue = new Queue<string>();
            firstSequence = new List<string>();
            guessSequence = new List<string>();
            correctionSequence = new List<string>();

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
                    string msg = messageQueue.Dequeue();

                    // para cada cliente, enviar-lhe mensagem
                    foreach (var client in clients.Values.ToList())
                        client.messageQueue.Enqueue(msg);
                }
            }
        }

        public void StartGame()
        {
            int player = 1;
            jogada = 0;
            challengerIsCheater = false;
            nameP1 = nameP2 = "";
            firstSequence.Clear();
            guessSequence.Clear();
            correctionSequence.Clear();

            foreach (KeyValuePair<string, ChatClient> pair in clients)
            {                
                pair.Value.Start(player == 1 ? (!invertido ? "1" : "2") : (!invertido ? "2" : "1"));
                if (!invertido)
                {
                    if (nameP1 == "") 
                        nameP1 = pair.Key;
                    else 
                        nameP2 = pair.Key;
                }
                else
                {
                    if (nameP2 == "") 
                        nameP2 = pair.Key;
                    else
                        nameP1 = pair.Key;
                }
                Console.WriteLine("Start send to " + pair.Key);
                player++;
            }

            messageQueue.Enqueue(ChatClient.messageString + nameP1 + " is setting the sequence. " + nameP2 + " please stand by\n");
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

        public void SendGuess()
        {
            foreach (string s in guessSequence)
            {
                clients[nameP1].messageQueue.Enqueue(ChatClient.guessString + s + "\n");
            }
            correctionSequence.Clear();
        }

        public int VerefyGuess()
        {
            brancas = pretas = 0;
            foreach (string s in firstSequence)
            {
                foreach (string s2 in guessSequence)
                {
                    if (s == s2)
                    {
                        if (firstSequence.IndexOf(s) == guessSequence.IndexOf(s2))
                            pretas++;
                        else
                            brancas++;
                    }
                }
            }
            jogada++;
            if (pretas == 4)
                return 1;
            else if (jogada == 10)
                return -1;
            else return 0;
        }

        public void SendCorrection()
        {
            foreach (string s in correctionSequence)
            {
                clients[nameP2].messageQueue.Enqueue(ChatClient.correctionString + s + "\n");
            }
            guessSequence.Clear();
        }

        public void VerefyCorrection()
        {
            if (!challengerIsCheater)
            {
                int brancas = 0;
                int pretas = 0;
                foreach (string s in correctionSequence)
                {
                    if (s == "Preto")
                        pretas++;
                    else if (s == "Branco")
                        brancas++;
                }

                if (this.brancas != brancas || this.pretas != pretas)
                    challengerIsCheater = true;
            }
        }

        public void ChallengerWin()
        {
            clients[nameP1].messageQueue.Enqueue(ChatClient.victoryString + "\n");
            clients[nameP2].messageQueue.Enqueue(ChatClient.defeatString + "\n");
            invertido = !invertido;
            StartGame();
        }

        public void ChallengedWin()
        {
            clients[nameP2].messageQueue.Enqueue(ChatClient.victoryString + "\n");
            clients[nameP1].messageQueue.Enqueue(ChatClient.defeatString + "\n");
            invertido = !invertido;            
            StartGame();
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

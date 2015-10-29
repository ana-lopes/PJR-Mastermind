using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MastermindServer
{
    class ChatClient
    {
        /*public const byte messageByte = (byte)'M', errorByte = (byte)'E', loginAprovedByte = (byte)'O';
        public const byte startByte = (byte)'S', playByte = (byte)'P', stopPlayingByte = (byte)'B';
        public const byte guessByte = (byte)'C', correctionByte = (byte)'D';
        public const byte victoryByte = (byte)'V', defeatByte = (byte)'Z';*/
        public const string renameString = "N", loginAprovedString = "O", messageString = "M";
        public const string logoutString = "L", startString = "S", playString = "P"; 
        public const string stopPlayingString = "B", errorString = "E", firstSequenceString = "A";
        public const string guessString = "C", correctionString = "D", victoryString = "V";
        public const string defeatString = "Z", restartString = "R", cheaterString = "H";

        bool authenticated = false;
        bool playing = false;
        bool dead;
        string mensagem = "";
        public Queue<string> messageQueue;

        TcpClient client;
        public NetworkStream stream;

        ChatServer server;

        string nick;

        public ChatClient(TcpClient client, ChatServer server)
        {
            messageQueue = new Queue<string>();
            this.server = server;
            this.client = client;
            this.stream = client.GetStream();
            // Inicialmente o nosso nick é o nosso IP e porta
            this.nick = client.Client.RemoteEndPoint.ToString();
            dead = false;
            Thread thread = new Thread(SendMessage);
            thread.Start();
        }
        
        public void Run()
        {
            while (true)
            {
                // Ler a mensagem que pode ser dividida por pacotes IP.
                // Daí se ter convencionado que cada linha termina com newline.
                try
                {
                    ReadMessage();

                    // O processamento das mensagens recebidas depende do estado em que o 
                    // cliente está: antes ou depois de se ter autenticado.
                    if (authenticated)
                    {
                        if (!DecodeMessage())
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (!TryAuthentication())
                        {
                            Die("TryAuthentication");
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Die("Run");
                    break;
                }
            }
        }

        public void ReadMessage()
        {
            while (!mensagem.Contains("\n"))
            {
                byte[] buffer = new byte[1024];
                int readBytes = stream.Read(buffer, 0, 1024);
                mensagem += Encoding.ASCII.GetString(buffer, 0, readBytes);
            }

        }

        public void SendMessage()
        {
            while (true)
            {
                if (messageQueue.Count > 0)
                {
                    try
                    {
                        string msg = messageQueue.Dequeue();
                        byte[] buffer = Encoding.ASCII.GetBytes(msg);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    catch { Die("SendMessage"); }
                }
            }
        }

        public bool TryAuthentication()
        {
            Console.WriteLine(nick + " não autenticado");
            // Se não está autenticado, só recebemos mensagens do tipo N, com
            // o nick desejado. 
            if (mensagem.StartsWith(renameString))
            {
                if (server.NumberClients <= ChatServer.maxNumberPlayers)
                {
                    bool b = TryRename();
                    if (b && server.NumberClients == ChatServer.maxNumberPlayers)
                        server.StartGame();
                    
                    mensagem = "";

                    return b;
                }
                else
                {
                    messageQueue.Enqueue(errorString + "1\n");
                    
                    Die("MaxNumberPlayers");

                    mensagem = "";

                    return false;
                }
            }
            else
            {
                Die("FailedAuthentication");

                mensagem = "";

                return false;
            }  
        }

        public bool TryRename()
        {
            //Tentamos usar esse nick. Se estiver livre, retornamos
            // com mensagem de sucesso "O"
            // se o nick estiver em uso, retornamos com mensagem de erro "E0"
            // se a mensagem não estiver de acordo com o protocolo, matamos cliente.
            string novoNick = mensagem.Substring(1,
                mensagem.IndexOfAny(
                    new char[] { '\n', '\r' }) - 1);
            if (server.Rename(
                client.Client.RemoteEndPoint.ToString(),
                novoNick))
            {
                // we are authenticated
                Console.WriteLine(novoNick + " ligou-se!");
                if (!authenticated)
                {
                    authenticated = true;
                    this.nick = novoNick;
                    messageQueue.Enqueue(loginAprovedString + "\n");
                    Console.WriteLine(nick + " foi autenticado");
                    server.messageQueue.Enqueue(messageString + novoNick + " ligou-se!\n");
                }
                return true;
            }
            else
            {
                // nop, that nick is in use.

                messageQueue.Enqueue(errorString + "0\n");
                Die("FailedTryRename");
                return false;
            }

        }

        public bool DecodeMessage()
        {            
            string firstMessage = this.mensagem.Substring(0, this.mensagem.IndexOfAny(new char[] { '\n' }) + 1);
            // Se está autenticado, recebe mensagens do tipo M. Extrai a mensagem
            // e coloca-a na queue do ChatServer para que este envie para todos os
            // clientes. 
            // Se a mensagem é desconhecida, então mata-se o cliente por não 
            // respeitar o protocolo.
            Console.WriteLine(nick + " autenticado");

            if (firstMessage.StartsWith(messageString))
            {
                return ProcessMessage(firstMessage);
            }
            else if (firstMessage.StartsWith(firstSequenceString))
            {
                return ProcessFirstSequence(firstMessage);
            }
            else if (firstMessage.StartsWith(guessString))
            {
                return ProcessGuess(firstMessage);
            }
            else if (firstMessage.StartsWith(correctionString))
            {
                return ProcessCorrection(firstMessage);
            }
            else if (firstMessage.StartsWith(logoutString) && !dead)
            {
                return ProcessLogout(firstMessage);
            }
            else if (firstMessage.StartsWith(cheaterString))
            {
                return ProcessCheater(firstMessage);
            }
            else
            {
                Die("FailedDecodeMessage");
                return false;
            } 
        }

        private bool ProcessCheater(string firstMessage)
        {
            string cheater = firstMessage.Substring(1, firstMessage.IndexOfAny(new char[] { '\n', '\r' }) - 1);

            if (server.challengerIsCheater)
            {
                server.ChallengedWin();
                server.messageQueue.Enqueue(messageString + "CHEATER FOUND! You should be ashamed\n");
            }
            else
            {
                server.ChallengerWin();
                server.messageQueue.Enqueue(messageString + "NOT A CHEATER! Have faith in humanity\n");
            }

            if (mensagem.Length != firstMessage.Length)
            {
                mensagem = mensagem.Substring(mensagem.IndexOfAny(new char[] { '\n' }) + 1, mensagem.Length - firstMessage.Length);
            }
            else
                mensagem = "";
            return true;
        }

        private bool ProcessMessage(string firstMessage)
        {
            string messageContent = firstMessage.Substring(1, firstMessage.IndexOfAny(new char[] { '\n', '\r' }) - 1);

            //rename nick 
            if (firstMessage.StartsWith(messageString + "/rename"))
            {
                string newNick = messageContent.Substring(8, messageContent.Length - 8);
                string oldname = nick;

                if (server.Rename(nick, newNick))
                {
                    nick = newNick;
                    newNick = messageString + oldname + " changed their name to " + nick + "\n";
                    server.messageQueue.Enqueue(newNick);

                    Console.WriteLine(newNick);
                }

                else
                {
                    newNick = messageString + nick + " can't change name to " + newNick + " because it's already in use. Try again.\n";
                    messageQueue.Enqueue(newNick);

                    Console.WriteLine(newNick);
                }

            }
            //if plain message
            else
            {
                messageContent = messageString + nick + ": " + messageContent + "\n";
                server.messageQueue.Enqueue(messageContent);
                Console.WriteLine(messageContent);
            }

            if (mensagem.Length != firstMessage.Length)
            {
                mensagem = mensagem.Substring(mensagem.IndexOfAny(new char[] { '\n' }) + 1, mensagem.Length - firstMessage.Length);
            }
            else
                mensagem = "";
            return true;
        }

        private bool ProcessFirstSequence(string firstMessage)
        {
            string color = firstMessage.Substring(1, firstMessage.Length - 2);
            server.firstSequence.Add(color);
            Console.WriteLine(nick + " mandou " + color);
            if (server.firstSequence.Count == 4)
            {
                Console.WriteLine(nick + " mandou sequencia completa");
                server.messageQueue.Enqueue(messageString + "Sequence sent\n");
                foreach (KeyValuePair<string, ChatClient> pair in server.clients)
                {
                    pair.Value.Switch();
                }
            }
            if (mensagem.Length != firstMessage.Length)
            {
                mensagem = mensagem.Substring(mensagem.IndexOfAny(new char[] { '\n' }) + 1, mensagem.Length - firstMessage.Length);
            }
            else
                mensagem = "";
            return true;
        }

        private bool ProcessLogout(string firstMessage)
        {
            string logout = messageString + nick + " logging out.\n";
            server.messageQueue.Enqueue(logout);

            Console.WriteLine(nick + " logging out");

            Die("Logout");
            if (mensagem.Length != firstMessage.Length)
            {
                mensagem = mensagem.Substring(mensagem.IndexOfAny(new char[] { '\n' }) + 1, mensagem.Length - firstMessage.Length);
            }
            else
                mensagem = "";
            return true;
        }

        private bool ProcessGuess(string firstMessage)
        {
            string color = firstMessage.Substring(1, firstMessage.Length - 2);
            server.guessSequence.Add(color);
            Console.WriteLine(nick + " mandou " + color);
            int result = 2;
            if (server.guessSequence.Count == 4)
            {
                Console.WriteLine(nick + " mandou sequencia completa");
                result = server.VerefyGuess();
                foreach (KeyValuePair<string, ChatClient> pair in server.clients)
                {
                    pair.Value.Switch();
                }
                server.SendGuess();
            }

            if (result == 1)
                server.ChallengedWin();
            else if (result == -1)
                server.ChallengerWin();
            else if (result == 0)
                server.messageQueue.Enqueue(messageString + "Challenged has sent guess\n");
            
            if (mensagem.Length != firstMessage.Length)
            {
                mensagem = mensagem.Substring(mensagem.IndexOfAny(new char[] { '\n' }) + 1, mensagem.Length - firstMessage.Length);
            }
            else
                mensagem = "";
            return true;
        }

        private bool ProcessCorrection(string firstMessage)
        {
            string color = firstMessage.Substring(1, firstMessage.Length - 2);
            server.correctionSequence.Add(color);
            Console.WriteLine(nick + " mandou " + color);
            if (server.correctionSequence.Count == 4)
            {
                Console.WriteLine(nick + " mandou sequencia completa");
                server.messageQueue.Enqueue(messageString + "Challenger has sent correction\n");
                server.VerefyCorrection();
                foreach (KeyValuePair<string, ChatClient> pair in server.clients)
                {
                    pair.Value.Switch();
                }
                server.SendCorrection();
            }            

            if (mensagem.Length != firstMessage.Length)
            {
                mensagem = mensagem.Substring(mensagem.IndexOfAny(new char[] { '\n' }) + 1, mensagem.Length - firstMessage.Length);
            }
            else
                mensagem = "";
            return true;
        }

        public void Start(string b)
        {
            messageQueue.Enqueue(startString + b + "\n");
        }

        public void Switch()
        {
            if (playing)
                StopPlaying();
            else
                Play();
        }

        public void Play()
        {
            messageQueue.Enqueue(playString + "\n");
            playing = true;
        }

        public void StopPlaying()
        {
            messageQueue.Enqueue(stopPlayingString + "\n");
            playing = false;
        }

        public void Die(string motivo)
        {
            // Matar um cliente é fechar a ligação e removê-lo da lista
            // de clientes ativos. Também adicionamos uma mensagem a enviar
            // aos restantes clientes a avisar que o utilizador se desligou.
            stream.Close();
            client.Close();

            if (server.clients.Remove(nick))
            {
                Console.WriteLine(nick + " desligou-se");
                dead = true;
                foreach (KeyValuePair<string, ChatClient> pair in server.clients)
                {
                    pair.Value.messageQueue.Enqueue(restartString + "\n");
                }
            }
        }

    }
}

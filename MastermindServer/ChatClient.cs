using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MastermindServer
{
    class ChatClient
    {
        public const byte messageByte = (byte)'M', errorByte = (byte)'E', loginAprovedByte = (byte)'O';
        public const byte startByte = (byte)'S', playByte = (byte)'P', stopPlayingByte = (byte)'B';
        public const byte guessByte = (byte)'C', correctionByte = (byte)'D';
        public const byte victoryByte = (byte)'V', defeatByte = (byte)'Z';
        public const string renameString = "N", messageString = "M", logoutString = "L";
        public const string firstSequenceString = "A", guessString = "C", correctionString = "D";

        bool authenticated = false;
        bool playing = false;
        bool dead;
        string mensagem = "";

        TcpClient client;
        public NetworkStream stream;

        ChatServer server;

        string nick;

        public ChatClient(TcpClient client, ChatServer server)
        {
            this.server = server;
            this.client = client;
            this.stream = client.GetStream();
            // Inicialmente o nosso nick é o nosso IP e porta
            this.nick = client.Client.RemoteEndPoint.ToString();
            dead = false;
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

        public void SendMessage(byte[] buffer)
        {
            try
            {
                stream.WriteByte(messageByte);
                stream.Write(buffer, 0, buffer.Length);
                stream.WriteByte((byte)'\n');
            }
            catch
            {
                Die("SendMessage");
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
                    stream.WriteByte(errorByte);
                    stream.WriteByte((byte)'1');
                    stream.WriteByte((byte)'\n');
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
                    stream.WriteByte((byte)loginAprovedByte);
                    stream.WriteByte((byte)'\n');
                    Console.WriteLine(nick + " foi autenticado");
                    server.messageQueue.Enqueue(novoNick + " ligou-se!");
                }
                return true;
            }
            else
            {
                // nop, that nick is in use.
                stream.WriteByte((byte)errorByte);
                stream.WriteByte((byte)'0');
                stream.WriteByte((byte)'\n');
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
            else
            {
                Die("FailedDecodeMessage");
                return false;
            } 
        }

        private bool ProcessMessage(string firstMessage)
        {
            string messageContent = firstMessage.Substring(1, firstMessage.IndexOfAny(new char[] { '\n', '\r' }) - 1);

            //rename nick 
            if (firstMessage.StartsWith(messageContent + "/rename"))
            {
                string newNick = messageContent.Substring(8, messageContent.Length - 8);
                string oldname = nick;

                if (server.Rename(nick, newNick))
                {
                    nick = newNick;
                    newNick = oldname + " changed their name to " + nick;
                    server.messageQueue.Enqueue(newNick);

                    Console.WriteLine(newNick);
                }

                else
                {
                    newNick = nick + " can't change name to " + newNick + " because it's already in use. Try again.";
                    server.messageQueue.Enqueue(newNick);

                    Console.WriteLine(newNick);
                }

            }
            //if plain message
            else
            {
                messageContent = nick + ": " + messageContent;
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
            string logout = nick + " logging out. ";
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
            int result = 0;
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

        public void Start(byte b)
        {
            stream.WriteByte(startByte);
            stream.WriteByte(b);
            stream.WriteByte((byte)'\n');
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
            stream.WriteByte((byte)playByte);
            stream.WriteByte((byte)'\n');
            playing = true;
        }

        public void StopPlaying()
        {
            stream.WriteByte((byte)stopPlayingByte);
            stream.WriteByte((byte)'\n');
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
            }
        }

    }
}

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
        const byte messageByte = (byte)'M', errorByte = (byte)'E', loginAprovedByte = (byte)'O';
        const byte startByte = (byte)'S', playByte = (byte)'P', stopPlayingByte = (byte)'B';
        const string renameString = "N", messageString = "M", logoutString = "L"; 

        bool authenticated = false;
        bool dead;

        TcpClient client;
        NetworkStream stream;

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
                Die();
            }
        }

        public void Run()
        {
            while (true)
            {
                // Ler a mensagem que pode ser dividida por pacotes IP.
                // Daí se ter convencionado que cada linha termina com newline.
                try
                {
                    string mensagem = ReadMessage();

                    // O processamento das mensagens recebidas depende do estado em que o 
                    // cliente está: antes ou depois de se ter autenticado.
                    if (authenticated)
                    {
                        if (!DecodeMessage(mensagem))
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (!TryAuthentication(mensagem))
                        {
                            Die();
                            break;
                        }
                    }
                }
                catch
                {
                    Die();
                    break;
                }
            }
        }

        public bool TryAuthentication(string mensagem)
        {
            Console.WriteLine(nick + " não autenticado");
            // Se não está autenticado, só recebemos mensagens do tipo N, com
            // o nick desejado. 
            if (mensagem.StartsWith(renameString))
            {
                if (server.NumberClients <= ChatServer.maxNumberPlayers)
                {
                    bool b = TryRename(mensagem);
                    if (b && server.NumberClients == ChatServer.maxNumberPlayers)
                        server.StartGame();
                    return b;
                }
                else
                {
                    stream.WriteByte(errorByte);
                    stream.WriteByte((byte)'1');
                    stream.WriteByte((byte)'\n');
                    Die();
                    return false;
                }
            }
            else
            {
                Die();
                return false;
            }  
        }

        public string ReadMessage()
        {
            int readBytes;
            string mensagem = "";
            byte[] buffer = new byte[1024];
            do
            {
                readBytes = stream.Read(buffer, 0, 1024);
                mensagem += Encoding.ASCII.GetString(buffer, 0, readBytes);
            } while (!mensagem.Contains("\n"));

            return mensagem;
        }

        public bool DecodeMessage(string mensagem)
        {
            // Se está autenticado, recebe mensagens do tipo M. Extrai a mensagem
            // e coloca-a na queue do ChatServer para que este envie para todos os
            // clientes. 
            // Se a mensagem é desconhecida, então mata-se o cliente por não 
            // respeitar o protocolo.
            Console.WriteLine(nick + " autenticado");
            if (mensagem.StartsWith(messageString))
            {
                string msg = mensagem.Substring(1, mensagem.IndexOfAny(new char[] { '\n', '\r' }) - 1);
                msg = nick + ": " + msg;
                server.messageQueue.Enqueue(msg);
                Console.WriteLine(msg);
                return true;
            }
            else if (mensagem.StartsWith(logoutString) && !dead)
            {
                Console.WriteLine(nick + " logging out");

                dfgdfgdg //mandar notificacao
                Die();
                return true;
            }
            else
            {
                Die();
                return false;
            } 
        }

        public bool TryRename(string mensagem)
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
                Die();
                return false;
            }
                
        }

        public void Start(byte b)
        {
            stream.WriteByte(startByte);
            stream.WriteByte(b);
            stream.WriteByte((byte)'\n');
        }

        public void Play()
        {
            stream.WriteByte((byte)playByte);
            stream.WriteByte((byte)'\n'); 
        }

        public void StopPlaying()
        {
            stream.WriteByte((byte)stopPlayingByte);
            stream.WriteByte((byte)'\n');
        }

        public void Die()
        {
            // Matar um cliente é fechar a ligação e removê-lo da lista
            // de clientes ativos. Também adicionamos uma mensagem a enviar
            // aos restantes clientes a avisar que o utilizador se desligou.
            stream.Close();
            client.Close();

            if (ChatServer.clients.Remove(nick))
            {
                Console.WriteLine(nick + " desligou-se");
                server.messageQueue.Enqueue(nick + " desligou-se!");
                dead = true;
            }
        }

    }
}

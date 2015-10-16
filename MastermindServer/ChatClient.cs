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
        }

        public void Run()
        {
            while (true)
            {
                // Ler a mensagem que pode ser dividida por pacotes IP.
                // Daí se ter convencionado que cada linha termina com newline.
                byte[] buffer = new byte[1024];
                string mensagem = "";
                int readBytes;
                try
                {
                    do
                    {
                        readBytes = stream.Read(buffer, 0, 1024);
                        mensagem = mensagem +
                            Encoding.ASCII.GetString(buffer, 0, readBytes);
                    } while (!mensagem.Contains("\n"));

                    // O processamento das mensagens recebidas depende do estado em que o 
                    // cliente está: antes ou depois de se ter autenticado.
                    if (authenticated)
                    {
                        // Se está autenticado, recebe mensagens do tipo M. Extrai a mensagem
                        // e coloca-a na queue do ChatServer para que este envie para todos os
                        // clientes. 
                        // Se a mensagem é desconhecida, então mata-se o cliente por não 
                        // respeitar o protocolo.
                        Console.WriteLine(nick + " autenticado");
                        if (mensagem.StartsWith("M"))
                        {
                            string msg = mensagem.Substring(1,
                                          mensagem.IndexOfAny(
                                        new char[] { '\n', '\r' }) - 1);
                            msg = nick + ": " + msg;
                            server.messageQueue.Enqueue(msg);
                            Console.WriteLine(msg);
                        }
                        else if (mensagem.StartsWith("L") && !dead)
                        {
                            Console.WriteLine(nick + " logging out");
                            Die();
                        }
                        else
                        {
                            Die();
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine(nick + " não autenticado");
                        // Se não está autenticado, só recebemos mensagens do tipo N, com
                        // p nick desejado. Tentamos usar esse nick. Se estiver livre, retornamos
                        // com mensagem de sucesso "O"
                        // se o nick estiver em uso, retornamos com mensagem de erro "E0"
                        // se a mensagem não estiver de acordo com o protocolo, matamos cliente.
                        if (mensagem.StartsWith("N"))
                        {
                            string novoNick = mensagem.Substring(1,
                                mensagem.IndexOfAny(
                                    new char[] { '\n', '\r' }) - 1);
                            if (server.Rename(
                                client.Client.RemoteEndPoint.ToString(),
                                novoNick))
                            {
                                // we are authenticated
                                Console.WriteLine(novoNick + " ligou-se!");
                                authenticated = true;
                                this.nick = novoNick;
                                stream.WriteByte((byte)'O');
                                stream.WriteByte((byte)'\n');
                                Console.WriteLine(nick + " foi autenticado");
                                server.messageQueue.Enqueue(novoNick + " ligou-se!");
                            }
                            else
                            {
                                // nop, that nick is in use.
                                stream.WriteByte((byte)'E');
                                stream.WriteByte((byte)'0');
                                stream.WriteByte((byte)'\n');
                            }
                        }
                        else
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
                bool dead = true;
            }
        }

    }
}

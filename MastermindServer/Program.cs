using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MastermindServer
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Sou o Servidor!!!!\nQue port queres usar, bebé?");
            /*int port;
            while (!Int32.TryParse(Console.ReadLine(), out port))
            {
                Console.WriteLine("Não dá, bebé... :/");
            };
            Console.WriteLine("ok, bebé ;)");*/
            ChatServer server = new ChatServer(8888);
            server.Run();
        }
    }
}

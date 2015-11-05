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
            Console.WriteLine("Sou o Servidor!!!!\nQue port queres usar?");
            int port;
            while (!Int32.TryParse(Console.ReadLine(), out port))
            {
                Console.WriteLine("Não dá.. :/");
            };
            Console.WriteLine("ok ;)");
            ChatServer server = new ChatServer(port);
            server.Run();
        }
    }
}

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
            Console.WriteLine("Sou o Servidor!!!!");
            ChatServer server = new ChatServer(8888);
            server.Run();
        }
    }
}

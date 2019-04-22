using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Chat
{
    class Program
    {
        static string input;
        static Server server;
        static Client client;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter your role : !sv - server, !cl - user");
       
            string ip = "";
            input = "!sv";
            while (true)
            {
                //input = Console.ReadLine();
                if (input == "!sv")
                {
                    server = new Server();
                    ip = server.Ip;
                    break;
                }
                else if (input == "!cl")
                {
                    Console.WriteLine("Enter server IP:");
                    ip = Console.ReadLine();
                    break;
                }
                else
                {
                    Console.WriteLine("Enter correct");
                }
            }
            client = new Client(ip);

            _handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);

        }

        [DllImport("Kernel32")]
        private static extern bool SetConsoleCtrlHandler(EventHandler handler, bool add);

        private delegate bool EventHandler(CtrlType sig);
        static EventHandler _handler;

        enum CtrlType
        {
            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT = 1,
            CTRL_CLOSE_EVENT = 2,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT = 6
        }

        private static bool Handler(CtrlType sig)
        {
            switch (sig)
            {
                case CtrlType.CTRL_C_EVENT:
                case CtrlType.CTRL_LOGOFF_EVENT:
                case CtrlType.CTRL_SHUTDOWN_EVENT:
                case CtrlType.CTRL_CLOSE_EVENT:
                default:
                    if (input == "<server>") server.ExitServer();
                    else client.ExitClient();
                    return false;

            }
        }
    }
}

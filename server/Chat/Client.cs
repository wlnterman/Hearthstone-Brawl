using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace Chat
{
    class Client
    {
        Socket client;
        string name;
        bool work;

        public Client(string serverIPStr)
        {
            work = true;
            IPAddress ipServer;
            try
            {
                ipServer = IPAddress.Parse(serverIPStr);
            }
            catch (FormatException)
            {
                Console.WriteLine("Enter correct Server IP");
                string str = Console.ReadLine();
                ipServer = IPAddress.Parse(str);
            }
            
            IPEndPoint ipEndPoint = new IPEndPoint(ipServer, 8888);
            client = new Socket(ipServer.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(ipEndPoint);
            }
            catch (System.Net.Sockets.SocketException)
            {
                Console.WriteLine("Error : Server is not in network");
                Console.ReadLine();
            }
            Console.WriteLine("Введите имя");
            //name = Console.ReadLine();
            name = "Host";
            byte[] msg = Encoding.UTF8.GetBytes(name);
            int bytesSent = client.Send(msg);

            Thread sender = new Thread(Send);
            sender.Start();


            Thread reciever = new Thread(Recieve);
            reciever.Start();       
        }

        public void HelpMe()
        {
            Console.WriteLine("Write <list>, if you want to read the list of users");
            Console.WriteLine("Write <help>, if you want to read this tip  again");
            Console.WriteLine("Write <history>, if you want to see your message history");
            Console.WriteLine("Write <DM> 'name', if you want to send private message to the user called 'name'");
            Console.WriteLine("Other messages will be send to common chat");
            Console.WriteLine("Write <close>, if you want to exit chat");
        }

        public void Recieve()
        {
            while (work)
            {
                string message = null;
                byte[] bytes = new byte[1024];
                int bytesRec;
                try
                {
                    bytesRec = client.Receive(bytes);
                }
                catch (SocketException)
                {
                    Environment.Exit(1);
                    break;
                } 
                message += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                File.AppendAllText(name+"hystory.txt", message + Environment.NewLine);
                Console.WriteLine(message);
            }
        }

        public void Send()
        {
            while (work)
            {
                string message = Console.ReadLine();

                if (message=="<help>")
                {
                    HelpMe();
                }
                else if (message == "<list>")
                {
                    byte[] msg = Encoding.UTF8.GetBytes(message);
                    int bytesSent = client.Send(msg);
                }
                else if (message == "<history>")
                {
                    foreach (string line in  File.ReadLines(name+"hystory.txt"))
                    {
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    byte[] msg = Encoding.UTF8.GetBytes(message);
                    Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
                    int bytesSent = client.Send(msg);
                }
          
            }           
        }

        public void ExitClient()
        {
            byte[] msg = Encoding.UTF8.GetBytes("<close>" +" "+ name);
            int bytesSent = client.Send(msg);
            client.Close();
            work = false;
        }

    }
}

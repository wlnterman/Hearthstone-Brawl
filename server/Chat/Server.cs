using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Chat
{
    public class Server
    {
        Socket sListener;
        Dictionary<string, Socket> ClientsDict;
        public string Ip;
        bool work;


        public Server()
        {
            work = true;
            IPAddress ipAddr = GetLocalIPAddress();
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 8888);

            Ip = ipAddr.ToString();

            Console.WriteLine("Your IP: {0}", Ip);

            ClientsDict = new Dictionary<string, Socket>();
            sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            sListener.Bind(ipEndPoint);
            sListener.Listen(8);

            Thread recieverConnections = new Thread(RecieveConnections);
            recieverConnections.Start();
        }

        public void RecieveConnections()
        {
            while (work)
            {
                Socket client = sListener.Accept();

                string name = null;
                byte[] bytes = new byte[1024];
                int bytesRec = client.Receive(bytes);
                name += Encoding.UTF8.GetString(bytes, 0, bytesRec);

                ClientsDict[name] = client;
                Broadcast(name + " has joined the game");
                Thread clientHandler = new Thread(() => HandleClient(name));
                clientHandler.Start();
            }
        }

        public string WriteListOfClients()
        {
            string list = "";
            foreach (KeyValuePair<string, Socket> client in ClientsDict)
            {
                list = list + "\n" + client.Key;
            }
            return list;
        }

        public void HandleClient(string name)
        {
            while (work)
            {
                string message = null;
                byte[] bytes = new byte[1024];
                int bytesRec;
                try
                {
                    bytesRec = ClientsDict[name].Receive(bytes);
                }
                catch (SocketException)
                {      
                    break;
                }
                
                message += Encoding.UTF8.GetString(bytes, 0, bytesRec);
                if (message == "<list>")
                {      
                    byte[] mes = Encoding.UTF8.GetBytes(WriteListOfClients());
                    ClientsDict[name].Send(mes);
                }
                else if (message.Split(' ')[0] == "<DM>")
                {
                    string mes = "";
                    for (int i = 2; i < message.Split(' ').Count(); i++)
                    {
                        mes = mes + " " + message.Split(' ')[i];
                    }
                    SendPrivateMessage(name, message.Split(' ')[1],mes);
                }
                else if (message.Split(' ')[0] == "<close>")
                {
                    ClientsDict[message.Split(' ')[1]].Close();
                    ClientsDict.Remove(message.Split(' ')[1]);
                    Broadcast(message.Split(' ')[1]+"  has left the chat");
                    break;
                }
                else
                {
                    message = name + " : " + message;
                    Broadcast(message);
                }        
                
            }
        }

        public void SendPrivateMessage(string sender, string reciever, string message)
        {
            if (ClientsDict.ContainsKey(reciever))
            {
                message = sender + " to " + reciever + " : " + message;
                byte[] mes = Encoding.UTF8.GetBytes(message);
                ClientsDict[sender].Send(mes);
                ClientsDict[reciever].Send(mes);
            }
            else
            {
                byte[] mes = Encoding.UTF8.GetBytes("There is no such user in network");
                ClientsDict[sender].Send(mes);
            }
        }

        public void Broadcast(string message)
        {
            byte[] msg = Encoding.UTF8.GetBytes(message);
            foreach (KeyValuePair<string, Socket> client in ClientsDict)
            {
                client.Value.Send(msg);
            }
        }

        IPAddress GetLocalIPAddress()
        {
            string sHostName = Dns.GetHostName();
            IPHostEntry ipE = Dns.GetHostEntry(sHostName);
            return ipE.AddressList[ipE.AddressList.Count() - 1];
        }

        public void ExitServer()
        {
            Console.WriteLine("Server left");
            foreach (KeyValuePair<string, Socket> client in ClientsDict)
            {
                client.Value.Close();
            }
            sListener.Close();
            work = false;
        }

    }
}

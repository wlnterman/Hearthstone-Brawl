using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Effects;


namespace kurstry
{
    class Client
    {
        Socket client;
        string name;
        bool work;
        MainWindow mainwindow;
        public string tmpbtnName = "connect";
        public string prev = "connect";
        public string cur = "inconnect";
        private int reciver = 0;

        public Client(string serverIPStr,string nameenter, MainWindow mainwindow)
        {
            this.mainwindow = mainwindow;
            work = true;
            IPAddress ipServer;
            try
            {
                ipServer = IPAddress.Parse(serverIPStr);
            }
            catch (FormatException)
            {
                MessageBox.Show("Enter correct Server IP");
                string str = serverIPStr;
                ipServer = IPAddress.Parse(str);
            }

            IPEndPoint ipEndPoint = new IPEndPoint(ipServer, 8888);
            client = new Socket(ipServer.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                client.Connect(ipEndPoint);

                name = nameenter;
                byte[] msg = Encoding.UTF8.GetBytes(name);
                int bytesSent = client.Send(msg);

                MessageBox.Show("Connected. Waiting for response");

                Thread sender = new Thread(Send);
                //Thread sender = new Thread(new ThreadStart(Send));
                sender.Start();


                Thread reciever = new Thread(Recieve);
                reciever.Start();
            }
            catch (System.Net.Sockets.SocketException)
            {
                MessageBox.Show("Error : Server is not in network, Servers are too bysy");
            }

            
        }

        public void Setbtn(string s)
        {
            this.tmpbtnName = s;
        }

        public  void Recieve()
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
                //if (message.Split(' ')[0] == "Host")
                //if (message.Split(' ')[1] == ":" && message.Split(' ')[0] != mainwindow.nameenter.Text)

                if (message.Split(' ')[1] == ":" && message.Split(' ')[0] != name && reciver >= 2)
                {
                    string mes = "";
                    for (int i = 2; i < message.Split(' ').Count(); i++)       //2 для общего сообщения 4 для приватного
                    {
                        if (mes == "")
                            mes = mes + message.Split(' ')[i];
                        else
                            mes = mes + " " + message.Split(' ')[i];
                    }
                    message = mes;
                    switch (message)
                    {
                        case "start":
                            //mainwindow.StartButton.Performclick();
                            mainwindow.ButtonStart_Click(mainwindow.StartButton, new RoutedEventArgs());
                            break;
                        case "exit":
                            mainwindow.ExitButton_Click(null, null);
                            break;
                        case "endturn":
                            mainwindow.EndTurnButton_Click(null, null);
                            break;
                        case "button1":
                            mainwindow.button1_Click(null, null);
                            break;
                        case "button7":
                            mainwindow.button7_Click(null, null);
                            break;
                        case "button2":
                            mainwindow.button2_Click(null, null);
                            break;
                        case "button8":
                            mainwindow.button8_Click(null, null);
                            break;
                        case "button3":
                            mainwindow.button3_Click(null, null);
                            break;
                        case "button9":
                            mainwindow.button9_Click(null, null);
                            break;
                        case "button4":
                            mainwindow.button4_Click(null, null);
                            break;
                        case "button10":
                            mainwindow.button10_Click(null, null);
                            break;
                        case "button12":
                            mainwindow.button12_Click(null, null);
                            break;
                        case "button11":
                            mainwindow.button11_Click(null, null);
                            break;
                        case "back from choice1":
                            mainwindow.ButtonBackFromChoice_Click1(null, null);
                            break;
                        case "back from choice2":
                            mainwindow.ButtonBackFromChoice_Click2(null, null);
                            break;
                        case "back from choice3":
                            mainwindow.ButtonBackFromChoice_Click3(null, null);
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                    File.AppendAllText(name + "hystory.txt", message + Environment.NewLine);
                    Console.WriteLine(message);
                    reciver = 0;
                }

                cur = message;

                //// Running on the worker thread
                //string newText = "abc";
                //form.Label.Invoke((MethodInvoker)delegate {
                //    // Running on the UI thread
                //    form.Label.Text = newText;
                //});
                //// Back on the worker thread

                                                              //направить в функцию
                reciver++;
            }
        }

        public void Send()
        {
            string message = tmpbtnName;
            byte[] msg = Encoding.UTF8.GetBytes(message);
            client.Send(msg);
            reciver = 0;
        }

        //public void Send(string tmpbtnName)
        //{
        //    string message = tmpbtnName;
        //    byte[] msg = Encoding.UTF8.GetBytes(message);
        //    client.Send(msg);

        //    ////while (work)
        //    ////{
        //    //    //string message = Console.ReadLine();                                      //отправить после нажатия
        //    //    string message = Button.ClickEvent.Name;
        //    //        //"StartButton";

        //    //    // if (message == "<history>")
        //    //    //{
        //    //    //    foreach (string line in File.ReadLines(name + "hystory.txt"))
        //    //    //    {
        //    //    //        Console.WriteLine(line);
        //    //    //    }
        //    //    //}
        //    //    //else
        //    //    byte[] msg = Encoding.UTF8.GetBytes(message);
        //    //    //Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop - 1);
        //    //    int bytesSent = client.Send(msg);

        //    ////}
        //}

        public void ExitClient()
        {
            byte[] msg = Encoding.UTF8.GetBytes("<close>" + " " + name);
            int bytesSent = client.Send(msg);
            client.Close();
            work = false;
        }

    }
}

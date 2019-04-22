using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
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
using System.Diagnostics;
using System.Threading;


namespace kurstry
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //timerHP();

        }

        private bool starter = false;
        private int MinionBuf;
        private int NowPlayer = 1;
        public int button1TimesToDamage = 1;
        public int Button2TimesToDamage = 1;
        public int Button3TimesToDamage = 1;
        public int Button4TimesToDamage = 1;
        public int Button7TimesToDamage = 1;
        public int Button8TimesToDamage = 1;
        public int Button9TimesToDamage = 1;
        public int Button10TimesToDamage = 1;
        static Client client;

        public int Button1TimesToDamage
        {
            get
            {
                return button1TimesToDamage;
            }
            set
            {
                button1TimesToDamage = value;
                if (Button1TimesToDamage == 1 && NowPlayer == 1)
                {

                    //System.Windows.Media.Effects.BevelBitmapEffect bev = new System.Windows.Media.Effects.BevelBitmapEffect();
                    //System.Windows.Media.Effects.DropShadowEffect drop = new System.Windows.Media.Effects.DropShadowEffect();
                    //drop.ShadowDepth = 0.0;
                    //drop.Opacity = 0.9;
                    //drop.BlurRadius = 12;
                    //bev.BevelWidth = 5;
                    //bev.EdgeProfile = System.Windows.Media.Effects.EdgeProfile.BulgedUp;
                    //bev.LightAngle = 135;
                    //bev.Relief = 0.3;
                    //bev.Smoothness = 0.2;
                    //temprect.BitmapEffect = bev;

                    //button1.Effect.

                    //button1.BorderThickness = new Thickness(10);
                    button1.BorderBrush = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    //button1.BorderThickness = new Thickness(0);
                }
            }
        }

        public void ButtonChoice_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("buf choice");
            client.Send();
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                GameMechanic.ShowGrid(grHeroPowerChoice, grGameField);
            });
        }

        public void ButtonBackFromChoice_Click1(object sender, RoutedEventArgs e)
        {
            client.Setbtn("back from choice1");
            client.Send();
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                GameMechanic.HideGrid(grHeroPowerChoice, grGameField);
            });
            MinionBuf = 1;
        }

        public void ButtonBackFromChoice_Click2(object sender, RoutedEventArgs e)
        {
            client.Setbtn("back from choice2");
            client.Send();
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                GameMechanic.HideGrid(grHeroPowerChoice, grGameField);
            });
            MinionBuf = 2;
        }

        public void ButtonBackFromChoice_Click3(object sender, RoutedEventArgs e)
        {
            client.Setbtn("back from choice3");
            client.Send();
            if (NowPlayer == 1)
            {
                GameMechanic.p1 += 5;
                if (GameMechanic.p1 > 30)
                    GameMechanic.p1 = 30;
            }
            else
            {
                GameMechanic.p2 += 5;
                if (GameMechanic.p2 > 30)
                    GameMechanic.p2 = 30;
            }

            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                GameMechanic.HideGrid(grHeroPowerChoice, grGameField);
            });
            
            //line1.X1 = 410;
            //line1.X2 = 410;//Mouse.GetPosition(this);
            //line1.Y1 =300;
            //line1.Y2 =370;
        }

        public void ButtonStart_Click(object sender, RoutedEventArgs e)
        {  
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
             {
                 GameMechanic.HideGridNoBlur(grMainMenu, grGameField);
             });
            if (!starter)
            {
                timer();
                timerHP();
                starter = true;
            }
            client.Setbtn("start");
            client.Send();
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                TimerHP.Start();
                TimerMain.Stop();
                TimerMain.Interval = new TimeSpan(0, 0, 0);
                TimerMain.Start();
                TimerMain.Interval = new TimeSpan(0, 0, 61);
            });
            //TimerHP.Start();
            //TimerMain.Stop();
            //TimerMain.Interval = new TimeSpan(0, 0, 0);
            //TimerMain.Start();
            //TimerMain.Interval = new TimeSpan(0, 0, 61);
        }

        DispatcherTimer TimerMain = new System.Windows.Threading.DispatcherTimer();
        private void timer()
        { 
            TimerMain.Tick += new EventHandler(MainTick);
            TimerMain.Interval = new TimeSpan(0, 0, 61);
            TimerMain.Start();  //почему внутри этих скобок работает а за ними нет}
        }

        private void MainTick(object sender, EventArgs e)
        {
            if (NowPlayer == 1)
            {
                NowPlayer = 2;
                Turn.Content = "p2";
                Button7TimesToDamage = 1;
                Button8TimesToDamage = 1;
                Button9TimesToDamage = 1;
                Button10TimesToDamage = 1;
                Button1TimesToDamage = 1;
                Button2TimesToDamage = 1;
                Button3TimesToDamage = 1;
                Button4TimesToDamage = 1;
            }
            else
            {
                NowPlayer = 1;
                Turn.Content = "p1";
                Button7TimesToDamage = 1;
                Button8TimesToDamage = 1;
                Button9TimesToDamage = 1;
                Button10TimesToDamage = 1;
                Button1TimesToDamage = 1;
                Button2TimesToDamage = 1;
                Button3TimesToDamage = 1;
                Button4TimesToDamage = 1;
            }
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                GameMechanic.ChangeLabel(label, NowPlayer, P1Turn, P2Turn);
            });
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                GameMechanic.ShowGrid(grTurnAnimation, grGameField);
            });            
            
            animationTimer();
            //Updating the Label which displays the current second
            //  lblSeconds.Content = DateTime.Now.Second;
            //b_endturn.Click;
            //Forcing the CommandManager to raise the RequerySuggested event
            //    CommandManager.InvalidateRequerySuggested();
        }

        DispatcherTimer TimerHP = new System.Windows.Threading.DispatcherTimer();
        public void timerHP()
        {
            TimerHP.Tick += new EventHandler(HPTick);
            TimerHP.Interval = new TimeSpan(0, 0, 0);
            TimerHP.Start();  //почему внутри этих скобок работает а за ними нет}
        }
        
        private void HPTick(object sender, EventArgs e)
        {
            GameMechanic.HeroHPShow(HeroHP1, HeroHP2);
            label.Content = NowPlayer;// dispatcherTimer.Interval;
            label1.Content = GameMechanic.mas1[0, 0].ToString() + "        " + GameMechanic.mas1[1, 0].ToString();
            label2.Content = GameMechanic.mas1[0, 1].ToString() + "        " + GameMechanic.mas1[1, 1].ToString();
            label3.Content = GameMechanic.mas1[0, 2].ToString() + "        " + GameMechanic.mas1[1, 2].ToString();
            label4.Content = GameMechanic.mas1[0, 3].ToString() + "        " + GameMechanic.mas1[1, 3].ToString();
            label5.Content = GameMechanic.mas2[0, 0].ToString() + "        " + GameMechanic.mas2[1, 0].ToString();
            label6.Content = GameMechanic.mas2[0, 1].ToString() + "        " + GameMechanic.mas2[1, 1].ToString();
            label7.Content = GameMechanic.mas2[0, 2].ToString() + "        " + GameMechanic.mas2[1, 2].ToString();
            label8.Content = GameMechanic.mas2[0, 3].ToString() + "        " + GameMechanic.mas2[1, 3].ToString();
            //if (client.cur != client.prev)
            //{
            //    switch (client.cur)
            //    {
            //        case "start":

            //            //mainwindow.StartButton.Performclick();
            //            ButtonStart_Click(null,null);
            //            //ButtonStart_Click(mainwindow.StartButton, new RoutedEventArgs());
            //            break;
            //        case "exit":
            //            ExitButton_Click(null, null);
            //            //mainwindow.ExitButton_Click(null, null);
            //            break;
            //        default:
            //            Console.WriteLine("Default case");
            //            break;
            //    }
            //    client.prev = client.cur;
            //}
        }

        DispatcherTimer AnimationTimer = new System.Windows.Threading.DispatcherTimer();
        public void animationTimer()
        {
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            AnimationTimer.Interval = new TimeSpan(0, 0, 1);
            AnimationTimer.Start();  //почему внутри этих скобок работает а за ними нет}
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            GameMechanic.HideGrid(grTurnAnimation, grGameField);
            AnimationTimer.Stop();
            GameMechanic.ShowGrid(grHeroPowerChoice, grGameField);
        }

        private void MainExitButton_Click(object sender, RoutedEventArgs e)
        {
            //client.Setbtn("<close>");
            //client.Send();
            Environment.Exit(0);
        }

        public void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("finish");
            client.Send();
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                GameMechanic.HideGrid(grWinner, grGameField);
            });
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                GameMechanic.HideGridNoBlur(grGameField, grMainMenu);
            });            
            GameMechanic.p1 = 30;
            GameMechanic.p2 = 30;
            Array.Copy(GameMechanic.mas3, GameMechanic.mas1, GameMechanic.mas3.Length);
            Array.Copy(GameMechanic.mas3, GameMechanic.mas2, GameMechanic.mas3.Length);
            TimerMain.Stop();
            TimerHP.Stop();
            //TimerMain.IsEnabled = false;


        }

        public void EndTurnButton_Click(object sender, RoutedEventArgs e)

        {
            client.Setbtn("endturn");
            client.Send();
            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                TimerMain.Stop();
                TimerMain.Interval = new TimeSpan(0, 0, 0);
                TimerMain.Start();
                TimerMain.Interval = new TimeSpan(0, 0, 61);
            });
            //TimerMain.Stop();
            //TimerMain.Interval = new TimeSpan(0, 0, 0);
            //TimerMain.Start();
            //TimerMain.Interval = new TimeSpan(0, 0, 61);
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button1");
            client.Send();
            if (Button1TimesToDamage != 0)
            {
                if (NowPlayer == 1)//заменить на 1 для р1
                { 
                    GameMechanic.PressedFirstButton = 0;//заменить для других на 1 2 и 3
                    Buf();
                    MinionBuf = 0;
                }
                else
                {
                    GameMechanic.PressedSecondButton = 0;//заменить для других на 1 2 и 3
                    GameMechanic.TheMainProcedure(this, GameMechanic.PressedFirstButton, GameMechanic.PressedSecondButton, NowPlayer);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        public void button7_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button7");
            client.Send();
            if (Button7TimesToDamage != 0)
            {
                if (NowPlayer == 2)//заменить на 1 для р1
                {
                    GameMechanic.PressedFirstButton = 0;//заменить для других на 1 2 и 3
                    Buf();
                    MinionBuf = 0;
                }
                else
                {
                    GameMechanic.PressedSecondButton = 0;//заменить для других на 1 2 и 3
                    GameMechanic.TheMainProcedure(this, GameMechanic.PressedFirstButton, GameMechanic.PressedSecondButton, NowPlayer);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        public void button2_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button2");
            client.Send();
            if (Button2TimesToDamage != 0)
            {
                if (NowPlayer == 1)//заменить на 1 для р1
                { 
                    GameMechanic.PressedFirstButton = 1;//заменить для других на 1 2 и 3
                    Buf();
                    MinionBuf = 0;
                }
            else
                {
                    GameMechanic.PressedSecondButton = 1;//заменить для других на 1 2 и 3
                    GameMechanic.TheMainProcedure(this, GameMechanic.PressedFirstButton, GameMechanic.PressedSecondButton, NowPlayer);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        public void button8_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button8");
            client.Send();
            if (Button8TimesToDamage != 0)
            {
                if (NowPlayer == 2)//заменить на 1 для р1
                { 
                    GameMechanic.PressedFirstButton = 1;//заменить для других на 1 2 и 3
                    Buf();
                    MinionBuf = 0;
                }
            else
                {
                    GameMechanic.PressedSecondButton = 1;//заменить для других на 1 2 и 3
                    GameMechanic.TheMainProcedure(this, GameMechanic.PressedFirstButton, GameMechanic.PressedSecondButton, NowPlayer);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        public void button3_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button3");
            client.Send();
            if (Button3TimesToDamage != 0)
            {
                if (NowPlayer == 1)//заменить на 1 для р1
                { 
                    GameMechanic.PressedFirstButton = 2;//заменить для других на 1 2 и 3
                    Buf();
                    MinionBuf = 0;
                }
            else
                {
                    GameMechanic.PressedSecondButton = 2;//заменить для других на 1 2 и 3
                    GameMechanic.TheMainProcedure(this, GameMechanic.PressedFirstButton, GameMechanic.PressedSecondButton, NowPlayer);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        //public void nyre()
        //{
        //    button3_Click();
        //}

        public void button9_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button9");
            client.Send();
            if (Button9TimesToDamage != 0)
            {
                if (NowPlayer == 2)//заменить на 1 для р1
                { 
                    GameMechanic.PressedFirstButton = 2;//заменить для других на 1 2 и 3
                    Buf();
                    MinionBuf = 0;
                }
                else
                {
                    GameMechanic.PressedSecondButton = 2;//заменить для других на 1 2 и 3
                    GameMechanic.TheMainProcedure(this, GameMechanic.PressedFirstButton, GameMechanic.PressedSecondButton, NowPlayer);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        public void button4_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button4");
            client.Send();
            if (Button4TimesToDamage != 0)
            {
                if (NowPlayer == 1)//заменить на 1 для р1
                { 
                    GameMechanic.PressedFirstButton = 3;//заменить для других на 1 2 и 3
                    Buf();
                    MinionBuf = 0;
                }
                else
                {
                    GameMechanic.PressedSecondButton = 3;//заменить для других на 1 2 и 3
                    GameMechanic.TheMainProcedure(this, GameMechanic.PressedFirstButton, GameMechanic.PressedSecondButton, NowPlayer);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        public void button10_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button10");
            client.Send();
            if (Button10TimesToDamage != 0)
            {
                if (NowPlayer == 2)//заменить на 1 для р1
                { 
                    GameMechanic.PressedFirstButton = 3;//заменить для других на 1 2 и 3
                    Buf();
                    MinionBuf = 0;
                }
                else
                {
                    GameMechanic.PressedSecondButton = 3;//заменить для других на 1 2 и 3
                    GameMechanic.TheMainProcedure(this, GameMechanic.PressedFirstButton, GameMechanic.PressedSecondButton, NowPlayer);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        public void button12_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button12");
            client.Send();
            if (GameMechanic.PressedFirstButton != 4)
            {
                if (NowPlayer == 2)//заменить на 1 для р1
                {
                    TheMainProcedureHero1(GameMechanic.PressedFirstButton);
                    GameMechanic.PressedFirstButton = 4;
                }
            }

        }

        public void button11_Click(object sender, RoutedEventArgs e)
        {
            client.Setbtn("button11");
            client.Send();
            if (GameMechanic.PressedFirstButton != 4)
            {
                if (NowPlayer == 1)//заменить на 1 для р1
                {
                    TheMainProcedureHero2(GameMechanic.PressedFirstButton);
                    GameMechanic.PressedFirstButton = 4;
                }
            }
        }

        public void Buf()
        {
            if (NowPlayer == 1)
            {
                if (MinionBuf == 1)
                {
                    switch (GameMechanic.PressedFirstButton)
                    {
                        case 0:
                            GameMechanic.mas1[0, 0] += 3;
                            break;

                        case 1:
                            GameMechanic.mas1[0, 1] += 3;
                            break;

                        case 2:
                            GameMechanic.mas1[0, 2] += 3;
                            break;

                        case 3:
                            GameMechanic.mas1[0, 3] += 3;
                            break;

                        default:
                            //GameMechanic.PressedFirstButton = 4;
                            break;
                    }
                }

                if (MinionBuf == 2)
                {
                    switch (GameMechanic.PressedFirstButton)
                    {
                        case 0:
                            GameMechanic.mas1[1, 0] += 3;
                            break;

                        case 1:
                            GameMechanic.mas1[1, 1] += 3;
                            break;

                        case 2:
                            GameMechanic.mas1[1, 2] += 3;
                            break;

                        case 3:
                            GameMechanic.mas1[1, 3] += 3;
                            break;

                        default:
                            //GameMechanic.PressedFirstButton = 4;
                            break;
                    }
                }
            }
            else
                if (NowPlayer == 2)
                {
                    if (MinionBuf == 1)
                    {
                        switch (GameMechanic.PressedFirstButton)
                        {
                            case 0:
                                GameMechanic.mas2[0, 0] += 3;
                                break;

                            case 1:
                                GameMechanic.mas2[0, 1] += 3;
                                break;

                            case 2:
                                GameMechanic.mas2[0, 2] += 3;
                                break;

                            case 3:
                                GameMechanic.mas2[0, 3] += 3;
                                break;

                            default:
                                //GameMechanic.PressedFirstButton = 4;
                                break;
                        }
                    }

                    if (MinionBuf == 2)
                    {
                        switch (GameMechanic.PressedFirstButton)
                        {
                            case 0:
                                GameMechanic.mas2[1, 0] += 3;
                                break;

                            case 1:
                                GameMechanic.mas2[1, 1] += 3;
                                break;

                            case 2:
                                GameMechanic.mas2[1, 2] += 3;
                                break;

                            case 3:
                                GameMechanic.mas2[1, 3] += 3;
                                break;

                            default:
                               // GameMechanic.PressedFirstButton = 4;
                                break;
                        }
                    }
                }
            MinionBuf = 0;
            
        }

        private void TheMainProcedureHero1(int PressedFirstButton)//процедура удар по герою 1
        {
            //обработка хп героя
            if (0 >= GameMechanic.p1 - GameMechanic.mas2[0, GameMechanic.PressedFirstButton])
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    Congratulations.Content = "player 2 won";
                });
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    GameMechanic.ShowGrid(grWinner, grGameField);               //Поражение 1
                });  
            }
            else
                GameMechanic.p1 -= GameMechanic.mas2[0, PressedFirstButton];//хп героя после нападения

            GameMechanic.OnePunchMode(this, GameMechanic.PressedFirstButton, NowPlayer);         
    }

        private void TheMainProcedureHero2(int PressedFirstButton)//процедура удар по герою 2
        {
            //обработка хп героя
            if (0 >= GameMechanic.p2 - GameMechanic.mas1[0, PressedFirstButton])
            {
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    Congratulations.Content = "player 1 won";
                });
                Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
                {
                    GameMechanic.ShowGrid(grWinner, grGameField);               //Поражение 2
                });
                
            }
            else
                GameMechanic.p2 -= GameMechanic.mas1[0, PressedFirstButton];//хп героя после нападения

            GameMechanic.OnePunchMode(this, GameMechanic.PressedFirstButton, NowPlayer);
        }

        private void NameIpSendButton_Click(object sender, RoutedEventArgs e)
        {
            client = new Client(ipenter.Text, nameenter.Text, this);
            //butt
        }

        //private void Buttonsend(string tmpbtnName)
        //{
        //    if (client != null)
        //    {

        //        string message = tmpbtnName;
        //        byte[] msg = Encoding.UTF8.GetBytes(message);
        //        client.Send(msg);
        //    }
        //    else
        //        throw new Exception("client = null");
        //}
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    class GameMechanic
    {
        public static int p1 = 30;
        public static int p2 = 30;
        public static int[,] mas1 = new int[2, 5] { { 2, 2, 2, 2, 0 }, { 4, 4, 4, 4, 0 } };
        public static int[,] mas2 = new int[2, 5] { { 29, 2, 2, 2, 0 }, { 4, 4, 4, 4, 0 } };   //debug version
        public static int[,] mas3 = new int[2, 5] { { 29, 2, 2, 2, 0 }, { 4, 4, 4, 4, 0 } };   //debug version
        public static int PressedFirstButton = 4;
        public static int PressedSecondButton = 4;

        public static void ShowGrid(Grid grNew, Grid grOld)
        {
            grNew.Visibility = Visibility.Visible;

            BlurEffect blEf = new BlurEffect();
            blEf.Radius = 15;
            grOld.Effect = blEf;
        }

        public static void HideGrid(Grid grNew, Grid grOld)
        {
            grNew.Visibility = Visibility.Hidden;

            grOld.Effect = null;
        }

        public static void HideGridNoBlur(Grid grNew, Grid grOld)
        {
            grNew.Visibility = Visibility.Hidden;
            grOld.Visibility = Visibility.Visible;  
        }

        //your turn
        public static void ChangeLabel(Label label, int i, Rectangle P1Turn, Rectangle P2Turn)
        {
            if (i == 1)
            {
                label.Content = "p1";
                P2Turn.Fill = Brushes.Red;
                P1Turn.Fill = Brushes.Lime;
            }
            else
            {
                label.Content = "p2";
                P2Turn.Fill = Brushes.Lime;
                P1Turn.Fill = Brushes.Red;
                //P1Turn.Fill = Brushes.Red; 
                //SolidColorBrush(color.red);
            }
        }

        public static void HeroHPShow(Label label1, Label label2)
        {
            label1.Content = p1;
            label2.Content = p2;
        }

        //универсальная процедура общёта хп фигура на фигуру
        public static void TheMainProcedure(MainWindow window, int PressedFirstButton, int PressedSecondButton, int NowPlayer)//обсчёт процедуры кнопка на кнопку
        {
            int tmp1, tmp2, tmp3, tmp4, tmp5, tmp6, tmp7, tmp8;
            tmp1= mas1[0, PressedSecondButton];
            tmp2= mas1[1, PressedSecondButton];
            tmp3= mas2[0, PressedFirstButton];
            tmp4= mas2[1, PressedFirstButton];

            tmp5 = mas2[0, PressedSecondButton];
            tmp6 = mas2[1, PressedSecondButton];
            tmp7 = mas1[0, PressedFirstButton];
            tmp8 = mas1[1, PressedFirstButton];

            if (PressedFirstButton != 4)
            {
                if (NowPlayer == 2)
                {
                    //обработка хп жертвы
                    if (0 >= tmp2 - tmp3)
                    {
                        mas1[0, PressedSecondButton] = 2;//восстановление после уничтожения фигуры жертвы
                        mas1[1, PressedSecondButton] = 4;
                    }
                    else
                        mas1[1, PressedSecondButton] = tmp2-tmp3;//хп жертвы после нападения

                    //обработка хп атакующего
                    if (0 >= tmp4 - tmp1)
                    {
                        mas2[0, PressedFirstButton] = 2;//восстановление после уничтожения фигуры атакующего
                        mas2[1, PressedFirstButton] = 4;
                    }
                    else
                        mas2[1, PressedFirstButton] = tmp4 - tmp1;//хп атакующего после нападения

                    //карта ходит только 1 раз
                    switch (PressedFirstButton)
                    {
                        case 0:
                            window.Button7TimesToDamage = 0;
                            break;

                        case 1:
                            window.Button8TimesToDamage = 0;
                            break;

                        case 2:
                            window.Button9TimesToDamage = 0;
                            break;

                        case 3:
                            window.Button10TimesToDamage = 0;
                            break;

                        default:
                            PressedFirstButton = 4;
                            break;
                    }
                }
                else    //Ход 1рвого игрока
                {
                    //обработка хп жертвы
                    if (0 >= tmp6 - tmp7)
                    {
                        mas2[0, PressedSecondButton] = 2;//восстановление после уничтожения фигуры жертвы
                        mas2[1, PressedSecondButton] = 4;
                    }
                    else
                        mas2[1, PressedSecondButton] = tmp6 - tmp7;//хп жертвы после нападения

                    //обработка хп атакующего
                    if (0 >= tmp8 - tmp5)
                    {
                        mas1[0, PressedFirstButton] = 2;//восстановление после уничтожения фигуры атакующего
                        mas1[1, PressedFirstButton] = 4;
                    }
                    else
                        mas1[1, PressedFirstButton] = tmp8 - tmp5;//хп атакующего после нападения

                    //карта ходит только 1 раз
                    switch (PressedFirstButton)
                    {
                        case 0:
                            window.Button1TimesToDamage = 0;
                            break;

                        case 1:
                            window.Button2TimesToDamage = 0;
                            break;

                        case 2:
                            window.Button3TimesToDamage = 0;
                            break;

                        case 3:
                            window.Button4TimesToDamage = 0;
                            break;

                        default:
                            PressedFirstButton = 4;
                            break;
                    }
                }
            }
        }

        // ходить 1 существом 1 раз (для атаки по героям)
        public static void OnePunchMode(MainWindow window, int PressedFirstButton, int NowPlayer)
        {
            if (NowPlayer == 1)
                switch (PressedFirstButton)
                {
                    case 0:
                        window.Button1TimesToDamage = 0;
                        break;

                    case 1:
                        window.Button2TimesToDamage = 0;
                        break;

                    case 2:
                        window.Button3TimesToDamage = 0;
                        break;

                    case 3:
                        window.Button4TimesToDamage = 0;
                        break;

                    default:
                        PressedFirstButton = 4;
                        break;
                }
            else
                switch (PressedFirstButton)
                {
                    case 0:
                        window.Button7TimesToDamage = 0;
                        break;

                    case 1:
                        window.Button8TimesToDamage = 0;
                        break;

                    case 2:
                        window.Button9TimesToDamage = 0;
                        break;

                    case 3:
                        window.Button10TimesToDamage = 0;
                        break;

                    default:
                        PressedFirstButton = 4;
                        break;
                }
        }
    }
}

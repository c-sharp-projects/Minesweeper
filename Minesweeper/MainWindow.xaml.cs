using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<int> LObj;
        int Score;
        public MainWindow()
        {
            InitializeComponent();
            LObj = new List<int>();
            SetIcons();
            GenerateBomb();
            Score = 0;
        }

      
        public void SetIcons()
        {
            Icons IObj = new Icons();
            IObj.iconame = AppDomain.CurrentDomain.BaseDirectory + @"\Game\Icons\button.ico";
            Game_Grid.DataContext = IObj;

        }

        public void GenerateBomb()
        {
            Random rnd = new Random();
            for (int i = 0; i < 65; i++)
            {
                LObj.Add(rnd.Next(99));
            }
        }

        ImageBrush ChkPoint(int Points)
        {
            string name = string.Empty;

            switch(Points)
            {
                case 1:
                    name = Environment.CurrentDirectory + @"\Game\Icons\one.png";
                    break;
                case 2:
                    name = Environment.CurrentDirectory + @"\Game\Icons\two.png";
                    break;
                case 3:
                    name = Environment.CurrentDirectory + @"\Game\Icons\three.png";
                    break;
                case 4:
                    name = Environment.CurrentDirectory + @"\Game\Icons\four.png";
                    break;
                case 5:
                    name = Environment.CurrentDirectory + @"\Game\Icons\five.png";
                    break;
            }
            return new ImageBrush(new BitmapImage(new Uri(name)));
        }

        public void fun()
        {
            Random rnd = new Random();
            try
            {
                foreach (Grid ctrlGrid in Game_Grid.Children)
                {
                    foreach (Control ctrl in ctrlGrid.Children)
                    {
                        if (ctrl.GetType() == typeof(Button))
                        {
                            if (LObj.Contains(Convert.ToInt32(ctrl.Uid)))
                            {
                                ctrl.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\Game\Icons\bomb.png")));
                            }
                            else
                            {
                                
                                int Points = rnd.Next(1, 5);
                                ctrl.Background = ChkPoint(Points);
                            }

                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void ActionListener(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            
            if (LObj.Contains(Convert.ToInt32(btn.Uid)))
            {
                btn.Background = new ImageBrush(new BitmapImage(new Uri(Environment.CurrentDirectory + @"\Game\Icons\bomb.png")));
                MessageBox.Show("You Score is : "+ Score);
                fun();
            }
            else
            {
                Random rnd = new Random();
                int Points = rnd.Next(1,5);
                btn.Background = ChkPoint(Points);
                Score += Points;
                
            }

        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            LObj.Clear();
            GenerateBomb();
            Score = 0;

            try
            {
                foreach (Grid ctrlGrid in Game_Grid.Children)
                {
                    foreach (Control ctrl in ctrlGrid.Children)
                    {
                        if (ctrl.GetType() == typeof(Button))
                        {
                            ctrl.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + @"\Game\Icons\button.ico")));
                        }
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }

    class Icons
    {
        public string iconame;
        public BitmapImage ButtonImg
        {
            get
            {
                return getImage(iconame);
            }
        }

        private BitmapImage getImage(string str)
        {
            return new BitmapImage(new Uri(str));
        }
    }
}

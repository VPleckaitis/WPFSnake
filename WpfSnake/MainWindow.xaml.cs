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
using WpfSnake.Models;

namespace WpfSnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Game theGame;
        private Snake theSnake;
        private Map theMap;
        private bool isInMenu = true;
        private bool gameIsRunning = false;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            bool success = true;
            int rows = 10;
            int columns = 10;
            try { rows = Convert.ToInt32(tbHeight.Text); }
            catch { success = false; }

            try { columns = Convert.ToInt32(tbWidth.Text); }
            catch { success = false; }

            if (success)
            {
                theMap = new Map(rows, columns);
                theSnake = new Snake(new Cell((int)rows / 2, (int)columns / 2)); // We start at middle of map
                theGame = new Game(theSnake, theMap);

                grdOptions.Visibility = Visibility.Collapsed; // hide options

                theGameGrid.Height = rows * 10; // set size of canvas. Each row is 10 px of size
                theGameGrid.Width = columns * 10; // set size of canvas. Each column is 10 px of size

                theViewBox.Visibility = Visibility.Visible;

                isInMenu = false; // Just so our clicks do smth
                // initial draw snake head

                Rectangle rect = new Rectangle() { Width = 10, Height = 10 };
                rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                theGameGrid.Children.Add(rect);
                Canvas.SetTop(rect, (rows / 2) * 10); 
                Canvas.SetLeft(rect, (columns / 2) * 10);

            }
            else
            {
                MessageBox.Show("Invalid number specified !", "WPFSnake", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void DrawFood(Cell cell)
        {

            BitmapImage bmi = null;
            Random rnd = new Random();
            switch(rnd.Next(3))
            {
                case 0:
                    bmi = new BitmapImage(new Uri("pack://application:,,,/Images/food2.png"));
                    break;
                case 1:
                    bmi = new BitmapImage(new Uri("pack://application:,,,/Images/food3.png"));
                    break;
                default:
                    bmi = new BitmapImage(new Uri("pack://application:,,,/Images/food.png"));
                    break;
            }

            Image img = new Image();
            img.Width = 10;
            img.Height = 10;
            img.Source = bmi;

            img.Tag = "Food"; // Just so we know what it is

            theGameGrid.Children.Add(img);
            Canvas.SetTop(img, cell.Row * 10);
            Canvas.SetLeft(img, cell.Column * 10);
        }

        public void DigestFood()
        {
            foreach (var child in theGameGrid.Children)
            {
                if (child is Image)
                {
                    if(((Image)child).Tag=="Food")
                    {
                        ((Image)child).Source = new BitmapImage(new Uri("pack://application:,,,/Images/digested_food.png"));
                        ((Image)child).Tag = "DigestedFood";
                    }
                }
            }
        }

        public void DrawSnake()
        {
            // 2DO
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!isInMenu)
            {
                if (e.Key == Key.Up) theGame.Direction = Game.MovementDirection.Up;
                else if (e.Key == Key.Down) theGame.Direction = Game.MovementDirection.Down;
                else if (e.Key == Key.Left) theGame.Direction = Game.MovementDirection.Left;
                else if (e.Key == Key.Right) theGame.Direction = Game.MovementDirection.Right;
            }
        }
    }
}

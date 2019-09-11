using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private static int score = 0;
        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged;
        private static void NotifyStaticPropertyChanged(string propertyName)
        {
            if (StaticPropertyChanged != null)
                StaticPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }

        public static int Score
        {
            get { return score; }
            set
            {
                score = value; NotifyStaticPropertyChanged("Score");
            }
        }

        DispatcherTimer timer;
        private int timerInterval = 500;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, timerInterval);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            GameUpdateAction();
        }

        private void BtnConfirm_Click(object sender, RoutedEventArgs e)
        {
            bool success = true;
            int rows, columns; // initialise rows and columns
            try { rows = Convert.ToInt32(tbHeight.Text); }
            catch { success = false; rows = -1; }

            try { columns = Convert.ToInt32(tbWidth.Text); }
            catch { success = false; columns = -1; }

            if (success)
            {
                theMap = new Map(rows, columns);


                theSnake = new Snake(new Cell((int)rows / 2, (int)columns / 2)); // We start at middle of map
                theGame = new Game(theSnake, theMap);
                theGame.mapRows = rows;
                theGame.mapColumns = columns;

                grdOptions.Visibility = Visibility.Collapsed; // hide options

                theGameGrid.Height = rows * 10; // set size of canvas. Each row is 10 px of size
                theGameGrid.Width = columns * 10; // set size of canvas. Each column is 10 px of size

                theViewBox.Visibility = Visibility.Visible;

                isInMenu = false; // Just so our clicks do smth
                tbTheScore.Visibility = Visibility.Visible;

                DrawSnake(); // initial draw snake head
                DrawFood(theGame.FoodCell);

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
            switch (rnd.Next(3))
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
                    if (((Image)child).Tag == "Food")
                    {
                        ((Image)child).Source = new BitmapImage(new Uri("pack://application:,,,/Images/digested_food.png"));
                        ((Image)child).Tag = "DigestedFood";
                    }
                }
            }
        }

        public void DrawSnake()
        {
            List<Rectangle> toRemove = new List<Rectangle>();
            foreach (var child in theGameGrid.Children)
            {
                if (child is Rectangle)
                {
                    toRemove.Add((Rectangle)child);
                }
            }
            foreach (Rectangle rect in toRemove)
            {
                theGameGrid.Children.Remove(rect);
            }


            foreach (Cell cell in theSnake.SnakeBody)
            {
                Rectangle rect = new Rectangle() { Width = 10, Height = 10 };
                if (cell.Column == theSnake.SnakeHead.Column && cell.Row == theSnake.SnakeHead.Row)
                {
                    rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                }
                else
                {
                    rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.Yellow);

                }
                rect.Tag = "Snake";
                theGameGrid.Children.Add(rect);
                Canvas.SetTop(rect, cell.Row * 10);
                Canvas.SetLeft(rect, cell.Column * 10);
            }

            this.UpdateLayout();

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!isInMenu)
            {
                bool arrow_pressed = false;
                if (e.Key == Key.Up) { theGame.Direction = Game.MovementDirection.Up; arrow_pressed = true; }
                else if (e.Key == Key.Down) { theGame.Direction = Game.MovementDirection.Down; arrow_pressed = true; }
                else if (e.Key == Key.Left) { theGame.Direction = Game.MovementDirection.Left; arrow_pressed = true; }
                else if (e.Key == Key.Right) { theGame.Direction = Game.MovementDirection.Right; arrow_pressed = true; }
                if (!gameIsRunning) { gameIsRunning = true; timer.Start(); }

                if (arrow_pressed)
                {
                    GameUpdateAction();
                }
            }
        }

        private void GameUpdateAction()
        {
            theGame.Update();
            if (!theGame.GameOver)
            {
                if (theGame.MapHasChanged)
                {
                    Score++;
                    DigestFood();
                    DrawFood(theGame.FoodCell);
                    theGame.MapHasChanged = false;
                }
                DrawSnake();
                timer.Interval = new TimeSpan(0, 0, 0, 0, timerInterval - score);
            }
            else
            {
                timer.Stop();
                MessageBox.Show("Game over !", "WPFSnake", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
    }
}

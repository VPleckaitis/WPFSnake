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

                grdOptions.Visibility = Visibility.Collapsed;
                theGameGrid.Height = rows * 10;
                theGameGrid.Width = columns * 10;

                theViewBox.Visibility = Visibility.Visible;
                Rectangle rect = new Rectangle() { Width = 10, Height = 10 } ;
                rect.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                theGameGrid.Children.Add(rect);
                Canvas.SetTop(rect, (rows / 2) * 10);
                Canvas.SetLeft(rect, (columns / 2) * 10);
              //  { Width=10,Height=10,Canvas.SetTop(this,(rows/2)*10), Canvas.SetLeft(this, (columns/2)*10)})
            }
            else
            {
                MessageBox.Show("Invalid number specified !","WPFSnake",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

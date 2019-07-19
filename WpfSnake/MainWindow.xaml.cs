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

namespace WpfSnake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            }
            else
            {
                MessageBox.Show("Invalid number specified !","WPFSnake",MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

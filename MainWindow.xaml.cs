using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Sorter
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void StartSort(object sender, RoutedEventArgs e)
        {
            int number = 0;
            int delay = 0;
            SortingType type;

            try
            {
                number = Convert.ToInt32(numBoxItems.Text);
                delay = Convert.ToInt32(numBoxDelay.Text);

                switch (sortingType.SelectedIndex) 
                {
                    case 0:
                        type = SortingType.Selection;
                        break;
                    case 1:
                        type = SortingType.Insertion;
                        break;
                    default:
                        type = SortingType.Selection;
                        break;
                }


                SortingWindow sortingWindow = new SortingWindow(number, delay, type);
                sortingWindow.Show();
            } 
            catch
            {
                MessageBox.Show("Please Enter A Number");
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sorter
{
    public enum SortingType 
    { 
        Selection,
        Insertion
    }

    public partial class SortingWindow : Window
    {
        private int numItems;
        private int delay;
        private SortingType type;
        private List<Item> items = new List<Item>();

        public SortingWindow()
        {
            InitializeComponent();
        }

        public SortingWindow(int _numItems, int _delay, SortingType _type)
        {
            numItems = _numItems;
            delay = _delay;
            type = _type;

            InitializeComponent();

            createItems();
        }

        public void createItems()
        {
            int padding = 2;
            double width = (this.Width - (numItems * padding) - 18) / (double)numItems;
            double height = 500 / (double)numItems
;
            for (int i = 0; i < numItems; i++)
            {
                items.Add(new Item(width, height, i, canvas));
            }
        }

        private void Shuffle(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            List<Item> shuffled = new List<Item>();

            for (int i = 0; i < numItems; i++)
            {
                shuffled.Add(items[i]);
            }

            for (int i = 0; i < items.Count; i++)
            {
                int index = (int)(random.NextDouble() * shuffled.Count);
                items[i] = shuffled[index];
                shuffled.RemoveAt(index);
                items[i].updatePosition(i);
            }
        }

        private void StartSort(object sender, RoutedEventArgs e)
        {
            switch (type)
            {
                case SortingType.Selection:
                    new Thread(SelectionSort).Start();
                    break;
                case SortingType.Insertion:
                    new Thread(InsertionSort).Start();
                    break;
                default:
                    new Thread(SelectionSort).Start();
                    break;
            }
        }

        private void SelectionSort()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            Item temp; 
            int smallest;

            for (int i = 0; i < numItems; i++)
            {
                smallest = i;
                for (int j = i + 1; j < numItems; j++)
                {
                    if (items[j].getOrder() < items[smallest].getOrder())
                    {
                        smallest = j;
                    }
                    items[j].setColor(Brushes.Blue);
                    Thread.Sleep(delay);
                    items[j].setColor(Brushes.LightBlue);
                }
                temp = items[smallest];
                items[smallest] = items[i];
                items[i] = temp;

                items[smallest].updatePosition(smallest);
                items[i].updatePosition(i);
            }

            timer.Stop();

            time.Dispatcher.Invoke(new Action(() =>
            {
                time.Text = timer.ElapsedMilliseconds.ToString() + "ms";
            }));
        }

        private void InsertionSort()
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();

            int n = items.Count;

            for (int i = 1; i < n; ++i)
            {
                Item key = items[i];
                int j = i - 1;

                while (j >= 0 && items[j].getOrder() > key.getOrder())
                {
                    items[j + 1] = items[j];
                    items[j + 1].updatePosition(j + 1);

                    items[j + 1].setColor(Brushes.Blue);
                    Thread.Sleep(delay);
                    items[j + 1].setColor(Brushes.LightBlue);

                    j = j - 1;
                }
                items[j + 1] = key;
                items[j + 1].updatePosition(j + 1);
            }

            timer.Stop();

            time.Dispatcher.Invoke(new Action (() => 
            {
                time.Text = timer.ElapsedMilliseconds.ToString() + "ms";
            }));
        }
    }
}

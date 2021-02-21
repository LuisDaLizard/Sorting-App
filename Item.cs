using System;
using System.Collections.Generic;
using System.Text;
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
    class Item
    {
        private double width;
        private double height;
        private int order;
        private Canvas canvas;
        private Rectangle item = new Rectangle();

        public Item(double _width, double _height, int _order, Canvas _canvas)
        {
            width = _width;
            height = _height;
            order = _order;
            canvas = _canvas;

            initialize();
        }

        private void initialize()
        {
            item.Fill = Brushes.LightBlue;
            item.Width = width;
            item.Height = order * height + height;
            Canvas.SetLeft(item, order * width + 2 * (order + 1));

            canvas.Children.Add(item);
        }

        public int getOrder()
        {
            return order;
        }
        public void setColor(Brush color)
        {
            item.Dispatcher.Invoke(new Action(() =>
            {
                item.Fill = color;
            }));
        }
        public void updatePosition(int i)
        {
            item.Dispatcher.Invoke(new Action(() =>
            {
                Canvas.SetLeft(item, i * width + 2 * (i + 1));
            }));
        }
    }
}

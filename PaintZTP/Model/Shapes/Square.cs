using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Media.TextFormatting;

namespace PaintZTP.Model.Shapes
{

    public class Square : Shape
    {

        public Square(Point center)
        {
            this.center = center;
            this.size = 50;
        }

        public override Shape Clone()
        {

            Square SquareCloned = new Square(this.center);
            return SquareCloned;
        }

        public override WriteableBitmap Draw(WriteableBitmap bitmap)
        {
            Trace.WriteLine("Square Draw");
            Point start = new Point(center.X - (int)(size / 2), center.Y - (int)(size / 2));

            // Góra i dół
            for (int x = 0; x < size; x++)
            {
                bitmap = WBA.DrawDot(bitmap, (int)start.X + x, (int)start.Y, System.Windows.Media.Color.FromRgb(255, 0, 0));
                bitmap = WBA.DrawDot(bitmap, (int)start.X + x, (int)start.Y + size - 1, System.Windows.Media.Color.FromRgb(255, 0, 0));
            }

            // Lewy i prawy
            for (int y = 1; y < size - 1; y++)
            {
                bitmap = WBA.DrawDot(bitmap, (int)start.X, (int)start.Y + y, System.Windows.Media.Color.FromRgb(255, 0, 0));
                bitmap = WBA.DrawDot(bitmap, (int)start.X + size - 1, (int)start.Y + y, System.Windows.Media.Color.FromRgb(255, 0, 0));
            }

            return bitmap;
        }

        public override Point Move(Point newCenter)
        {
            Trace.WriteLine("Square Move");
            var tmp = this.center;
            this.center = newCenter;
            return tmp;
        }

    }

}

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

    public class Triangle : Shape
    {

        public Triangle(Point center)
        {
            this.center = center;
            this.size = 50;
        }

        public override Shape Clone()
        {

            Triangle TriangleCloned = new Triangle(this.center);
            return TriangleCloned;
        }

        public override WriteableBitmap Draw(WriteableBitmap bitmap)
        {
            Trace.WriteLine("Triangle Draw");
            Point start = new Point(center.X - (int)(size / 2), center.Y);

            // podstawa
            for (int x = 0; x < size; x++)
            {
                bitmap = WBA.DrawDot(bitmap, (int)start.X + x, (int)start.Y, System.Windows.Media.Color.FromRgb(255, 0, 0));
            }

            int height = (int)(size * Math.Sqrt(3) / 2); //wysokosc
            for (int y = 1; y <= height; y++)
            {
                //lewy
                int xLeft = (int)(start.X + y / Math.Sqrt(3));
                bitmap = WBA.DrawDot(bitmap, xLeft, (int)start.Y - y, System.Windows.Media.Color.FromRgb(255, 0, 0));

                // prawy
                int xRight = (int)(start.X + size - y / Math.Sqrt(3));
                bitmap = WBA.DrawDot(bitmap, xRight, (int)start.Y - y, System.Windows.Media.Color.FromRgb(255, 0, 0));
            }

            return bitmap;
        }

        public override Point Move(Point newCenter)
        {
            Trace.WriteLine("Triangle Move");
            var tmp = this.center;
            this.center = newCenter;
            return tmp;
        }

    }

}

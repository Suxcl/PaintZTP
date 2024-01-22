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

    public class Circle : Shape
    {

        public Circle(Point center)
        {
            this.center = center;
            this.size = 50;
        }

        public override Shape Clone()
        {

            Circle CircleCloned = new Circle(this.center);
            return CircleCloned;
        }

        public override WriteableBitmap Draw(WriteableBitmap bitmap)
        {
            Trace.WriteLine("Circle Draw");
            int radius = size / 2;
            int xCenter = (int)center.X;
            int yCenter = (int)center.Y;

            int x = radius;
            int y = 0;
            int radiusError = 1 - x;

            // Drawing the initial points
            DrawCirclePoints(bitmap, xCenter, yCenter, x, y);

            while (x >= y)
            {
                y++;

                if (radiusError < 0)
                {
                    radiusError += 2 * y + 1;
                }
                else
                {
                    x--;
                    radiusError += 2 * (y - x) + 1;
                }

                DrawCirclePoints(bitmap, xCenter, yCenter, x, y);
            }

            return bitmap;
        }
        private void DrawCirclePoints(WriteableBitmap bitmap, int xCenter, int yCenter, int x, int y)
        {
            bitmap = WBA.DrawDot(bitmap, xCenter + x, yCenter + y, System.Windows.Media.Color.FromRgb(255, 0, 0));
            bitmap = WBA.DrawDot(bitmap, xCenter - x, yCenter + y, System.Windows.Media.Color.FromRgb(255, 0, 0));
            bitmap = WBA.DrawDot(bitmap, xCenter + x, yCenter - y, System.Windows.Media.Color.FromRgb(255, 0, 0));
            bitmap = WBA.DrawDot(bitmap, xCenter - x, yCenter - y, System.Windows.Media.Color.FromRgb(255, 0, 0));
            bitmap = WBA.DrawDot(bitmap, xCenter + y, yCenter + x, System.Windows.Media.Color.FromRgb(255, 0, 0));
            bitmap = WBA.DrawDot(bitmap, xCenter - y, yCenter + x, System.Windows.Media.Color.FromRgb(255, 0, 0));
            bitmap = WBA.DrawDot(bitmap, xCenter + y, yCenter - x, System.Windows.Media.Color.FromRgb(255, 0, 0));
            bitmap = WBA.DrawDot(bitmap, xCenter - y, yCenter - x, System.Windows.Media.Color.FromRgb(255, 0, 0));
        }
        public override Point Move(Point newCenter)
        {
            Trace.WriteLine("Circle Move");
            var tmp = this.center;
            this.center = newCenter;
            return tmp;
        }

    }

}

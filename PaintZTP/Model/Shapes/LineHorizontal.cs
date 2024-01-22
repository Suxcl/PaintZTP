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
    
    public class LineHorizontal : Shape
    {

        public LineHorizontal(Point center)
        {
            this.center = center;
            this.size = 50;
        }

        public override Shape Clone()
        {
            
            LineHorizontal lineCloned = new LineHorizontal(this.center);
            return lineCloned;
        }
        
        public override WriteableBitmap Draw(WriteableBitmap bitmap)
        {
            Trace.WriteLine("LineHirozontal draw");
            Point start = new Point(center.X - (int)(size / 2), center.Y);
            for (int i = 0; i < +size; i++)
            {
                //Trace.WriteLine((int)start.X + i+ " <-x y-> "+ (int)start.Y);
                bitmap = WBA.DrawDot(bitmap, (int)start.X + i, (int)start.Y, this.color);
            }
            return bitmap;
        }
       
        public override Point Move(Point newCenter)
        {
            Trace.WriteLine("LineHirozontal Move");
            var tmp = this.center;
            this.center = newCenter;
            return tmp;
        }

    }

}

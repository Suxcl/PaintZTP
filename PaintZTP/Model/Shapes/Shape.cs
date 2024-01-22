using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using PaintZTP.Model;
using System.Drawing;
using System.Diagnostics;
using PaintZTP.Model.Adapter;

namespace PaintZTP.Model.Shapes
{
    public abstract class Shape
    {
        public Color color { get; set; }
        private Paint paint;
        public System.Windows.Point center { get; set; }
        public int size { get; set; }

        public WriteableBitmapAdapter WBA;

        protected Shape()
        {
            paint = Paint.GetInstance();
            color = new Color(0, 0, 0);
            WBA = new WriteableBitmapAdapter();

        }
        public abstract Shape Clone();
        public abstract WriteableBitmap Draw(WriteableBitmap bitmap);

        public abstract System.Windows.Point Move(System.Windows.Point newCenter);

    }
}

using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using PaintZTP.Model;

using System.Diagnostics;
using PaintZTP.Model.Adapter;
using MColor = System.Windows.Media.Color;

namespace PaintZTP.Model.Shapes
{
    public abstract class Shape
    {
        public MColor color { get; set; }
        private Paint paint;
        public System.Windows.Point center { get; set; }
        public int size { get; set; }

        public WriteableBitmapAdapter WBA;

        protected Shape()
        {
            paint = Paint.GetInstance();
            color = MColor.FromRgb(0, 0, 0);
            WBA = new WriteableBitmapAdapter();

        }
        public abstract Shape Clone();
        public abstract WriteableBitmap Draw(WriteableBitmap bitmap);
        public abstract System.Windows.Point Move(System.Windows.Point newCenter);
        public void SetSize(int Size)
        {
            this.size = Size;
        }
        public void SetColor(MColor color)
        {
            this.color = color;
        }

    }
}

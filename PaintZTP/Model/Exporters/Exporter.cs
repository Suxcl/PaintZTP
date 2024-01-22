using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PaintZTP.Model.Exporters
{
    public abstract class Exporter
    {
        public void Export(WriteableBitmap Bitmap, string FilePath)
        {
            BitmapEncoder encoder = CreateBitmapEncoder();
        }
        protected abstract BitmapEncoder CreateBitmapEncoder();
        protected abstract string GetFormat();
    }
}

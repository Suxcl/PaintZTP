using System;
using System.Collections.Generic;
using System.IO;
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
            BitmapFrame bitmapFrame = BitmapFrame.Create(Bitmap);
            encoder.Frames.Add(bitmapFrame);
            using (FileStream stream = new FileStream(FilePath, FileMode.Create))
            {
                encoder.Save(stream);
            }
        }
        protected abstract BitmapEncoder CreateBitmapEncoder();
        protected abstract string GetFormat();
     

    }
}

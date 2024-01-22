using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PaintZTP.Model.Exporters
{
    public class JPGExporter : Exporter
    {
        protected override BitmapEncoder CreateBitmapEncoder()
        {
            return new JpegBitmapEncoder();
        }

        protected override string GetFormat()
        {
            return "JPEG";
        }
    }
}

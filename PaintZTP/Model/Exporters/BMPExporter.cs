using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PaintZTP.Model.Exporters
{
    public class BmpExporter : Exporter
    {
        protected override BitmapEncoder CreateBitmapEncoder()
        {
            return new BmpBitmapEncoder();
        }

        protected override string GetFormat()
        {
            return "BMP";
        }

        
    }
}

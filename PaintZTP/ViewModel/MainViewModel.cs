using PaintZtp;
using PaintZTP.Model;
using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Windows;
using WPoint = System.Windows.Point;
using MColor = System.Windows.Media.Color;
using System.Windows.Controls;
using System.Windows.Input;

using System.Windows.Media.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PaintZTP.Model.Shapes;

using System.Windows.Media;
using System.Diagnostics;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using PaintZTP.Model.Exporters;

namespace PaintZTP.ViewModel
{
    class MainViewModel : INPCAdapter
    {
        private Paint paint;
        public WriteableBitmap Bitmap { get; private set; }
       
        public RelayCommand Image_MouseLeftButtonDown { get; internal set; }
        public IEnumerable ListBoxShapes { get; internal set; }
        public int SelectedShapeId { get; set; }
        public string SelectedShapeSize { get; set; }
        public string SelectedShapeR { get; set; }
        public string SelectedShapeG { get; set; }
        public string SelectedShapeB { get; set; }
        public RelayCommand SetColor { get; internal set; }
        public RelayCommand SetSize { get; internal set; }
        public RelayCommand Undo { get; internal set; }
        public RelayCommand Remove { get; internal set; }
        public RelayCommand Export { get; internal set; }
        public RelayCommand ClearAll { get; internal set; }
        public RelayCommand AddLineHorizontal { get; internal set; }
        public RelayCommand AddSquare { get; internal set; }
        public RelayCommand AddTriangle { get; internal set; }
        public RelayCommand AddCircle { get; internal set; }





        public MainViewModel()
        {
            InitializeMouseEvents();
            InitializeFunctionButtons();
            InitializeShapeButtons();
            paint = Paint.GetInstance();            
            Bitmap = paint.GetBitmap();
            ListBoxShapes = paint.GetShapes();
            SelectedShapeId = -1;            
        }

        private void InitializeMouseEvents()
        {
            Image_MouseLeftButtonDown = new RelayCommand(MouseEventArgs =>
            {
                Trace.WriteLine("left mouse clicked");
                Trace.WriteLine("SelectedShapeId: "+SelectedShapeId);
                if (SelectedShapeId == -1) return;
                var e = (MouseEventArgs)MouseEventArgs;
                var pos = e.GetPosition((Image)e.Source);
                pos = new WPoint((int)pos.X, (int)pos.Y);
                Trace.WriteLine("poistion clicked: "+pos);
                paint.Move(SelectedShapeId, pos);
                Trace.WriteLine("shapes: " + paint.GetShapes());

            });
        }
        private void InitializeShapeButtons()
        {
            AddLineHorizontal = new RelayCommand(list =>
                AddShape(0)
            );
            AddSquare = new RelayCommand(list =>
                 AddShape(1)
            );
            AddTriangle = new RelayCommand(list =>
                AddShape(2)
            );
            AddCircle = new RelayCommand(list =>
            AddShape(3)
            );
        }

        private void InitializeFunctionButtons()
        {
            Undo = new RelayCommand(sender => 
            {
                paint.Undo();
            });
            ClearAll = new RelayCommand(sender => 
            {
                    paint.ClearAll();
                    ListBoxShapes = paint.GetShapes();
                    SelectedShapeId = -1;
            });
            Export = new RelayCommand(sender => {

                var dialog = new SaveFileDialog();
                dialog.Title = "Export drawing";
                dialog.InitialDirectory = Directory.GetCurrentDirectory();
                dialog.Filter = "BMP image files|*.bmp|PNG image files|*.png|JPG image files|*.jpg"; 
                dialog.FilterIndex = 1; 

                if (dialog.ShowDialog() == true)
                {
                    paint.Export(dialog.FilterIndex - 1, dialog.FileName);
                }
            });
            SetColor = new RelayCommand(sender => {
                if (SelectedShapeId < 0) { return; }
                if (
                       byte.TryParse(SelectedShapeR, out byte r) 
                    && byte.TryParse(SelectedShapeG, out byte g) 
                    && byte.TryParse(SelectedShapeB, out byte b)
                )
                {
                    paint.SetColor(SelectedShapeId, MColor.FromRgb(r,g,b));
                }
                else
                {
                    // show dialog
                }
            });
            SetSize = new RelayCommand(sender => {
                if (SelectedShapeId < 0) { return; }
                if (int.TryParse(SelectedShapeSize, out int result))
                {
                    paint.SetSize(SelectedShapeId, result);
                }
                else
                {
                    // show dialog
                }
                    
            });
            Remove = new RelayCommand(sender => {
                if(SelectedShapeId < 0) { return; }
                paint.Remove(SelectedShapeId);
            });
        }
        private void AddShape(int paletteIndex)
        {
            paint.Add(paletteIndex);
        }





    }
}

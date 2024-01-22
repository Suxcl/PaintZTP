using PaintZtp;
using PaintZTP.Model;
using Microsoft.Win32;
using System;
using System.Collections;
using System.IO;
using System.Windows;
using WPoint = System.Windows.Point;
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







        public MainViewModel()
        {
            InitializeMouseEvents();
            InitializeShapeButtons();
            InitializeFunctionButtons();

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
                AddShape(0, (ListBox)list)
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
            Export = new RelayCommand(sender => { });
            SetColor = new RelayCommand(sender => { });
            SetSize = new RelayCommand(sender => { });
            Remove = new RelayCommand(list => {
                var listBox = (ListBox)list;
                var index = listBox.SelectedIndex;
                if(index < 0) { return; }
                if (paint.Remove(index))
                {
                    var count = listBox.Items.Count;
                    if(count == 0)
                    {
                        listBox.SelectedIndex = -1;
                    }
                    else
                    {
                        if(index == count)
                        {
                            index=index-1;
                        }
                        listBox.SelectedIndex = index;
                    }
                }
            });
        }

        private void AddShape(int index, ListBox listBox)
        {
            Trace.WriteLine("AddShape in viewmodel");
            paint.Add(index);
            SelectedShapeId = 0;
        }
        


        
    }
}

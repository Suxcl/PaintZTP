﻿using PaintZTP.Model.Commands;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PaintZTP.Model.Shapes;
using System.Diagnostics;
using System.Collections;
using System;
using System.Xml.Linq;
using System.Collections.ObjectModel;
using PaintZTP.Model.Adapter;

namespace PaintZTP.Model
{
    internal class Paint
    {
        private static readonly Paint Instance = new Paint();

        private WriteableBitmap Bitmap;
        private CommandController commandController;
        private Shapes.Shape[] Shapes;
        private WriteableBitmapAdapter WBAdapter;
        
       

        public static Paint GetInstance()
        {
            return Instance;
        }

        private Paint()
        {
            Bitmap = new WriteableBitmap(700, 560, 96, 96, PixelFormats.Bgra32, null);
            Shapes = new Shapes.Shape[3];
            Shapes[0] = new LineHorizontal(new Point(100,100));
            WBAdapter = new WriteableBitmapAdapter();
            commandController = new CommandController();
        }

        public WriteableBitmap GetBitmap()
        {
            return Bitmap;
        }
        public IEnumerable GetShapes()
        {
            return commandController.GetShapes();
        }
        public void ClearAll()
        {
            commandController.Clear();
            WBAdapter.ClearBitmapToWhite(Bitmap);
            

        }
 
        public void Add(int index)
        {
            Trace.WriteLine("Add in Paint "+index);
            var shape = Shapes[index].Clone();
            Bitmap = shape.Draw(Bitmap);
            commandController.Add(shape);
            Redraw();
        }
        public void Redraw()
        {
            Trace.WriteLine("Redraw in Paint");
            WBAdapter.ClearBitmapToWhite(Bitmap);
            var shapeIterator = commandController.GetShapesIterator();
            while (shapeIterator.PossibleRead())
            {
                var s = shapeIterator.Current();
                Trace.WriteLine("Drawn Shape info shape: " + s + " shape info: " + s.center);
                Bitmap = s.Draw(Bitmap);
                
            }
        }
        public bool Move(int index, Point newCenter)
        {
            if (commandController.Move(index, newCenter))
            {
                Redraw();
                return true;
            }
            return false;
        }
        public bool Undo()
        {
            if (commandController.Undo())
            {
                Redraw();
                return true;
            }
            return false;
        }
        public bool Remove(int index)
        {
            if (commandController.Remove(index)) { Redraw(); return true;}
            return false;

        }



    }
}
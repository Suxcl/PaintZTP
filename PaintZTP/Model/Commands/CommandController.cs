using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using PaintZTP.Model.Shapes;
using PaintZTP.Model.Commands;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows.Shapes;
using System.Security.Cryptography;
using Shape = PaintZTP.Model.Shapes.Shape;
using PaintZtp.Model.Commands;
using MColor = System.Windows.Media.Color;
using System.Reflection;

namespace PaintZTP.Model.Commands
{
    public class CommandController
    {

        private ObservableCollection<Shape> Shapes;
        private Stack<Command> History;


        public CommandController()
        {
            Shapes = new ObservableCollection<Shape>();
            History = new Stack<Command>();
        }

        public IEnumerable GetShapes()
        {
            return Shapes;
        }
        private void TraceHistory()
        {
            Trace.WriteLine("Shape.count: " + Shapes.Count);
            foreach(Shape s in Shapes)
            {
                Trace.WriteLine("Shape: " + s + "center " + s.center);
            }
            for(int i = 0; i < Shapes.Count; i++)
            {
                Trace.WriteLine("Index: "+i+" Shape: " + Shapes[i] + "center " + Shapes[i].center);
            }
            Trace.WriteLine("History.count: " + History.Count);

            foreach(Command c in History)
            {
                Trace.WriteLine("command: " + c);
            }
        }
        public void Add(Shape shape)
        {
            
            History.Push(new Add(Shapes));
            Shapes.Insert(0, shape);
            TraceHistory();
            Trace.WriteLine("Added shape: " + shape + " to Commands history History:" + History + " Shapes " + Shapes);
        }
        public bool indexChecker(int index)
        {
            if (index < 0 || index >= Shapes.Count)
            {    
                return true;
            }
            return false;
        }
        public bool Remove(int index)
        {
            
            Trace.WriteLine("Removed shape from Commands history");
            
            if (indexChecker(index)) { return false; }
            var removed = Shapes[index];
            History.Push(new Remove(Shapes, index, removed));
            Shapes.RemoveAt(index);
            TraceHistory();
            Trace.WriteLine("Removing index: " + index);
            return true;
        }

        public bool Move(int index, Point newCenter)
        {
            Trace.WriteLine("Move in CC");
            TraceHistory();
            Trace.WriteLine("Index: "+ index);
            if (indexChecker(index)) { return false; }
            var moved = Shapes[index];
            var oldCenter = moved.Move(newCenter);
            History.Push(new Move(moved, oldCenter));
            TraceHistory();
            Trace.WriteLine("Move in CommandController shape: " + moved + " oldcenter: " + oldCenter + " newCenter: " + newCenter);
            return true;
        }
        public void Clear()
        {
            Trace.WriteLine("Clear in CC");
            Shapes.Clear();
            History.Clear();
            TraceHistory();
            
        }
        public bool Undo()
        {
            Trace.WriteLine("Undo in CC");
            TraceHistory();
            if (History.Count == 0)
                return false;
            History.Pop().Undo();
            TraceHistory();
            return true;
        }

        public void SetSize(int index, int Size)
        {
            if (indexChecker(index)) { return; }
            var toChange = Shapes[index];
            var oldSize = toChange.size;
            toChange.SetSize(Size);
            History.Push(new SetSize(toChange, oldSize));
        }
        public void SetColor(int index, MColor color)
        {
            if (indexChecker(index)) { return; }
            var toChange = Shapes[index];
            var oldSize = toChange.size;
            toChange.SetColor(color);
            History.Push(new SetSize(toChange, oldSize));
        }

        public class ShapesIterator
        {
            private ObservableCollection<Shape> shapes;
            private int index;

            public ShapesIterator(ObservableCollection<Shape> Items)
            {
                shapes = Items;
                index = shapes.Count - 1;
            }
            
            public bool PossibleRead() => index >= 0;
            public Shape Current() => shapes[index--];
        }
        public ShapesIterator GetShapesIterator()
        {
            return new ShapesIterator(this.Shapes);
        }
        
    }

}

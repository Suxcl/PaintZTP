using System.Collections.ObjectModel;
using PaintZTP.Model.Shapes;

namespace PaintZTP.Model.Commands
{
    public class Remove :Command
    {
        private ObservableCollection<Shape> Shapes;
        private int RemovedIndex;
        private Shape Removed;

        public Remove(ObservableCollection<Shape> shapes, int removedIndex, Shape removed)
        {
            Shapes = shapes;
            RemovedIndex = removedIndex;
            Removed = removed;
        }

        public override void Undo()
        {
            Shapes.Insert(RemovedIndex, Removed);
        }
    }
}

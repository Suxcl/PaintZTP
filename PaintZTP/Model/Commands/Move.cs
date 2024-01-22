using System.Windows;
using PaintZTP.Model.Shapes;

namespace PaintZTP.Model.Commands
{
    public class Move : Command
    {
        private Shape Moved;
        private Point oldCenter;
        

        public Move(Shape moved, Point point)
        {
            Moved = moved;
            oldCenter = moved.center;
        }

        public override void Undo()
        {
            Moved.Move(oldCenter);
        }
    }
}

using System.Windows;
using PaintZTP.Model.Shapes;
namespace PaintZTP.Model.Commands
{
    internal class SetSize : Command
    {
        private Shape shape;
        private int oldSize;

        public SetSize(Shape s, int oldSize)
        {
            shape = s;
            this.oldSize = oldSize;
        }
        public override void Undo()
        {
            shape.SetSize(oldSize);
        }
    }
}

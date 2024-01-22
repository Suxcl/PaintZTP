using System.Windows;
using PaintZTP.Model.Shapes;
using System.Windows.Media;
namespace PaintZTP.Model.Commands
{
    internal class SetColor : Command
    {
        private Shape shape;
        private System.Windows.Media.Color oldColor;

        public SetColor(Shape s, System.Windows.Media.Color oldColor)
        {
            shape = s;
            this.oldColor = oldColor;
        }
        public override void Undo()
        {
            shape.SetColor(oldColor);
        }
    }
}

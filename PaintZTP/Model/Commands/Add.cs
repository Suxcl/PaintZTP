using System.Collections.ObjectModel;
using PaintZTP.Model.Commands;
using PaintZTP.Model.Shapes;

namespace PaintZtp.Model.Commands
{
    public class Add : Command
    {
        private ObservableCollection<Shape> Shapes;
        

        public Add(ObservableCollection<Shape> shapes)
        {
            Shapes = shapes;
        }


        public override void Undo()
        {
            Shapes.RemoveAt(0);
        }
    }
}

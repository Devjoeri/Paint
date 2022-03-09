using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Paint.Commands
{
    class Resize : ICommand
    {
        Shape shape;
        Canvas canvas;
        public Resize(Shape shape, Canvas canvas)
        {
            this.shape = shape;
            this.canvas = canvas;
        }
        public void Execute()
        {
            canvas.Children.Add(shape);
        }

        public void Undo()
        {
            canvas.Children.Remove(shape);
        }
    }
}

using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Paint.Commands
{
    class Draw : ICommand
    {
        BaseShape shape;
        Canvas canvas;
        public Draw(BaseShape shape, Canvas canvas)
        {
            this.shape = shape;
            this.canvas = canvas;
        }
        public void Execute()
        {
            shape.Accept(new VisitorDraw(canvas));
        }

        public void Undo()
        {
            shape.Accept(new VisitorDelete(canvas));
        }
    }
}

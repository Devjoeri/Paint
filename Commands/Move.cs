using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Paint.Commands
{
    class Move : ICommand
    {
        MasterShape shape;
        Point position;
        Point oldPosition;
        public Move(MasterShape shape, Canvas canvas, Point oldPosition, Point position)
        {
            this.shape = shape;
            this.oldPosition = oldPosition;
            this.position = position;
        }
        public void Execute()
        {
            // have old position and add the new position to it, so it becomes the new position
            shape.Accept(new VisitorMove(position));
        }

        public void Undo()
        {
            shape.Accept(new VisitorMove(oldPosition));
        }
    }
}

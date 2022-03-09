using Paint.Composite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Paint.Visitors
{
    class VisitorMove : IVisitor
    {
        Point newPoint;
        public VisitorMove(Point position)
        {
            newPoint = position;
        }
        public void Visit(Tree tree)
        {
            foreach(var component in tree.children)
            {
                component.Accept(this);
            }
        }

        public void visitEllipse(BaseShape shape)
        {
            Canvas.SetTop(shape, newPoint.Y);
            Canvas.SetLeft(shape, newPoint.X);
        }

        public void visitRectangle(BaseShape shape)
        {
            Canvas.SetTop(shape, newPoint.Y);
            Canvas.SetLeft(shape, newPoint.X);
        }
        public void visitOrnament(Ornament shape)
        {
            Canvas.SetTop(shape, newPoint.Y);
            Canvas.SetLeft(shape, newPoint.X);
        }

    }
}

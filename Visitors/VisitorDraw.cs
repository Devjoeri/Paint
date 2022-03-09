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
    class VisitorDraw : IVisitor
    {
        Canvas canvas;
        public VisitorDraw(Canvas canvas)
        {
            this.canvas = canvas;
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
            canvas.Children.Add(shape);
        }

        public void visitRectangle(BaseShape shape)
        {
            canvas.Children.Add(shape);
        }
        public void visitOrnament(Ornament shape)
        {
            canvas.Children.Add(shape);
        }

    }
}

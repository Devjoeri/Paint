using Paint.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Visitors
{
    class VisitorResize : IVisitor
    {
        public void Visit(Tree tree)
        {
            foreach (var component in tree.children)
            {
                component.Accept(this);
            }
        }

        public void visitEllipse(BaseShape shape)
        {
            throw new NotImplementedException();
        }

        public void visitRectangle(BaseShape shape)
        {
            throw new NotImplementedException();
        }
        public void visitOrnament(Ornament shape)
        {
            throw new NotImplementedException();
        }
    }
}

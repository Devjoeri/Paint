using Paint.Composite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Visitors
{
    public interface IVisitor
    {
        public void Visit(Tree tree);
        public void visitRectangle(BaseShape shape);
        public void visitEllipse(BaseShape shape);

        public void visitOrnament(Ornament ornament);
    }
}

using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace Paint
{
    /// <summary>
    /// Abstract shape 
    /// </summary>
    public abstract class MasterShape : Shape
    {
        public abstract string Display(MasterShape shape, int depth);

        public abstract void Accept(IVisitor visitor);
    }
}

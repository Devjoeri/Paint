using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint.Strategies
{
    public interface IStrategy
    {
        public string GetString(MasterShape baseShape, int depth);
        Geometry GetGeometry();

        public void Accept(IVisitor visitor, BaseShape baseShape);
    }
}

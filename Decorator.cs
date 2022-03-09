using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Paint
{
    public abstract class Decorator : MasterShape
    {
        public MasterShape shape;
        public Decorator(MasterShape shape)
        {
            this.shape = shape;
        }
        //protected override Geometry DefiningGeometry => throw new NotImplementedException();
    }
}

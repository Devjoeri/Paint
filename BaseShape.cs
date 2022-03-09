using Paint.Strategies;
using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Paint
{
    public class BaseShape : MasterShape
    {
        private int x, y, width, height;
        public IStrategy Strategy;

        public BaseShape(IStrategy s)
        {
            Strategy = s;
        }

        protected override Geometry DefiningGeometry => Strategy.GetGeometry();

        public int StrategyHeight { get; internal set; }
        public int StrategyWidth { get; internal set; }


        public override string Display(MasterShape shape, int depth)
        {
            return Strategy.GetString(shape, depth);
        }

        public override void Accept(IVisitor visitor)
        {
            Strategy.Accept(visitor, this);
        }
    }
}

using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint
{
    public class Ornament : Decorator
    {
        string text = "";
        string position = "";

        protected override Geometry DefiningGeometry
        {
            get
            {
                Point p1 = new Point(0.0d, 0.0d);
                Point p2 = new Point(0, 0);
                Point p3 = new Point(0, 0);
                Point p4 = new Point(0, 0.0d);

                List<PathSegment> segments = new List<PathSegment>(3);
                segments.Add(new LineSegment(p1, true));
                segments.Add(new LineSegment(p2, true));
                segments.Add(new LineSegment(p3, true));
                segments.Add(new LineSegment(p4, true));

                List<PathFigure> figures = new List<PathFigure>(1);
                PathFigure pf = new PathFigure(p1, segments, true);
                figures.Add(pf);

                Geometry geometry = new PathGeometry(figures, FillRule.EvenOdd, null);
                return geometry;
            }
        }

        public Ornament(MasterShape shape) : base(shape) { }

        public void setText(string text)
        {
            this.text = text;
        }

        public void setLocation(string location)
        {
            this.position = location;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.visitOrnament(this);
        }

        public void Draw(Canvas paintCanvas)
        {
            var label = new TextBlock();
            label.Text = this.text;
            //get shape position and height and witdth. Soo get center of elemement en to bottom sutract half
            Canvas.SetLeft(label, 100);
            Canvas.SetTop(label, 100);
            paintCanvas.Children.Add(label);
        }

        public override string Display(MasterShape shape, int depth)
        {
            return $"{new String('-', depth)}ornament {position} {text}";
        }
    }
}

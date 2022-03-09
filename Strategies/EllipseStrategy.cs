using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Paint.Strategies
{
    class EllipseStrategy : IStrategy
    {
        public EllipseGeometry EllipseGeometry = new EllipseGeometry();
        public double Height { get; set; }
        public double Width { get; set; }

        public EllipseStrategy(double width, double height)
        {
            Height = height;
            Width = width;
        }

        public Geometry GetGeometry()
        {
            EllipseGeometry.RadiusX = Width;
            EllipseGeometry.RadiusY = Height;
            return EllipseGeometry;
        }

        public string GetString(MasterShape baseShape, int depth)
        {
            return $"{new String('-', depth)}Ellipse {Canvas.GetLeft(baseShape)} {Canvas.GetTop(baseShape)} {Width} {Height} \n";
        }

        public void Accept(IVisitor visitor, BaseShape baseShape)
        {
            visitor.visitEllipse(baseShape);
        }
    }
}

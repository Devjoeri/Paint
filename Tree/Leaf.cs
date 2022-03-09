using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Paint.Composite
{
    public class Leaf : Component
    {
        // Constructor
        public Shape shape;
        string shapeType; 
        public Leaf(Shape shape)
        {
            this.shape = shape;
        }
        public override void Add(Component c)
        {
            Console.WriteLine("Cannot add to a leaf");
        }
        public override void Remove(Component c)
        {
            Console.WriteLine("Cannot remove from a leaf");
        }
        public override void Display(int depth)
        {
            if (shape.GetType() == typeof(Rectangle))
            {
                shapeType = "rectangle";
            }
            if (shape.GetType() == typeof(Ellipse))
            {
                shapeType = "ellipse";
            }

            Debug.WriteLine(new String('-', depth) + $"{shapeType} {Canvas.GetLeft(shape)} {Canvas.GetTop(shape)} {shape.Height} {shape.Width}");
        }
    }
}

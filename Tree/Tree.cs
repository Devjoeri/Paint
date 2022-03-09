using Paint.Visitors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;

namespace Paint.Composite
{
    public class Tree : MasterShape
    {
        protected override Geometry DefiningGeometry { get; }
        public List<MasterShape> children = new List<MasterShape>();
        // Constructor
        public void Add(MasterShape component)
        {
            children.Add(component);
        }
        public void Remove(MasterShape component)
        {
            children.Remove(component);
        }
        public override string Display(MasterShape shape, int depth)
        {
            List<string> list = new List<string>();
            string returnFullString = new String('-', depth);
            returnFullString += $"group {this.children.Count}\n";
            depth += 4;
            // Recursively display child nodes
            foreach (MasterShape component in children)
            {
                returnFullString += $"{component.Display(component, depth)}";
            }
            return returnFullString;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public MasterShape FindByShape(MasterShape selectedShape)
        {
            foreach (MasterShape shape in children)
            {
                if (shape.Equals(selectedShape))
                {
                    return this;
                }

                if (shape is Tree box)
                {
                    var result = box.FindByShape(selectedShape);
                    if (result != null)
                        return result;
                }
            }
            return null;
        }
    }
}

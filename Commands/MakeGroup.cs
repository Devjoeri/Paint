using Paint.Composite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Paint.Commands
{
    class MakeGroup : ICommand
    {
        List<MasterShape> _shapes;
        Tree _parent;
        Tree _child = new Tree();
        public MakeGroup(List<MasterShape> shapes, Tree parent)
        {
            this._shapes = shapes;
            this._parent = parent;
        }
        public void Execute()
        {
            foreach (var component in _shapes)
            {
                
                _child.Add(component);
                _parent.Remove(component);
                
            }
            _parent.Add(_child);
            _shapes.Clear();

        }

        public void Undo()
        {
            _parent.Remove(_child);
            foreach (var component in _shapes)
            {
                _parent.Add(component);
            }
        }
    }
}

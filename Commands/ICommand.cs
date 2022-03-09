using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint.Commands
{
    public interface ICommand
    {
        void Undo();
        void Execute();
    }
}

using Paint.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paint
{
    public sealed class ActionHandler
    {
        private ActionHandler() { }

        private static ActionHandler _instance;
        public static ActionHandler GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ActionHandler();
            }
            return _instance;
        }
        //the stack where all the commands are stored
        private readonly Stack<ICommand> _commands = new Stack<ICommand>();
        //the stack where all the undo commands are stored 
        private readonly Stack<ICommand> _undoCommands = new Stack<ICommand>();

        
        public void Undo()
        {
            ICommand command = _commands.Pop();
            command.Undo();
            _undoCommands.Push(command);
        }
        public void Redo()
        {
            ICommand command = _undoCommands.Pop();
            command.Execute();
            _commands.Push(command);
        }

        public void AddCommand(ICommand command)
        {
            _commands.Push(command);
            command.Execute();
        }

    }
}

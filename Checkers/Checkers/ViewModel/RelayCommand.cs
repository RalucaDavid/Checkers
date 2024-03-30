using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Checkers.ViewModel
{
    class RelayCommand : ICommand
    {
        private readonly Action<object> command;
        public event EventHandler CanExecuteChanged;
        public RelayCommand(Action<object> commandToDo)
        {
            command = commandToDo;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            command(parameter);
        }
    }
}

using System;
using System.Windows.Input;

namespace MachineLearningSoftware.Entities
{
    public class CommandDelegate : ICommand
    {
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _execute;

        public event EventHandler CanExecuteChanged;
        
        public CommandDelegate(Action<object> execute,
                       Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
        
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
        
        public bool ExecuteTrue(object context)
        {
            return true;
        }

        private bool ExecuteFalse(object context)
        {
            return false;
        }
    }
}

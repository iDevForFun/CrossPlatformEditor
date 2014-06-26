using System;
using System.Diagnostics;
using System.Windows.Input;
using WindowsEditor.Wpf.Annotations;

namespace WindowsEditor.Wpf.ViewModel
{
    [PublicAPI]
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Action executeViod;
        private readonly Predicate<object> canExecute;


        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public RelayCommand(Action execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            executeViod = execute;
            this.canExecute = canExecute;
        }

        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        [PublicAPI]
        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (execute != null)
            {
                execute(parameter);
                return;
            }
            if (executeViod != null)
            {
                executeViod();
            }

        }
    }
}
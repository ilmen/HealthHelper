using System;
using System.Windows.Input;

namespace HealthWorkHelper.Classes.Commands
{
    /// <summary> Класс делегата для команды без параметра </summary>
    public abstract class CommandBase : ICommand
    {
        public Action CommandAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                CommandAction();
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc();
        }

        public abstract event EventHandler CanExecuteChanged;
    }

    /// <summary> Класс делегата для команды c параметром </summary>
    public abstract class DelegateCommandBase<T> : ICommand
    {
        public Action<T> CommandAction { get; set; }
        public Func<T, bool> CanExecuteFunc { get; set; }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                CommandAction((T)parameter);
            }
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null || CanExecuteFunc((T)parameter);
        }

        public abstract event EventHandler CanExecuteChanged;
    }
}

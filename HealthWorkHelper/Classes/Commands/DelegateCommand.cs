using System;
using System.Windows.Input;

namespace HealthWorkHelper.Classes.Commands
{
    /// <summary> Класс делегата для команды без параметра </summary>
    public class DelegateCommand : CommandBase
    {
        public override event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    /// <summary> Класс делегата для команды c параметром </summary>
    public class DelegateCommand<T> : DelegateCommandBase<T>
    {
        public override event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

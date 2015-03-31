using System;
using System.Windows.Input;
using System.Windows.Threading;

namespace HealthWorkHelper.Classes.Commands
{
    /// <summary> Класс делегата для команды без параметра </summary>
    public class TimedCommand : CommandBase
    {
        private event EventHandler TimeCheckedCome = delegate { };

        public TimedCommand()
        {
            var tmr = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            tmr.Tick += (s, e) => TimeCheckedCome(s, e);
            tmr.Start();
        }

        public TimedCommand(EventHandler timeCheckedCome)
            : this()
        {
            timeCheckedCome += (s, e) => TimeCheckedCome(s, e);
        }

        public override event EventHandler CanExecuteChanged
        {
            add { TimeCheckedCome += value; }
            remove { TimeCheckedCome -= value; }
        }
    }

    /// <summary> Класс делегата для команды c параметром </summary>
    public class TimedCommand<T> : DelegateCommandBase<T>
    {
        private event EventHandler TimeCheckedCome = delegate { };

        public TimedCommand()
        {
            var tmr = new DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
            tmr.Tick += (s, e) => TimeCheckedCome(s, e);
            tmr.Start();
        }

        public TimedCommand(EventHandler timeCheckedCome)
            : this()
        {
            timeCheckedCome += (s, e) => TimeCheckedCome(s, e);
        }

        public override event EventHandler CanExecuteChanged
        {
            add { TimeCheckedCome += value; }
            remove { TimeCheckedCome -= value; }
        }
    }
}
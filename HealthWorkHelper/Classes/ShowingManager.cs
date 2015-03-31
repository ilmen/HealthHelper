using System;
using System.Windows;
using System.Windows.Input;

namespace HealthWorkHelper.Classes
{
    public class ShowingManager
    {
        public static readonly int DelaySecondsDuration = 5;
        public static readonly int RelaxSecondsDuration = 10;
        public static readonly int WorkSecondsDuration = 20;

        // время открытия = текущее + время смещения
        // время смещения = время работы или время переноса
        // в классе 2 времени:
        //      время открытия (для ShowDialog)
        //      время последней зарядки - для UI

        private Window storedWindow
        { get; set; }

        private DateTime OpenTime
        { get; set; }

        public DateTime LastRelaxTime
        { get; private set; }

        public event EventHandler Showing = delegate { };

        public ICommand ToWorkCommand
        { get; private set; }

        public ICommand DoDelayCommand
        { get; private set; }

        public ShowingManager(Window window)
        {
            storedWindow = window;
            storedWindow.Closing += (s, e) => e.Cancel = true;  // отменяем закрытие окна

            OpenTime = DateTime.Now.AddSeconds(WorkSecondsDuration);
            LastRelaxTime = DateTime.Now;

            ToWorkCommand = new DelegateCommand()
            {
                CanExecuteFunc = () => OpenTime.AddSeconds(RelaxSecondsDuration) <= DateTime.Now,
                CommandAction = () => ToWork()
            };

            DoDelayCommand = new DelegateCommand()
            {
                CommandAction = () => DoDelay()
            };
        }

        private void ToWork()
        {
            OpenTime = DateTime.Now.AddSeconds(WorkSecondsDuration);
            LastRelaxTime = DateTime.Now;
            storedWindow.Hide();
        }

        private void DoDelay()
        {
            OpenTime = DateTime.Now.AddSeconds(DelaySecondsDuration);
            storedWindow.Hide();
        }

        public void Start()
        {
            while(true)
            {
                if (OpenTime <= DateTime.Now)
                {
                    Showing(this, new EventArgs());
                    storedWindow.ShowDialog();
                }

                System.Threading.Thread.Sleep(200);
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}

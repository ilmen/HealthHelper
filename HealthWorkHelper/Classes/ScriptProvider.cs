using HealthWorkHelper.Classes.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace HealthWorkHelper.Classes
{
    public class ScriptProvider
    {
        // время открытия = текущее + время смещения
        // время смещения = время работы или время переноса
        // в классе 2 времени:
        //      время открытия (для ShowDialog)
        //      время последней зарядки - для UI

        #region Fields & Properties
        private DateTime OpenTime
        { get; set; }

        private Window StoredWindow
        { get; set; }

        private IScriptManagerSettingProvider StoredSettingProvider
        { get; set; }

        public TimeSpan DelayDuration
        {
            get
            {
                return StoredSettingProvider.DelayDuration;
            }
        }
        public TimeSpan RelaxDuration
        {
            get
            {
                return StoredSettingProvider.RelaxDuration;
            }
        }
        public TimeSpan WorkDuration
        {
            get
            {
                return StoredSettingProvider.WorkDuration;
            }
        }

        public DateTime LastRelaxTime
        { get; private set; }

        public event EventHandler Showing = delegate { };

        public ICommand ToWorkCommand
        { get; private set; }

        public ICommand DoDelayCommand
        { get; private set; } 
        #endregion

        public ScriptProvider(IScriptManagerSettingProvider settingProvider, Window window)
        {
            StoredSettingProvider = settingProvider;

            StoredWindow = window;
            StoredWindow.Closing += (s, e) => e.Cancel = true;  // отменяем закрытие окна

            OpenTime = DateTime.Now;
            if (!System.Diagnostics.Debugger.IsAttached) OpenTime = OpenTime.Add(WorkDuration);
            LastRelaxTime = DateTime.Now;

            ToWorkCommand = new TimedCommand(Showing)
            {
                CanExecuteFunc = () => OpenTime.Add(RelaxDuration) <= DateTime.Now,
                CommandAction = () => ToWork()
            };

            DoDelayCommand = new DelegateCommand()
            {
                CommandAction = () => DoDelay()
            };
        }

        private void ToWork()
        {
            OpenTime = DateTime.Now.Add(WorkDuration);
            LastRelaxTime = DateTime.Now;
            StoredWindow.Hide();
        }

        private void DoDelay()
        {
            OpenTime = DateTime.Now.Add(DelayDuration);
            StoredWindow.Hide();
        }

        public void Start()
        {
            while (true)
            {
                if (OpenTime <= DateTime.Now)
                {
                    Showing(this, new EventArgs());
                    StoredWindow.ShowDialog();
                }

                System.Threading.Thread.Sleep(200);
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}

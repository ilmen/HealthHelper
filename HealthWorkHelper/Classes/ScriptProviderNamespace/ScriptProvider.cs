using HealthWorkHelper.Classes.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace HealthWorkHelper.Classes.ScriptProviderNamespace
{
    public class ScriptProvider : ITimingProvider
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

        public event EventHandler OnWork = delegate { };
        public event EventHandler OnDelay = delegate { };
        public event EventHandler OnShowing = delegate { };

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

            ToWorkCommand = new TimedCommand(OnShowing)
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

            OnWork(this, new EventArgs());
        }

        private void DoDelay()
        {
            OpenTime = DateTime.Now.Add(DelayDuration);
            StoredWindow.Hide();

            OnDelay(this, new EventArgs());
        }

        public void Start()
        {
            while (true)
            {
                if (OpenTime <= DateTime.Now)
                {
                    OnShowing(this, new EventArgs());
                    StoredWindow.ShowDialog();
                }

                System.Threading.Thread.Sleep(200);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        public TimeSpan GetTimeToRelax()
        {
            return LastRelaxTime + WorkDuration - DateTime.Now;
        }
    }
}

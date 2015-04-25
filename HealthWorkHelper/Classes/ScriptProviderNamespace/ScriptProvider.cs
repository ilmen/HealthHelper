using HealthWorkHelper.Classes.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace HealthWorkHelper.Classes.ScriptProviderNamespace
{
    public class ScriptProvider : ITimeToRelaxProvider
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
        
        /// <summary>
        /// Коэффициент времени для расчета промежутка, на который откладывается отдых.
        /// Промежуток считается так: MinDelayTime + (MaxDelayTime - MinDelayTime) * коэффициент
        /// </summary>
        private readonly double[] delayPeriods = new double[] { 1, 0.5, 0.25, 0 };

        private int delayPeriodsIndex = 0;

        public TimeSpan DelayDuration
        {
            get
            {
                var delta = StoredSettingProvider.MaxDelayDuration - StoredSettingProvider.MinDelayDuration;
                var offset = TimeSpan.FromSeconds(delta.Seconds * delayPeriods[delayPeriodsIndex]);
                return StoredSettingProvider.MinDelayDuration + offset;
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
            delayPeriodsIndex = 0;

            OnWork(this, new EventArgs());
        }

        private void DoDelay()
        {
            OpenTime = DateTime.Now.Add(DelayDuration);
            StoredWindow.Hide();
            if (delayPeriodsIndex < delayPeriods.Length - 1) delayPeriodsIndex++;   // получаем посл-ть: 0, 1, 2, 3, 3, 3, ...

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

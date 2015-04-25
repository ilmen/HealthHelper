using HealthWorkHelper.Classes.Converters;
using HealthWorkHelper.Classes.ScriptProviderNamespace;
using System;
using System.Windows.Threading;

namespace HealthWorkHelper.Classes.TaskIconClasses.TaskIconStates
{
    public class WorkingState : AbstractTaskIconState
    {
        private ITaskIconStateMachine taskIconManager;
        private IScriptManagerSettingProvider timingProvider;
        private DispatcherTimer showBalloonTimer;
        private DateTime lastActivateTime;

        #region Нахождение моментов отображения всплывающего сообщения
        // периодически (с логарифмическим периодом) отображаем время до отдыха
        // за основу взята функция y = ln(a - x/k) / ln(100/k) * 100, где x = 1..100, k = 3, a = 100 / k + 1
        //      ln(100/k) - коэффициент сжатия для задания области значений функции равной 1..100
        //      x/k - сжатие функции, для получения более "гладкого" графика
        //      a - x/k - зеркальное отображение функции относительно оси ординат (OY)
        // из полученной последовательности выбраны опорные точки:
        // 100; 66.[6]; 44.[4]; 22.[2]; 15.[5]; 8.[8]; 4.[4]; 2.[2] (где [n] - n в периоде)
        // для определения моментов отображения сообщения о остатке времени полученные
        // точки преобразуем в проценты от длины рабочего промежутка
        // 100% исключим из выборки, т.к. это момент смены состояния, когда сообщение отобразится принудительно 
        #endregion

        private readonly double[] showBalloonPeriods = new double[] { 0.6667, 0.4444, 0.2222, 0.1556, 0.0889, 0.0444, 0.0222 };
        private int showBalloonPeriodsIndex;

        public WorkingState(ITaskIconStateMachine taskIconManager, IScriptManagerSettingProvider timingProvider)
        {
            this.taskIconManager = taskIconManager;
            this.timingProvider = timingProvider;
            this.showBalloonTimer = new DispatcherTimer();
            this.showBalloonTimer.Tick += showBalloonTimer_Tick;
        }

        public override string GetBalloonTitle()
        {
            return "И снова - работаем...";
        }

        public override string GetBalloonText()
        {
            var timeSpanToRelax = taskIconManager.TimeToRelaxProvider.GetTimeToRelax();
            var timeToRelax = TimeSpanToStringConverter.ConvertToString(timeSpanToRelax);

            return "Запасись терпением...\r\nИли почитай чего-нибудь умное...\r\nСледующая передышка будет через " + timeToRelax + ".";
        }

        public override void Showing()
        {
            taskIconManager.SetCurrentState(taskIconManager.RelaxState);
        }

        public override void Activate()
        {
            lastActivateTime = DateTime.Now;
            showBalloonPeriodsIndex = 0;
            UpdateShowBalloonTimerIntervat();
            showBalloonTimer.Start();

            taskIconManager.ShowBallonToolTip();
        }

        public override void Deactivate()
        {
            showBalloonTimer.Stop();   
        }

        void showBalloonTimer_Tick(object sender, EventArgs e)
        {
            taskIconManager.ShowBallonToolTip();

            if (showBalloonPeriodsIndex < showBalloonPeriods.Length - 1)
            {
                showBalloonPeriodsIndex++;
                UpdateShowBalloonTimerIntervat();
            }
            else showBalloonTimer.Stop();
        }

        void UpdateShowBalloonTimerIntervat()
        {
            var nextPeriodMultiplier = showBalloonPeriods[showBalloonPeriodsIndex];
            var nextShowindTime = lastActivateTime + timingProvider.WorkDuration - TimeSpan.FromSeconds(timingProvider.WorkDuration.TotalSeconds * nextPeriodMultiplier);
            var debugInfoNowTime = DateTime.Now;
            showBalloonTimer.Interval = nextShowindTime - DateTime.Now;
        }
    }
}

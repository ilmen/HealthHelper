using HealthWorkHelper.Classes.Converters;
using HealthWorkHelper.Classes.ScriptProviderNamespace;
using System;

namespace HealthWorkHelper.Classes.TaskIconClasses.TaskIconStates
{
    public class DelayingState : AbstractTaskIconState
    {
        private ITaskIconStateMachine taskIconManager;

        public DelayingState(ITaskIconStateMachine taskIconManager)
        {
            this.taskIconManager = taskIconManager;
        }

        public override string GetBalloonTitle()
        {
            return "Отдых пока откладывается...";
        }

        public override string GetBalloonText()
        {
            var startRelaxTime = taskIconManager.TimeToRelaxProvider.GetTimeToRelax();
            var timeDelaying = TimeSpan.FromSeconds(-1 * startRelaxTime.TotalSeconds);

            return "Ты уже " + TimeSpanToStringConverter.ConvertToString(timeDelaying) + " как должен отдыхать! Глаза побереги!";
        }

        public override void DoDelay()
        {

            taskIconManager.ShowBallonToolTip();
        }

        public override void DoWork()
        {
            taskIconManager.SetCurrentState(taskIconManager.WorkingState);
        }

        //public override void Showing()
        //{
        //    taskIconManager.ShowBallonToolTip();
        //}

        public override void Activate()
        {
            taskIconManager.ShowBallonToolTip();
        }
    }
}

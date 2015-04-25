namespace HealthWorkHelper.Classes.TaskIconClasses.TaskIconStates
{
    public class RelaxState : AbstractTaskIconState
    {
        private ITaskIconStateMachine taskIconManager;

        public RelaxState(ITaskIconStateMachine taskIconManager)
        {
            this.taskIconManager = taskIconManager;
        }

        public override string GetBalloonTitle()
        {
            return "Передышка для тебя";
        }

        public override string GetBalloonText()
        {
            return "Пора передохнуть пару минут...";
        }

        public override void DoDelay()
        {
            taskIconManager.SetCurrentState(taskIconManager.DelayingState);
        }

        public override void DoWork()
        {
            taskIconManager.SetCurrentState(taskIconManager.WorkingState);
        }

        public override void Activate()
        {
            taskIconManager.ShowBallonToolTip();
        }
    }
}

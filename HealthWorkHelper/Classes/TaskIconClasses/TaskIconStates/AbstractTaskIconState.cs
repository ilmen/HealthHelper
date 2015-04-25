namespace HealthWorkHelper.Classes.TaskIconClasses.TaskIconStates
{
    /// <summary>
    /// Абстракный класс для реализации машины состояний для иконки в трее
    /// </summary>
    public abstract class AbstractTaskIconState
    {
        public abstract string GetBalloonTitle();

        public abstract string GetBalloonText();

        public virtual void DoDelay() { }

        public virtual void DoWork() { }

        public virtual void Showing() { }

        public virtual void Activate() { }

        public virtual void Deactivate() { }
    }
}

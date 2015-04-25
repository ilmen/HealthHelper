using HealthWorkHelper.Classes.ScriptProviderNamespace;
using HealthWorkHelper.Classes.TaskIconClasses.TaskIconStates;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HealthWorkHelper.Classes.TaskIconClasses
{
    public class TaskIconManager
    {
        private FakeContainer iconContainer = new FakeContainer();
        private NotifyIcon icon;

        #region State machine implementation
        public AbstractTaskIconState WorkingState
        { get; private set; }

        public AbstractTaskIconState RelaxState
        { get; private set; }

        public AbstractTaskIconState DelayingState
        { get; private set; }

        public AbstractTaskIconState CurrentState
        { get; private set; }

        public ITimeToRelaxProvider TimeToRelaxProvider
        { get; private set; }
        #endregion

        public TaskIconManager(ITimeToRelaxProvider timeToRelaxProvider, IScriptManagerSettingProvider timingProvider)
	    {
            this.TimeToRelaxProvider = timeToRelaxProvider;

            icon = new NotifyIcon(iconContainer)    // NotifyIcon не отображается, если нет контейнера для иконки - лечим это
            {
                Icon = new Icon(System.IO.Directory.GetCurrentDirectory() + "/Red.ico"),
                Text = "HealthWorkHelper",
                BalloonTipText = " ",
                BalloonTipTitle = " ",
                Visible = true,
            };
            icon.Click += (s, e) => ShowBallonToolTip();

            this.WorkingState = new WorkingState(this, timingProvider);
            this.RelaxState = new RelaxState(this);
            this.DelayingState = new DelayingState(this);
            SetCurrentState(this.WorkingState);

            this.TimeToRelaxProvider.OnWork += (s, e) => CurrentState.DoWork();
            this.TimeToRelaxProvider.OnDelay += (s, e) => CurrentState.DoDelay();
            this.TimeToRelaxProvider.OnShowing += (s, e) => CurrentState.Showing();
	    }

        public void SetCurrentState(AbstractTaskIconState newState)
        {
            if (this.CurrentState != null) this.CurrentState.Deactivate();
            this.CurrentState = newState;
            this.CurrentState.Activate();
        }

        public void ShowBallonToolTip()
        {
            icon.BalloonTipTitle = this.CurrentState.GetBalloonTitle();
            icon.BalloonTipText = this.CurrentState.GetBalloonText();
            icon.ShowBalloonTip(0);
        }
    }

    #region Fake UI container
    public class FakeContainer : IContainer
    {
        ComponentCollection components;

        public FakeContainer()
        {
            components = new ComponentCollection(new IComponent[] { });
        }

        public void Add(IComponent component, string name) { }

        public void Add(IComponent component) { }

        public ComponentCollection Components
        {
            get
            {
                return components;
            }
        }

        public void Remove(IComponent component) { }

        public void Dispose() { }
    } 
    #endregion
}

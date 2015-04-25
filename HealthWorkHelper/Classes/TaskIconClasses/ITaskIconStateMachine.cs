using HealthWorkHelper.Classes.ScriptProviderNamespace;
using HealthWorkHelper.Classes.TaskIconClasses.TaskIconStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthWorkHelper.Classes.TaskIconClasses
{
    public interface ITaskIconStateMachine
    {
        AbstractTaskIconState WorkingState { get; }

        AbstractTaskIconState RelaxState { get; }

        AbstractTaskIconState DelayingState { get; }

        AbstractTaskIconState CurrentState { get; }

        ITimingProvider TimeToRelaxProvider { get; }
        
        void SetCurrentState(AbstractTaskIconState newState);

        void ShowBallonToolTip();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthWorkHelper.Classes.ScriptProviderNamespace
{
    public class ScriptManagerSettingProvider : IScriptManagerSettingProvider
    {
        public TimeSpan MinDelayDuration
        { get; private set; }

        public TimeSpan MaxDelayDuration
        { get; private set; }

        public TimeSpan RelaxDuration
        { get; private set; }

        public TimeSpan WorkDuration
        { get; private set; }

        public TimeSpan TimeToRelaxRemindDuration
        { get; private set; }

        public ScriptManagerSettingProvider()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.MinDelayDuration = TimeSpan.FromSeconds(2);
                this.MaxDelayDuration = TimeSpan.FromSeconds(12);
                this.RelaxDuration = TimeSpan.FromSeconds(10);
                this.WorkDuration = TimeSpan.FromSeconds(60);
                this.TimeToRelaxRemindDuration = TimeSpan.FromSeconds(5);
            }
            else
            {
                this.MinDelayDuration = Properties.Settings.Default.MinDelayDuration;
                this.MaxDelayDuration = Properties.Settings.Default.MaxDelayDuration;
                this.RelaxDuration = Properties.Settings.Default.RelaxDuration;
                this.WorkDuration = Properties.Settings.Default.WorkDuration;
            }
        }
    }
}

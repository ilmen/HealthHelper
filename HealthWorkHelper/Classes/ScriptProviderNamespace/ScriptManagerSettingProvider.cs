using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthWorkHelper.Classes.ScriptProviderNamespace
{
    public class ScriptManagerSettingProvider : IScriptManagerSettingProvider
    {
        public TimeSpan DelayDuration
        { get; private set; }

        public TimeSpan RelaxDuration
        { get; private set; }

        public TimeSpan WorkDuration
        { get; private set; }

        public ScriptManagerSettingProvider()
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DelayDuration = TimeSpan.FromSeconds(10);
                this.RelaxDuration = TimeSpan.FromSeconds(5);
                this.WorkDuration = TimeSpan.FromSeconds(20);
            }
            else
            {
                this.DelayDuration = Properties.Settings.Default.DelayDuration;
                this.RelaxDuration = Properties.Settings.Default.RelaxDuration;
                this.WorkDuration = Properties.Settings.Default.WorkDuration;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthWorkHelper.Classes.ScriptProviderNamespace
{
    public interface IScriptManagerSettingProvider
    {
        TimeSpan MinDelayDuration
        { get; }

        TimeSpan MaxDelayDuration
        { get; }

        TimeSpan RelaxDuration
        { get; }

        TimeSpan WorkDuration
        { get; }
    }
}

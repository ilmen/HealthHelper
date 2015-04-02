using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthWorkHelper.Classes
{
    public interface IScriptManagerSettingProvider
    {
        TimeSpan DelayDuration
        { get; }
        TimeSpan RelaxDuration
        { get; }
        TimeSpan WorkDuration
        { get; }

    }
}

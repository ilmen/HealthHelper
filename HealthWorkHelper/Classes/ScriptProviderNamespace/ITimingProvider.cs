using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthWorkHelper.Classes.ScriptProviderNamespace
{
    public interface ITimingProvider
    {
        event EventHandler OnWork;
        event EventHandler OnDelay;
        event EventHandler OnShowing;

        TimeSpan GetTimeToRelax();
    }
}

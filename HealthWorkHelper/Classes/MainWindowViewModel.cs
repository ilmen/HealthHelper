using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using HealthWorkHelper.Classes.ScriptProviderNamespace;

namespace HealthWorkHelper.Classes
{
    public class MainWindowViewModel : ViewModelBase
    {
        private BackgroundProvider backgroundProvider;

        #region INPC - Background
        private BitmapImage _Background;

        public BitmapImage Background
        {
            get { return _Background; }
            set
            {
                if (_Background != null && _Background.Equals(value)) return;
                _Background = value;

                RaisePropertyChange("Background");
            }
        }
        #endregion
     
        public ScriptProvider ScriptProvider
        { get; private set; }

        public TimeSpan TimeInWork
        {
            get
            {
                return DateTime.Now - ScriptProvider.LastRelaxTime;
            }
        }

        public TimeSpan DelayDuration
        {
            get
            {
                return ScriptProvider.DelayDuration;
            }
        }

        public MainWindowViewModel(ScriptProvider scriptProvider, BackgroundProvider backgroundProvider)
        {
            this.backgroundProvider = backgroundProvider;
            this.ScriptProvider = scriptProvider;
            this.ScriptProvider.OnShowing += (s, e) =>
            {
                RaisePropertyChange("TimeInWork");
                this.Background = backgroundProvider.GetBackground();
            };
        }
    }
}
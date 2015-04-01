using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

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

        public DateTime LastRelaxTime
        {
            get
            {
                return ScriptProvider.LastRelaxTime;
            }
        }

        public MainWindowViewModel(ScriptProvider scriptProvider, BackgroundProvider backgroundProvider)
        {
            this.backgroundProvider = backgroundProvider;
            this.ScriptProvider = scriptProvider;
            this.ScriptProvider.Showing += (s, e) =>
            {
                RaisePropertyChange("LastRelaxTime");
                this.Background = backgroundProvider.GetBackground();
            };
        }
    }
}
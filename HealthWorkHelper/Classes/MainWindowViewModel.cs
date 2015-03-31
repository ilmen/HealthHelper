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
        private BackgroundProvider backgroundManager;

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
     
        public ShowingManager ShowingManager
        { get; private set; }

        public MainWindowViewModel(ShowingManager showingManager, BackgroundProvider backgroundManager)
        {
            this.backgroundManager = backgroundManager;
            this.ShowingManager = showingManager;
            this.ShowingManager.Showing += (s, e) => this.Background = backgroundManager.GetBackground();
        }
    }
}

//    public class MainWindowViewModel : ViewModelBase
//    {
//        public static readonly int WorkSecondsDuration = 20;  //40 * 60;
//        public static readonly int RestSecondsDuration = 5;   //60;
//        public static readonly int DelaySecondsDuration = 5;   //60;
//        public static readonly Random rndGenerator = new Random((int)DateTime.Now.Ticks);
//
//        #region ShowTime property
//        public event EventHandler ShowTimeChanged = delegate { };
//
//        private DateTime _ShowTime;
//
//        public DateTime ShowTime
//        {
//            get { return _ShowTime; }
//            set
//            {
//                _ShowTime = value;
//                ShowTimeChanged(this, new EventArgs());
//            }
//        } 
//        #endregion
//
//        public int WorkDurationInMinutes
//        {
//            get
//            {
//                return (int)(WorkSecondsDuration / 60);
//            }
//        }
//        
//        #region INPC - Background
//        private BitmapImage _Background;
//
//        public BitmapImage Background
//        {
//            get { return _Background; }
//            set
//            {
//                if (_Background != null && _Background.Equals(value)) return;
//                _Background = value;
//
//                RaisePropertyChange("Background");
//            }
//        }
//        #endregion
//        
//        public ICommand HideCommand
//        { get; private set; }
//
//        public ICommand DelayCommand
//        { get; private set; }
//
//        public MainWindowViewModel()
//        {
//            HideCommand = new HideCommand(this);
//            DelayCommand = new DelegateCommand<MainWindow>()
//            {
//                CommandAction = (w) => { if (w != null) w.Hide(); }
//            };
//        }
//
//        /// <summary>
//        /// Задает время появления для расчета нового состояния кнопки (оповещая об изменении через событие ShowTimeChanged).
//        /// Задает фоновое изображение.
//        /// </summary>
//        public void UpdateShowTime()
//        {
//            SetRandomBackground();
//            this.ShowTime = DateTime.Now;
//        }
//
//        private void SetRandomBackground()
//        {
//            //this.Background = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + @"\Images\12.jpg"));
//
//            var images = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Images\", "*.jpg");
//            if (images.Length == 0) return;
//
//            var file = images[rndGenerator.Next(0, images.Length)];
//            this.Background = new BitmapImage(new Uri(file));
//        }
//    }
//}

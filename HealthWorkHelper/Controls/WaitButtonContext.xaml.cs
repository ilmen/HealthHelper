using System.ComponentModel;
using System.Windows.Controls;

namespace HealthWorkHelper.Controls
{
    /// <summary>
    /// Логика взаимодействия для WaitButtonContext.xaml
    /// </summary>
    public partial class WaitButtonContext : UserControl, INotifyPropertyChanged
    {
        #region INPC implementation
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void RaisePropertyChange(string propertyName)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion


        //#region DP - PointerAngle
        //public double PointerAngle
        //{
        //    get { return (double)GetValue(PointerAngleProperty); }
        //    set { SetValue(PointerAngleProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for PointerAngle.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PointerAngleProperty =
        //    DependencyProperty.Register("PointerAngle", typeof(double), typeof(WaitButtonContext), new PropertyMetadata(0.0, PointerPropertyChanged));

        //private static void PointerPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var context = d as WaitButtonContext;
        //    if (context == null) return;

        //    context.PointerX = (double)e.NewValue / 50;
        //    context.PointerY = (double)e.NewValue / 50;
        //}
        //#endregion

        ////private double _PointerAngle;
        ////public double PointerAngle
        ////{
        ////     get
        ////     {
        ////         return _PointerAngle;
        ////     }
        ////    set
        ////    {
        ////        _PointerAngle = value;

        ////        PointerX = value;
        ////        PointerY = value;
        ////    }
        ////}

        ////#region DP - AnimationDuration
        ////public TimeSpan AnimationDuration
        ////{
        ////    get { return (TimeSpan)GetValue(AnimationDurationProperty); }
        ////    set { SetValue(AnimationDurationProperty, value); }
        ////}

        ////// Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        ////public static readonly DependencyProperty AnimationDurationProperty =
        ////    DependencyProperty.RegisterAttached("AnimationDuration", typeof(TimeSpan), typeof(WaitButtonContext), new PropertyMetadata(TimeSpan.FromSeconds(10), AnimationDurationPropertyChanged));

        ////private static void AnimationDurationPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        ////{

        ////}
        ////#endregion

        //#region INPC - PointerX
        //private double _PointerX;

        //public double PointerX
        //{
        //    get { return _PointerX; }
        //    set
        //    {
        //        if (_PointerX.Equals(value)) return;

        //        _PointerX = value;
        //        RaisePropertyChange("PointerX");
        //    }
        //}
        //#endregion
        
        //#region INPC - PointerY
        //private double _PointerY;

        //public double PointerY
        //{
        //    get { return _PointerY; }
        //    set
        //    {
        //        if (_PointerY.Equals(value)) return;

        //        _PointerY = value;
        //        RaisePropertyChange("PointerY");
        //    }
        //}
        //#endregion
        
        //public WaitButtonContext()
        //{
        //    var da = new DoubleAnimation(360, TimeSpan.FromSeconds(10));
        //    da.BeginAnimation(PointerAngleProperty, da);
        //}
    }
}

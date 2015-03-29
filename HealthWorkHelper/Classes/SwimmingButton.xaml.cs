using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HealthWorkHelper.Controls
{
    /// <summary>
    /// Interaction logic for SwimmingButton.xaml
    /// </summary>
    public partial class SwimmingButton : UserControl
    {
        public SwimmingButton()
        {
            InitializeComponent();

            //Button r;
        }

        #region DP - ActiveContent
        public object ActiveContent
        {
            get { return (object)GetValue(ActiveContentProperty); }
            set { SetValue(ActiveContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ActiveContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ActiveContentProperty =
            DependencyProperty.RegisterAttached("ActiveContent", typeof(object), typeof(SwimmingButton), new PropertyMetadata(null)); 
        #endregion

        #region DP - PassiveContent
        public object PassiveContent
        {
            get { return (object)GetValue(PassiveContentProperty); }
            set { SetValue(PassiveContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PassiveContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PassiveContentProperty =
            DependencyProperty.Register("PassiveContent", typeof(object), typeof(SwimmingButton), new PropertyMetadata(null)); 
        #endregion
    }
}

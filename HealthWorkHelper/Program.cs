using HealthWorkHelper.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthWorkHelper
{
    class Program
    {
        static DateTime showTime = DateTime.MinValue;

        [STAThread]
        static void Main(string[] args)
        {
            var backgroundProvider = new BackgroundProvider();

            var view = new MainWindow();
            var scriptProvider = new ScriptProvider(view);
            var vm = new MainWindowViewModel(scriptProvider, backgroundProvider);
            view.DataContext = vm;

            scriptProvider.Start();
        }
    }
}

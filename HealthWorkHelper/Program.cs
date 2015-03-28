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
            var vm = new MainWindowViewModel();
            var view = new MainWindow() { DataContext = vm };

            while(true)
            {
                if (DateTime.Now >= showTime.AddSeconds(MainWindowViewModel.WorkSecondsDuration))
                {
                    view.ShowDialog();
                    showTime = DateTime.Now;
                }

                System.Threading.Thread.Sleep(200);
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}

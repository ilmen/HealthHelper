﻿using HealthWorkHelper.Classes;
using HealthWorkHelper.Classes.ScriptProviderNamespace;
using System;

namespace HealthWorkHelper
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var backgroundProvider = new BackgroundProvider();
            var settingProvider = new ScriptManagerSettingProvider();

            var view = new MainWindow();
            var scriptProvider = new ScriptProvider(settingProvider, view);
            var vm = new MainWindowViewModel(scriptProvider, backgroundProvider);
            view.DataContext = vm;

            var iconManager = new TaskIconManager(settingProvider, scriptProvider);
            iconManager.ShowWorkIcon();

            scriptProvider.Start();
        }
    }
}

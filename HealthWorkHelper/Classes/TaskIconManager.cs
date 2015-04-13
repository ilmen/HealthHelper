﻿using HealthWorkHelper.Classes.ScriptProviderNamespace;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace HealthWorkHelper.Classes
{
    public class TaskIconManager
    {
        private FakeContainer iconContainer = new FakeContainer();
        private NotifyIcon icon;
        private IScriptManagerSettingProvider settingProvider;
        private ITimeToRelaxProvider timeToRelaxProvider;
        private bool cancelRemindTimeToRelax;

        public TaskIconManager(IScriptManagerSettingProvider settingProvider, ITimeToRelaxProvider timeToRelaxProvider)
	    {
            this.settingProvider = settingProvider;

            this.timeToRelaxProvider = timeToRelaxProvider;

            this.timeToRelaxProvider.OnWork += (s, e) =>
                {
                    ShowWorkIcon();
                    RunTimeToRelaxReminder();
                };
            
            this.timeToRelaxProvider.OnDelay += (s, e) => ShowDelayIcon();
            
            this.timeToRelaxProvider.OnShowing += (s, e) =>
                {
                    cancelRemindTimeToRelax = true;
                    ShowRelaxIcon();
                };

            icon = new NotifyIcon(iconContainer)    // NotifyIcon не отображается, если нет контейнера для иконки - лечим это
            {
                Icon  = new Icon(System.IO.Directory.GetCurrentDirectory() + "/Red.ico"),
                Text = "HealthWorkHelper",
                BalloonTipText = " ",
                BalloonTipTitle = " ",
                Visible = true,
            };
            icon.Click += (s, e) => ShowWorkIcon();
	    }

        private void RunTimeToRelaxReminder()
        {
            cancelRemindTimeToRelax = false;
            var timer = new System.Timers.Timer(settingProvider.TimeToRelaxRemindDuration.TotalMilliseconds) { AutoReset = false };
            timer.Elapsed += (ts, te) =>
            {
                if (!cancelRemindTimeToRelax)
                {
                    ShowWorkIcon();
                    timer.Start();
                }
                else
                {
                    timer.Dispose();
                }
            };
            timer.Start();
        }

        /// <summary>
        /// Отображение иконки во время зарядки (отображение окна)
        /// </summary>
        public void ShowRelaxIcon()
        {
            icon.BalloonTipTitle = "Передышка для тебя";
            icon.BalloonTipText = "Пора передохнуть пару минут...";
            ShowBallonToolTip();
        }

        /// <summary>
        /// Отображение иконки во время откладывания зарядки
        /// </summary>
        public void ShowDelayIcon()
        {
            icon.BalloonTipTitle = "Отдых пока откладывается...";
            icon.BalloonTipText = "А зачем?";
            ShowBallonToolTip();
        }

        /// <summary>
        /// Отображение иконки во время работы (после зарядки и сворачивания окна)
        /// </summary>
        public void ShowWorkIcon()
        {
            var timeToRelax = timeToRelaxProvider.GetTimeToRelax().TotalMinutes.ToString("F0");

            icon.BalloonTipTitle = "И снова - работаем...";
            icon.BalloonTipText = "Запасись терпением...\r\nИли почитай чего-нибудь умное...\r\nСледующая передышка будет через " + timeToRelax + " мин.";
            ShowBallonToolTip();
        }

        private void ShowBallonToolTip()
        {
            icon.ShowBalloonTip(2000);
        }
    }

    #region Fake UI container
    public class FakeContainer : IContainer
    {
        ComponentCollection components;

        public FakeContainer()
        {
            components = new ComponentCollection(new IComponent[] { });
        }

        public void Add(IComponent component, string name) { }

        public void Add(IComponent component) { }

        public ComponentCollection Components
        {
            get
            {
                return components;
            }
        }

        public void Remove(IComponent component) { }

        public void Dispose() { }
    } 
    #endregion
}

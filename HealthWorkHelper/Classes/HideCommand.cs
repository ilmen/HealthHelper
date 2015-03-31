using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace HealthWorkHelper.Classes
{
    /// <summary>
    /// Команда для сворачивания формы.
    /// </summary>
    /// <remarks>
    /// на модель для:
    /// 1. Использует свойство ShowTime модели для сравнивания времени появления с текущим.
    ///     Для активизации кнопки после истичения определенного времени.
    /// 2. Подписывает на событие ShowTimeChanged модели.
    ///     Для гарантированного значения IsEnabled = false на первых миллисекундах отображения формы, пока не сработал таймер.
    /// 3. Использует таймер для проверки значения IsEnabled.
    ///     Так как стандартный механизм оповещения через CommandManager.RequerySuggested, как в DelegateCommand,
    ///     не работает если форма неактивна и не перерисовывается, а это - стандартное состояние формы оповещения.
    /// </remarks>
    public class HideCommand : ICommand
    {
        private bool lastCanExecute = false;
        private MainWindowViewModel model;
        private DispatcherTimer tmr;

        private void CheckCanExecute()
        {
            var canExecute = CanExecute(null);
            if (canExecute != lastCanExecute)
            {
                lastCanExecute = canExecute;
                CanExecuteChanged(this, new EventArgs());
            }
        }

        public HideCommand(MainWindowViewModel model)
        {
            this.model = model;
            // TODO: расскомментить или избавиться от класса
            //this.model.ShowTimeChanged += (s, e) => CheckCanExecute();

            #region Initialize timer
            this.tmr = new DispatcherTimer();
            this.tmr.Interval = TimeSpan.FromMilliseconds(200);
            this.tmr.Tick += (s, e) =>
                {
                    CheckCanExecute();
                };
            this.tmr.Start();
            #endregion
        }

        public event EventHandler CanExecuteChanged = delegate { };

        public bool CanExecute(object parameter)
        {
            //return DateTime.Now >= model.ShowTime.AddSeconds(MainWindowViewModel.RestSecondsDuration);
            // TODO: расскомментить
            return true;
        }

        public void Execute(object parameter)
        {
            var window = parameter as MainWindow;
            if (window == null) return;

            window.Hide();
        }
    }
}

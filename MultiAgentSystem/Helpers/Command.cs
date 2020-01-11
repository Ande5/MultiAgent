using System;
using System.Windows.Input;

namespace MultiAgentSystem.Helpers
{
    public class Command : ICommand
    {
        private readonly Func<object, bool> _canExecute;
        private readonly Action<object> _execute;

        public Command(Action<object> execute) 
            => _execute = execute ?? throw new ArgumentNullException(nameof(execute));

        public Command(Action execute): this(o => execute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
        }

        public Command(Action<object> execute, Func<object, bool> canExecute): this(execute) 
            => _canExecute = canExecute ?? throw new ArgumentNullException(nameof(canExecute));

        public Command(Action execute, Func<bool> canExecute)
          : this(o => execute(), o => canExecute())
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));
            if (canExecute == null)
                throw new ArgumentNullException(nameof(canExecute));
        }

        /// <param name="parameter">
        /// <see cref="T:System.Object" />, используемый в качестве параметра, чтобы определить, можно ли выполнить команду.</param>
        /// <summary>Возвращает <see cref="T:System.Boolean" />, указывающее, можно ли выполнить команду с заданным параметром.</summary>
        /// <returns>Значение <see langword="true" />, если команда может быть выполнена; в противном случае — значение <see langword="false" />.</returns>
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);
        

        /// <summary>Происходит, когда целевой объект команды должен оценить, можно ли выполнить команду.</summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
               // WeakEventManager.AddEventHandler(value, "CanExecuteChanged");
               
            }
            remove
            {
               // WeakEventManager.RemoveEventHandler(value, "CanExecuteChanged");
            }
        }

        /// <param name="parameter">
        /// <see cref="T:System.Object" />, используемый в качестве параметра для выполнения действия.</param>
        /// <summary>Вызывает выполнение действия</summary>
        public void Execute(object parameter) => _execute(parameter);
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace MultiAgentSystem.ViewModels
{
    public class BaseAgentViewModel: INotifyPropertyChanged
    {
        //private readonly Timer _timer;

        //public BaseAgentViewModel()
        //{
        //    //_timer = new Timer(Reflection, 0, 0, 50);
        //}

        //protected virtual void Reflection(object obj){ }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string prop = "") 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        protected bool SetProperty<T>(ref T field, T value,
            [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

    }
}

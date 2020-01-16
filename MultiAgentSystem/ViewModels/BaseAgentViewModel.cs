using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MultiAgentSystem.ViewModels
{
    public class BaseAgentViewModel: INotifyPropertyChanged
    {
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

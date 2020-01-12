using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MultiAgentSystem.Helpers;

namespace MultiAgentSystem.ViewModels
{
    public class MapsAgentViewModel : BaseAgentViewModel
    {
        private int _size;

        public int Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        private int _gridX;

        public int GridX
        {
            get => _gridX;
            set => SetProperty(ref _gridX, value);
        }

        private int _gridY;

        public int GridY
        {
            get => _gridY;
            set => SetProperty(ref _gridY, value);
        }

       
        public ICommand ApplyCommand { get; }

        public MapsAgentViewModel()
        {
            ApplyCommand = new Command(Apply);
        }

        private void Apply()
        {

        }


        protected override void Reflection(object obj)
        {
            //TODO пересовка карты 
        }
    }
}

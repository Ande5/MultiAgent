using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MultiAgentSystem.Helpers;
using MultiAgentSystem.ServiceManager;

namespace MultiAgentSystem.ViewModels
{
    public class MapsAgentViewModel : BaseAgentViewModel
    {
        public int[,] MapDepths { get; }
        private readonly FileManager _fileManager;

        private int _size;

        public int Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public ICommand ApplyCommand { get; }

        public MapsAgentViewModel()
        {
            ApplyCommand = new Command(Apply);
            _fileManager = new FileManager("map.txt");
            MapDepths = _fileManager.LoadMap();
          
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

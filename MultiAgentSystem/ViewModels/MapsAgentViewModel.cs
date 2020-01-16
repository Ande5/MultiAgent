using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MultiAgentSystem.Helpers;
using MultiAgentSystem.Model;
using MultiAgentSystem.ServiceManager;
using MultiAgentSystem.Views;

namespace MultiAgentSystem.ViewModels
{
    public class MapsAgentViewModel : BaseAgentViewModel
    {

        public readonly List<ShipAgent> ShipAgents;

        public readonly List<TargetAgent> TargetAgents;

        public int[,] MapDepths { get; }
        private readonly FileManager _fileManager;
        private readonly GenerationAgents _generationAgents;

        private int _size;

        public int Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }

        public Action<object> OnSelect { get; set; }

        public ICommand ApplyCommand { get; }

        public ShipAgentViewModel ShipAgentViewModels;

        public MapsAgentViewModel()
        {
            ApplyCommand = new Command(Apply);
            _fileManager = new FileManager("map.txt");
            MapDepths = _fileManager.LoadMap();

            _generationAgents = new GenerationAgents {Count = 3 };

            TargetAgents = _generationAgents.GenTargetAgents(MapDepths.GetLength(0), 
                MapDepths.GetLength(1)).Distinct().ToList();

            ShipAgents = _generationAgents.GenerationShips(MapDepths.GetLength(0), 
                MapDepths.GetLength(1), TargetAgents).Distinct().ToList();

            ShipAgentViewModels = new ShipAgentViewModel(MapDepths, ShipAgents);
        }

        private void Apply()
        {

        }

        
        protected override void Reflection(object obj)
        {
            //TODO пересовка карты 
            //OnSelect?.BeginInvoke;
            //OnSelect?.Invoke(obj);
        }
    }
}

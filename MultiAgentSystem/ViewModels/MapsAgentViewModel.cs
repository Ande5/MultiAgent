using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MultiAgentSystem.Helpers;
using MultiAgentSystem.Model;
using MultiAgentSystem.ServiceManager;

namespace MultiAgentSystem.ViewModels
{
    public class MapsAgentViewModel : BaseAgentViewModel
    {
        public List<TargetAgent> TargetAgents;
        public List<ShipAgent> ShipAgents { get; set; } = new List<ShipAgent>();

        public int[,] MapDepths { get; }
        private readonly FileManager _fileManager;
        private GenerationAgents _generationAgents;
        

        private int _size = 45;

        public int Size
        {
            get => _size;
            set => SetProperty(ref _size, value);
        }


        private int _countAgent = 7;

        public int CountAgent
        {
            get => _countAgent;
            set => SetProperty(ref _countAgent, value);
        }


        public ICommand ApplyCommand { get; }

        public ShipAgentViewModel ShipAgentViewModels;

        public MapsAgentViewModel()
        {
            ApplyCommand = new Command(Apply);
            _fileManager = new FileManager("map.txt");
            MapDepths = _fileManager.LoadMap();
            Initialization();
        }

        private void Initialization()
        {
            _generationAgents = new GenerationAgents(MapDepths) { Count = CountAgent };

            TargetAgents = _generationAgents.GenTargetAgents(MapDepths.GetLength(0),
                MapDepths.GetLength(1)).Distinct().ToList();

            ShipAgents = _generationAgents.GenerationShips(MapDepths.GetLength(0),
                MapDepths.GetLength(1), TargetAgents).Distinct().ToList();

            ShipAgentViewModels = new ShipAgentViewModel(MapDepths, ShipAgents);
        }

        public void Apply() => Initialization();
      

        public void Reflection()
        {
            ShipAgentViewModels.Reflection(TargetAgents);
            ShipAgents = ShipAgentViewModels.ShipList;
        }
    }
}

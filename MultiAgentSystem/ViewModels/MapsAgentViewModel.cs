using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MultiAgentSystem.Helpers;
using MultiAgentSystem.Model;
using MultiAgentSystem.ServiceManager;

namespace MultiAgentSystem.ViewModels
{
    public class MapsAgentViewModel : BaseAgentViewModel
    {

        public readonly List<ShipAgent> ShipAgents;

        public readonly List<TargetAgent> TargetAgents;

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

            ShipAgents = GenerationAgents
                .GenerationShips(MapDepths.GetLength(0)
                    , MapDepths.GetLength(1)).Distinct().ToList();

            TargetAgents = GenerationAgents
                .GenTargetAgents(MapDepths.GetLength(0)
                    , MapDepths.GetLength(1)).Distinct().ToList();
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

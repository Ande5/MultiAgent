using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetworkLib;

namespace MultiAgentSystem.ViewModels
{
    public class ShipAgentViewModel: BaseAgentViewModel
    {
        private readonly ServiceNN _serviceNetwork = new ServiceNN(100000);

        public ShipAgentViewModel()
        {

        }

        protected override void Reflection(object obj)
        {
            //ToDO Получить значение глубины
           var result =  _serviceNetwork.Handle(new double[] { });
        }
    }
}

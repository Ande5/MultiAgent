using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.Model;
using NeuralNetworkLib;

namespace MultiAgentSystem.ViewModels
{
    public class ShipAgentViewModel : BaseAgentViewModel
    {
        private readonly ServiceNN _serviceNetwork = new ServiceNN(100000);
        private List<ShipAgent> _shipList = new List<ShipAgent>();
        private int[,] _depthsMap;

        private ShipAgentViewModel() { }

        public ShipAgentViewModel(int[,] depthsMap)
        {
            _depthsMap = depthsMap;
        }

        protected override void Reflection(object obj)
        {
            for(int i = 0; i < _shipList.Count; i++)
            {
                // 1. Получение ответа от нейросети о следующем шаге:
                var result = _serviceNetwork.Handle(GetSurroundingDepths(_shipList[i].Location, _shipList[i].MoveDirection));

                // 2. Перерисовка положения агента:


                // 3. Перерисовка направления движения:


                // 4. Обработка смерти корабля, при совпадении ячеек:

            }
        }

        private double[] GetSurroundingDepths(Position shipPosition, Direction moveDirection)
        {
            double[] depths = new double[3];

            // TODO: Еще сопоставить порядок элементов массива с направлением относительно корабля!
            // TODO: Добавить обработку (try-catch) краев карты

            switch(moveDirection)
            {
                case Direction.N:
                default:
                    depths[0] = _depthsMap[shipPosition.Y - 1, shipPosition.X - 1];
                    depths[1] = _depthsMap[shipPosition.Y - 1, shipPosition.X];
                    depths[2] = _depthsMap[shipPosition.Y - 1, shipPosition.X + 1];
                    break;
                case Direction.NW:
                    depths[0] = _depthsMap[shipPosition.Y, shipPosition.X - 1];
                    depths[1] = _depthsMap[shipPosition.Y - 1, shipPosition.X - 1];
                    depths[2] = _depthsMap[shipPosition.Y - 1, shipPosition.X];
                    break;
                case Direction.W:
                    depths[0] = _depthsMap[shipPosition.Y - 1, shipPosition.X - 1];
                    depths[1] = _depthsMap[shipPosition.Y, shipPosition.X - 1];
                    depths[2] = _depthsMap[shipPosition.Y + 1, shipPosition.X - 1];
                    break;
                case Direction.SW:
                    depths[0] = _depthsMap[shipPosition.Y, shipPosition.X - 1];
                    depths[1] = _depthsMap[shipPosition.Y + 1, shipPosition.X - 1];
                    depths[2] = _depthsMap[shipPosition.Y + 1, shipPosition.X];
                    break;
                case Direction.S:
                    depths[0] = _depthsMap[shipPosition.Y + 1, shipPosition.X - 1];
                    depths[1] = _depthsMap[shipPosition.Y + 1, shipPosition.X];
                    depths[2] = _depthsMap[shipPosition.Y + 1, shipPosition.X + 1];
                    break;
                case Direction.SE:
                    depths[0] = _depthsMap[shipPosition.Y + 1, shipPosition.X];
                    depths[1] = _depthsMap[shipPosition.Y + 1, shipPosition.X + 1];
                    depths[2] = _depthsMap[shipPosition.Y, shipPosition.X + 1];
                    break;
                case Direction.E:
                    depths[0] = _depthsMap[shipPosition.Y - 1, shipPosition.X + 1];
                    depths[1] = _depthsMap[shipPosition.Y, shipPosition.X + 1];
                    depths[2] = _depthsMap[shipPosition.Y + 1, shipPosition.X + 1];
                    break;
                case Direction.NE:
                    depths[0] = _depthsMap[shipPosition.Y - 1, shipPosition.X];
                    depths[1] = _depthsMap[shipPosition.Y - 1, shipPosition.X + 1];
                    depths[2] = _depthsMap[shipPosition.Y, shipPosition.X + 1];
                    break;
            }

            return depths;
        }
    }
}

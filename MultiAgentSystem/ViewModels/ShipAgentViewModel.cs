using System.Collections.Generic;
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

            switch(moveDirection)
            {
                case Direction.N:
                default:
                    try { depths[0] = _depthsMap[shipPosition.Y - 1, shipPosition.X - 1]; } catch { depths[0] = -50; }
                    try { depths[1] = _depthsMap[shipPosition.Y - 1, shipPosition.X]; } catch { depths[1] = -50; }
                    try { depths[2] = _depthsMap[shipPosition.Y - 1, shipPosition.X + 1]; } catch { depths[2] = -50; }
                    break;
                case Direction.NW:
                    try { depths[0] = _depthsMap[shipPosition.Y, shipPosition.X - 1]; } catch { depths[0] = -50; }
                    try { depths[1] = _depthsMap[shipPosition.Y - 1, shipPosition.X - 1]; } catch { depths[1] = -50; }
                    try { depths[2] = _depthsMap[shipPosition.Y - 1, shipPosition.X]; } catch { depths[2] = -50; }
                    break;
                case Direction.W:
                    try { depths[0] = _depthsMap[shipPosition.Y + 1, shipPosition.X - 1]; } catch { depths[0] = -50; }
                    try { depths[1] = _depthsMap[shipPosition.Y, shipPosition.X - 1]; } catch { depths[1] = -50; }
                    try { depths[2] = _depthsMap[shipPosition.Y - 1, shipPosition.X - 1]; } catch { depths[2] = -50; }
                    break;
                case Direction.SW:
                    try { depths[0] = _depthsMap[shipPosition.Y + 1, shipPosition.X]; } catch { depths[0] = -50; }
                    try { depths[1] = _depthsMap[shipPosition.Y + 1, shipPosition.X - 1]; } catch { depths[1] = -50; }
                    try { depths[2] = _depthsMap[shipPosition.Y, shipPosition.X - 1]; } catch { depths[2] = -50; }
                    break;
                case Direction.S:
                    try { depths[0] = _depthsMap[shipPosition.Y + 1, shipPosition.X + 1]; } catch { depths[0] = -50; }
                    try { depths[1] = _depthsMap[shipPosition.Y + 1, shipPosition.X]; } catch { depths[1] = -50; }
                    try { depths[2] = _depthsMap[shipPosition.Y + 1, shipPosition.X - 1]; } catch { depths[2] = -50; }
                    break;
                case Direction.SE:
                    try { depths[0] = _depthsMap[shipPosition.Y, shipPosition.X + 1]; } catch { depths[0] = -50; }
                    try { depths[1] = _depthsMap[shipPosition.Y + 1, shipPosition.X + 1]; } catch { depths[1] = -50; }
                    try { depths[2] = _depthsMap[shipPosition.Y + 1, shipPosition.X]; } catch { depths[2] = -50; }
                    break;
                case Direction.E:
                    try { depths[0] = _depthsMap[shipPosition.Y - 1, shipPosition.X + 1]; } catch { depths[0] = -50; }
                    try { depths[1] = _depthsMap[shipPosition.Y, shipPosition.X + 1]; } catch { depths[1] = -50; }
                    try { depths[2] = _depthsMap[shipPosition.Y + 1, shipPosition.X + 1]; } catch { depths[2] = -50; }
                    break;
                case Direction.NE:
                    try { depths[0] = _depthsMap[shipPosition.Y - 1, shipPosition.X]; } catch { depths[0] = -50; }
                    try { depths[1] = _depthsMap[shipPosition.Y - 1, shipPosition.X + 1]; } catch { depths[1] = -50; }
                    try { depths[2] = _depthsMap[shipPosition.Y, shipPosition.X + 1]; } catch { depths[2] = -50; }
                    break;
            }

            return depths;
        }
    }
}

using System.Collections.Generic;
using MultiAgentSystem.Model;
using MultiAgentSystem.ServiceManager;
using NeuralNetworkLib;

namespace MultiAgentSystem.ViewModels
{
    public class ShipAgentViewModel : BaseAgentViewModel
    {
        private readonly ServiceNN _serviceNetwork = new ServiceNN(100000);
        public List<ShipAgent> ShipList { get; }
        private int[,] _depthsMap;

        private DirectionManager _directionManager;

        private ShipAgentViewModel() { }

        public ShipAgentViewModel(int[,] depthsMap, List<ShipAgent> shipAgents)
        {
            _depthsMap = depthsMap;
            ShipList = shipAgents;

            _directionManager = new DirectionManager();
        }

        public void Reflection(List<TargetAgent> targetList)
        {
            for (int i = 0; i < ShipList.Count; i++)
            {
                ShipList[i].CurrentAwaitIteration--;

                if (ShipList[i].CurrentAwaitIteration == 0 && ShipList[i].Speed != 0)
                {
                    // 1. Получение ответа от нейросети о следующем шаге:
                    var result = _serviceNetwork.Handle(GetSurroundingDepths(ShipList[i].Location, ShipList[i].DirectionToTarget));

                    // 2. Обработка шага в клетку:
                    SetLocationByStep(result, ShipList[i]);

                    // 3. Обновление текущего направления движения:
                    ShipList[i].MoveDirection = _directionManager.UpdateDirectionAfterStep(ShipList[i].Location, ShipList[i].PrevPosition);

                    // 4. Обновление направления движения к цели:
                    ShipList[i].DirectionToTarget = _directionManager.InitializeDirection(ShipList[i].Location, targetList[i].Location);

                    ShipList[i].CurrentAwaitIteration = 10 - ShipList[i].Speed;
                }
            }

            // 5. Обработка столкновения корабля:
            HandleShipCrushing(targetList);

            // 5. Обработка кораблей, севших на мель:
            HandleStoppedShips();

            // 6. Обработка нахождения цели:
            HandleSuccessfulShips(targetList);
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

        private void SetLocationByStep(double[] netResult, ShipAgent ship)
        {
            ship.PrevPosition = new Position() { X = ship.Location.X, Y = ship.Location.Y };

            int maxNetResultIndex = GetMaxResultIndex(netResult);

            switch(ship.DirectionToTarget)
            {
                case Direction.NW:
                    if (maxNetResultIndex == 0) { ship.Location.X -= 1; }
                    if (maxNetResultIndex == 1) { ship.Location.X -= 1; ship.Location.Y -= 1; }
                    if (maxNetResultIndex == 2) { ship.Location.Y -= 1; }
                    break;
                case Direction.W:
                    if (maxNetResultIndex == 0) { ship.Location.X -= 1; ship.Location.Y += 1; }
                    if (maxNetResultIndex == 1) { ship.Location.X -= 1; }
                    if (maxNetResultIndex == 2) { ship.Location.X -= 1; ship.Location.Y -= 1; }
                    break;
                case Direction.SW:
                    if (maxNetResultIndex == 0) { ship.Location.Y += 1; }
                    if (maxNetResultIndex == 1) { ship.Location.X -= 1; ship.Location.Y += 1; }
                    if (maxNetResultIndex == 2) { ship.Location.X -= 1; }
                    break;
                case Direction.S:
                    if (maxNetResultIndex == 0) { ship.Location.X += 1; ship.Location.Y += 1; }
                    if (maxNetResultIndex == 1) { ship.Location.Y += 1; }
                    if (maxNetResultIndex == 2) { ship.Location.X -= 1; ship.Location.Y += 1; }
                    break;
                case Direction.SE:
                    if (maxNetResultIndex == 0) { ship.Location.X += 1; }
                    if (maxNetResultIndex == 1) { ship.Location.X += 1; ship.Location.Y += 1; }
                    if (maxNetResultIndex == 2) { ship.Location.Y += 1; }
                    break;
                case Direction.E:
                    if (maxNetResultIndex == 0) { ship.Location.X += 1; ship.Location.Y -= 1; }
                    if (maxNetResultIndex == 1) { ship.Location.X += 1; }
                    if (maxNetResultIndex == 2) { ship.Location.X += 1; ship.Location.Y += 1; }
                    break;
                case Direction.NE:
                    if (maxNetResultIndex == 0) { ship.Location.Y -= 1; }
                    if (maxNetResultIndex == 1) { ship.Location.X += 1; ship.Location.Y -= 1; }
                    if (maxNetResultIndex == 2) { ship.Location.X += 1; }
                    break;
                case Direction.N:
                default:
                    if (maxNetResultIndex == 0) { ship.Location.X -= 1; ship.Location.Y -= 1; }
                    if (maxNetResultIndex == 1) { ship.Location.Y -= 1; }
                    if (maxNetResultIndex == 2) { ship.Location.X += 1; ship.Location.Y -= 1; }
                    break;
            }

            if(_depthsMap.GetLength(0) <= ship.Location.Y ||
               _depthsMap.GetLength(1) <= ship.Location.X ||
               ship.Location.X < 0 || ship.Location.Y < 0)
            {
                ship.Location = new Position() { X = ship.PrevPosition.X, Y = ship.PrevPosition.Y };
            }
        }

        private int GetMaxResultIndex(double[] result)
        {
            int maxIndex = 0;
            double maxValue = result[maxIndex];

            for(int i = 0; i < result.Length; i++)
            {
                if(result[i] > maxValue)
                {
                    maxValue = result[i];
                    maxIndex = i;
                }
            }

            return maxIndex;
        }

        private void HandleShipCrushing(List<TargetAgent> targetList)
        {
            for (int k = 0; k < ShipList.Count; k++)
            {
                for (int j = 0; j < ShipList.Count; j++)
                {
                    if (ShipList[k].Location.X == ShipList[j].Location.X &&
                        ShipList[k].Location.Y == ShipList[j].Location.Y && k != j)
                    {
                        ShipList.Remove(ShipList[k]);
                        targetList.RemoveAt(k);

                        k--;
                    }
                }
            }
        }

        private void HandleStoppedShips()
        {
            for (int k = 0; k < ShipList.Count; k++)
            {
                for (int j = 0; j < ShipList.Count; j++)
                {
                    if (_depthsMap[ShipList[k].Location.Y, ShipList[k].Location.X] < 0)
                    {
                        ShipList[k].Speed = 0;
                    }
                }
            }
        }

        private void HandleSuccessfulShips(List<TargetAgent> targetList)
        {
            for (int i = 0; i < ShipList.Count; i++)
            {
                for (int k = 0; k < ShipList.Count; k++)
                {
                    if (ShipList[i].Location.X == targetList[k].Location.X &&
                       ShipList[i].Location.Y == targetList[k].Location.Y)
                    {
                        ShipList[i].Speed = 0;
                    }
                }
            }
        }
    }
}

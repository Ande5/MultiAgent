﻿using System.Collections.Generic;
using MultiAgentSystem.Model;
using MultiAgentSystem.ServiceManager;
using NeuralNetworkLib;

namespace MultiAgentSystem.ViewModels
{
    public class ShipAgentViewModel : BaseAgentViewModel
    {
        private readonly ServiceNN _serviceNetwork = new ServiceNN(100000);
        private List<ShipAgent> _shipList = new List<ShipAgent>();
        private int[,] _depthsMap;

        private DirectionManager _directionManager;

        private ShipAgentViewModel() { }

        public ShipAgentViewModel(int[,] depthsMap, List<ShipAgent> shipAgents)
        {
            _depthsMap = depthsMap;
            _shipList = shipAgents;

            _directionManager = new DirectionManager();
        }

        protected override void Reflection(object obj)
        {
            for (int i = 0; i < _shipList.Count; i++)
            {
                _shipList[i].CurrentAwaitIteration--;

                if (_shipList[i].CurrentAwaitIteration == 0)
                {
                    // 1. Получение ответа от нейросети о следующем шаге:
                    var result = _serviceNetwork.Handle(GetSurroundingDepths(_shipList[i].Location, _shipList[i].MoveDirection));

                    // 2. Обработка шага в клетку:
                    SetLocationByStep(result, _shipList[i]);

                    // 3. Обновление направления движения:
                    _shipList[i].MoveDirection = _directionManager.UpdateDirectionAfterStep(_shipList[i].Location, _shipList[i].PrevPosition);
                }

                _shipList[i].CurrentAwaitIteration = 10 - _shipList[i].Speed;
            }

            // 4. Обработка смерти корабля:
            //try
            //{
                for (int k = 0; k < _shipList.Count; k++)
                {
                    for (int j = 0; j < _shipList.Count; j++)
                    {
                        if (_shipList[k].Location.X == _shipList[j].Location.X &&
                            _shipList[k].Location.Y == _shipList[j].Location.Y)
                        {
                            _shipList.Remove(_shipList[k]);
                            k--;
                        }
                    }
                }
            //}
            //catch { }
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
            ship.PrevPosition = ship.Location;

            int maxNetResultIndex = GetMaxResultIndex(netResult);

            switch(ship.MoveDirection)
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
    }
}

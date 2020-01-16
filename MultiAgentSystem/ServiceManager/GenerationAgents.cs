using System;
using System.Collections.Generic;
using MultiAgentSystem.Model;

namespace MultiAgentSystem.ServiceManager
{
    public class GenerationAgents
    {
        public int Count { get; set; }

        private readonly int[,] _mapDepths;

        public GenerationAgents(int[,] mapDepths) => _mapDepths = mapDepths;

        public List<ShipAgent> GenerationShips(int gridX, int gridY, List<TargetAgent> targetAgents)
        {
            var shipAgents = new List<ShipAgent>();

            var directionManager = new DirectionManager();

            var random = new Random(DateTime.Now.Millisecond);
           
            for (int i = 0; i < Count; i++)
            {
                var shipAgent = new ShipAgent
                {
                    Speed = random.Next(8, 9),
                    Draft = random.Next(5, 35),
                    MaxSpeed = random.Next(9, 10),
                  
                    Location = new Position
                    {
                        X = random.Next(1, gridX-1),
                        Y = random.Next(1, gridY)
                    }
                };

                shipAgent.MoveDirection =
                    directionManager.InitializeDirection(shipAgent.Location, targetAgents[i].Location);
                shipAgent.CurrentAwaitIteration = 10 - shipAgent.Speed;

                CheckLocationShip(gridX, gridY, ref shipAgent);
                CheckLocationShipByTarget(gridX, gridY, targetAgents, directionManager, ref shipAgent);
                shipAgents.Add(shipAgent);
            }

            return shipAgents;
        }

        /// <summary>
        /// Генерация целей
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns></returns>
        public List<TargetAgent> GenTargetAgents(int gridX, int gridY)
        {
            var targetAgents = new List<TargetAgent>();

            var random = new Random(DateTime.Now.Millisecond);

            for (int z = 0; z < Count; z++)
            {
                var targetAgent = new TargetAgent
                {
                    Location = new Position
                    {
                        X = random.Next(1, gridX - 1),
                        Y = random.Next(1, gridY)
                    }
                };

                CheckLocationTarget(gridX, gridY, ref targetAgent);
              
                targetAgents.Add(targetAgent);
            }

            return targetAgents;
        }

        private void CheckLocationTarget(int gridX, int gridY, ref TargetAgent targetAgent)
        {
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < _mapDepths.GetLength(0); i++)
            {
                for (int j = 0; j < _mapDepths.GetLength(1); j++)
                {
                    if (_mapDepths[i, j] < 0
                        && targetAgent.Location.X == i && targetAgent.Location.Y == j)
                    {
                        targetAgent.Location.X = random.Next(1, gridX - 1);
                        targetAgent.Location.Y = random.Next(1, gridY);
                        CheckLocationTarget(gridX, gridY, ref targetAgent);
                    }
                }
            }
        }


        private void CheckLocationShip(int gridX, int gridY, ref ShipAgent shipAgent)
        {
            var random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < _mapDepths.GetLength(0); i++)
            {
                for (int j = 0; j < _mapDepths.GetLength(1); j++)
                {
                    if (_mapDepths[i, j] < 0
                        && shipAgent.Location.X == i && shipAgent.Location.Y == j)
                    {
                        shipAgent.Location.X = random.Next(1, gridX - 1);
                        shipAgent.Location.Y = random.Next(1, gridY);
                        CheckLocationShip(gridX, gridY, ref shipAgent);
                    }
                }
            }
        }


        private void CheckLocationShipByTarget(int gridX, int gridY,
            List<TargetAgent> targetAgents, DirectionManager directionManager, ref ShipAgent shipAgent)
        {
            var random = new Random(DateTime.Now.Millisecond);
            int count = 0;
            
            foreach (var targetAgent in targetAgents)
            {
                if (targetAgent.Location.X == shipAgent.Location.X &&
                    targetAgent.Location.Y == shipAgent.Location.Y)
                {
                    shipAgent.Location.X = random.Next(1, gridX - 1);
                    shipAgent.Location.Y = random.Next(1, gridX - 1);
                    shipAgent.MoveDirection =
                        directionManager.InitializeDirection(shipAgent.Location, targetAgents[count].Location);

                    CheckLocationShipByTarget(gridX, gridY, targetAgents, directionManager, ref shipAgent);
                }

                count++;
            }
        }

    }
}

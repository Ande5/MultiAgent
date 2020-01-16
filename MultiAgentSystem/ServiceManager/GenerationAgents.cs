using System;
using System.Collections.Generic;
using MultiAgentSystem.Model;

namespace MultiAgentSystem.ServiceManager
{
    public class GenerationAgents
    {

        public int Count { get; set; }

        public List<ShipAgent> GenerationShips(int gridX, int gridY, List<TargetAgent> targetAgents)
        {
            var shipAgents = new List<ShipAgent>();

            var directionManager = new DirectionManager();

            var random = new Random(DateTime.Now.Millisecond);
           
            for (int i = 0; i < Count; i++)
            {
                var shipAgent = new ShipAgent
                {
                    Speed = random.Next(1, 9),
                    Draft = random.Next(5, 35),
                    MaxSpeed = random.Next(9, 10),
                  
                    Location = new Position
                    {
                        X = random.Next(0, gridX),
                        Y = random.Next(0, gridY)
                    }
                };

                shipAgent.MoveDirection =
                    directionManager.InitializeDirection(shipAgent.Location, targetAgents[i].Location);
                shipAgent.CurrentAwaitIteration = 10 - shipAgent.Speed;

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
            
            for (int i = 0; i < Count; i++)
            {
                targetAgents.Add(new TargetAgent
                {
                    Location = new Position
                    {
                        X = random.Next(0, gridX),
                        Y = random.Next(0, gridY)
                    }
                });
            }

            return targetAgents;
        }

    }
}

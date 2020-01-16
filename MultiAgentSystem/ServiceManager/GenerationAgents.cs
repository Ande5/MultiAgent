using System;
using System.Collections.Generic;
using MultiAgentSystem.Model;

namespace MultiAgentSystem.ServiceManager
{
    public static class GenerationAgents
    {
        public static List<ShipAgent> GenerationShips(int gridX, int gridY)
        {
            var shipAgents = new List<ShipAgent>();

            var random = new Random(DateTime.Now.Millisecond);
            var shipCount =  random.Next(3, 8);
            for (int i = 0; i < shipCount; i++)
            {
                shipAgents.Add(new ShipAgent
                {
                    Speed = random.Next(20,50),
                    Draft = random.Next(5,35),
                    MaxSpeed = random.Next(40,50),
                    Location = new Position
                    {
                        X = random.Next(0, gridX),
                        Y = random.Next(0, gridY)
                    }
                });
            }

            return shipAgents;
        }

        /// <summary>
        /// Генерация целей
        /// </summary>
        /// <param name="gridX"></param>
        /// <param name="gridY"></param>
        /// <returns></returns>
        public static List<TargetAgent> GenTargetAgents(int gridX, int gridY)
        {
            var targetAgents = new List<TargetAgent>();

            var random = new Random(DateTime.Now.Millisecond);
            var targetCount = random.Next(3, 8);
            for (int i = 0; i < targetCount; i++)
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

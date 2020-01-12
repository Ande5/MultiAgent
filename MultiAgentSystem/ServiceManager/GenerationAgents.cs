using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using MultiAgentSystem.Model;

namespace MultiAgentSystem.ServiceManager
{
    public static class GenerationAgents
    {

        public static List<ShipAgent> GenerationShips()
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
                    MaxSpeed = random.Next(40,50)
                });
            }

            return shipAgents;
        }
    }
}

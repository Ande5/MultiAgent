using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiAgentSystem.Model
{
    public class ShipAgent : Agent
    {
        /// <summary>
        /// Скорость хода
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public int MaxSpeed { get; set; }

        /// <summary>
        /// Осадка
        /// </summary>
        public double Draft { get; set; }

        /// <summary>
        /// Направление движения
        /// </summary>
        public Direction MoveDirection { get; set; }
    }
}

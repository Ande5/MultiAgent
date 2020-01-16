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
        public double Speed { get; set; }

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public double MaxSpeed { get; set; }

        /// <summary>
        /// Осадка
        /// </summary>
        public double Draft { get; set; }

        /// <summary>
        /// Направление движения
        /// </summary>
        public Direction MoveDirection { get; set; }

        /// <summary>
        /// Предыдущая позиция
        /// </summary>
        public Position PrevPosition { get; set; }
    }
}

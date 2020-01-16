using System;

namespace MultiAgentSystem.Model
{
    public class Agent
    {
        /// <summary>
        /// Время нахождения в ячейке
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Позиция на нахождения агента
        /// </summary>
        public Position Location { get; set; }
    }
}

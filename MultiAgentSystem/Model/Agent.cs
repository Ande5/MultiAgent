using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        protected virtual void Reflection()
        {

        }

    }
}

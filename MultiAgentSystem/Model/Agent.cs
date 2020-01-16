using System;

namespace MultiAgentSystem.Model
{
    public class Agent: IEquatable<Agent>
    {
        /// <summary>
        /// Время нахождения в ячейке
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Позиция на нахождения агента
        /// </summary>
        public Position Location { get; set; }

        public bool Equals(Agent other)
        {

            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return Location.Equals(other.Location);
        }
    }
}

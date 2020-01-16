
namespace MultiAgentSystem.Model
{
    public class ShipAgent : Agent
    {
        /// <summary>
        /// Скорость хода
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Скорость хода
        /// </summary>
        public int CurrentAwaitIteration { get; set; }

        /// <summary>
        /// Максимальная скорость
        /// </summary>
        public int MaxSpeed { get; set; }

        /// <summary>
        /// Осадка
        /// </summary>
        public double Draft { get; set; }

        /// <summary>
        /// Текущее нправление движения
        /// </summary>
        public Direction MoveDirection { get; set; }

        /// <summary>
        /// Нправление движения по отношению к цели
        /// </summary>
        public Direction DirectionToTarget { get; set; }

        /// <summary>
        /// Предыдущая позиция
        /// </summary>
        public Position PrevPosition { get; set; }
    }
}

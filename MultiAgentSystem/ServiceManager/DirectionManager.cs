using MultiAgentSystem.Model;

namespace MultiAgentSystem.ServiceManager
{
    public class DirectionManager
    {
        public Direction InitializeDirection(Position shipPosition, Position targetPosition)
        {
            if(shipPosition.X == targetPosition.X &&
               shipPosition.Y > targetPosition.Y)
            {
                return Direction.N;
            }

            if (shipPosition.X > targetPosition.X &&
                shipPosition.Y > targetPosition.Y)
            {
                return Direction.NW;
            }

            if (shipPosition.X > targetPosition.X &&
                shipPosition.Y == targetPosition.Y)
            {
                return Direction.W;
            }

            if (shipPosition.X > targetPosition.X &&
                shipPosition.Y < targetPosition.Y)
            {
                return Direction.SW;
            }

            if (shipPosition.X == targetPosition.X &&
                shipPosition.Y < targetPosition.Y)
            {
                return Direction.S;
            }

            if (shipPosition.X < targetPosition.X &&
                shipPosition.Y < targetPosition.Y)
            {
                return Direction.SE;
            }

            if (shipPosition.X < targetPosition.X &&
                shipPosition.Y == targetPosition.Y)
            {
                return Direction.E;
            }

            if (shipPosition.X < targetPosition.X &&
                shipPosition.Y > targetPosition.Y)
            {
                return Direction.NE;
            }
            else
            {
                return Direction.N;
            }
        }

        public Direction UpdateDirectionAfterStep(Position currentPosition, Position prevPosition)
        {
            if (currentPosition.X == prevPosition.X &&
                currentPosition.Y > prevPosition.Y)
            {
                return Direction.S;
            }

            if (currentPosition.X > prevPosition.X &&
                currentPosition.Y > prevPosition.Y)
            {
                return Direction.SE;
            }

            if (currentPosition.X > prevPosition.X &&
                currentPosition.Y == prevPosition.Y)
            {
                return Direction.E;
            }

            if (currentPosition.X > prevPosition.X &&
                currentPosition.Y < prevPosition.Y)
            {
                return Direction.NE;
            }

            if (currentPosition.X == prevPosition.X &&
                currentPosition.Y < prevPosition.Y)
            {
                return Direction.N;
            }

            if (currentPosition.X < prevPosition.X &&
                currentPosition.Y < prevPosition.Y)
            {
                return Direction.NW;
            }

            if (currentPosition.X < prevPosition.X &&
                currentPosition.Y == prevPosition.Y)
            {
                return Direction.W;
            }

            if (currentPosition.X < prevPosition.X &&
                currentPosition.Y > prevPosition.Y)
            {
                return Direction.SW;
            }
            else
            {
                return Direction.N;
            }
        }
    }
}

using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public class LeftCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction direction) PreviousPosition
        { get; set; }

        public CommandType CommandType => CommandType.LEFT;

        public (byte newXAxis, byte newYAxis, Direction newDirection) Execute()
        {
            int tempDirection = (int)PreviousPosition.direction;
            tempDirection = tempDirection == 0 ? 3 : tempDirection - 1;

            return (PreviousPosition.XAxis, PreviousPosition.YAxis, (Direction)tempDirection);
        }
    }
}
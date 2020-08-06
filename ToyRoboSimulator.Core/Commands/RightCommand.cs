using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public class RightCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction direction) PreviousPosition
        { get; set; }

        public CommandType CommandType => CommandType.RIGHT;

        public (byte newXAxis, byte newYAxis, Direction newDirection) Execute()
        {
            int tempDirection = (int)PreviousPosition.direction;
            tempDirection = tempDirection == 3 ? 0 : tempDirection + 1;

            return (PreviousPosition.XAxis, PreviousPosition.YAxis, (Direction)tempDirection);
        }
    }
}
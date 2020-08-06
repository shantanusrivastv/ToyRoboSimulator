using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public class RightCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction CurrentDirection) PreviousPosition
        { get; set; }

        public CommandType CommandType => CommandType.RIGHT;

        public (byte XAxis, byte YAxis, Direction CurrentDirection) Execute()
        {
            int tempDirection = (int)PreviousPosition.CurrentDirection;
            tempDirection = tempDirection == 3 ? 0 : tempDirection + 1;

            return (PreviousPosition.XAxis, PreviousPosition.YAxis, (Direction)tempDirection);
        }
    }
}
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public class ReportCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction CurrentDirection) PreviousPosition
        { get; set; }

        public CommandType CommandType => CommandType.REPORT;

        public (byte XAxis, byte YAxis, Direction CurrentDirection) Execute()
        {
            return PreviousPosition;
        }
    }
}
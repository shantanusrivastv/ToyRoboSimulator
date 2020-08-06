using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public class ReportCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction direction) PreviousPosition
        { get; set; }

        public CommandType CommandType => CommandType.REPORT;

        public (byte newXAxis, byte newYAxis, Direction newDirection) Execute()
        {
            return PreviousPosition;
        }
    }
}
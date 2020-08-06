using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public interface ICommand
    {
        public (byte newXAxis, byte newYAxis, Direction newDirection) Execute();

        public (byte XAxis, byte YAxis, Direction direction) PreviousPosition { get; set; }

        public CommandType CommandType { get; }
    }
}
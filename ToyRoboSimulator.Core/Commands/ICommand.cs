using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public interface ICommand
    {
        public (byte XAxis, byte YAxis, Direction CurrentDirection) Execute();

        public (byte XAxis, byte YAxis, Direction CurrentDirection) PreviousPosition { get; set; }

        public CommandType CommandType { get; }
    }
}
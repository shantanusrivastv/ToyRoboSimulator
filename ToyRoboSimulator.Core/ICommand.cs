using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public interface ICommand
    {

        public (byte XAxis, byte YAxis, Direction CurrentDirection) Execute();
        public (byte XAxis, byte YAxis, Direction CurrentDirection) PreviousPosition { get; set; }

        public CommandType CommandType { get; }
    }
}
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public interface IRoboMover
    {
        (byte XAxis, byte YAxis, Direction CurrentDirection) PerformMove(string moveCommand);

        void SetPosition(string command);
    }
}
namespace ToyRoboSimulator.Core
{
    public interface ISimulator
    {
        (byte XAxis, byte YAxis, Direction CurrentDirection) MoveRobo(string moveCommand);
    }
}
namespace ToyRoboSimulator.Core.Helper
{
    public interface IValidator
    {
        bool ValidateFirstCommand(string command);

        bool ValidateInputCommand(string command);

        bool WithinValidRange(byte axisPoint);
    }
}
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public interface ICommandFactory
    {
        public ICommand CreateCommand(CommandType commandType);
    }
}
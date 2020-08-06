using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public interface ICommandFactory
    {
        public ICommand CreateCommand(CommandType commandType);
    }
}
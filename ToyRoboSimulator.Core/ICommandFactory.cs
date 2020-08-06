using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public interface ICommandFactory
    {
        public ICommand CreateCommand(CommandType commandType);
    }
}

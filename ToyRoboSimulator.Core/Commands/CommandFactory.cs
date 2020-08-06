using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private bool _hasInitialised;
        private static IEnumerable<ICommand> CommandList { get; set; }

        public ICommand CreateCommand(CommandType commandType)
        {
            if (!_hasInitialised)
            {
                var commandList = Assembly.GetExecutingAssembly().GetTypes()
                                           .Where(x => (typeof(ICommand).IsAssignableFrom(x) &&
                                                  !x.IsAbstract && !x.IsInterface))
                                            .ToList();

                //Creating a list for reuse
                CommandList = commandList.Select(x => Activator.CreateInstance(x) as ICommand).ToList();
                _hasInitialised = true;
            }

            return CommandList.Where(x => x.CommandType == commandType).SingleOrDefault();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
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
                                           .Where( x => (typeof(ICommand).IsAssignableFrom(x) &&
                                                   !x.IsAbstract && !x.IsInterface))
                                            .ToList();

                //Creating a list for resuse
                CommandList = commandList.Select(x => Activator.CreateInstance(x) as ICommand).ToList();
                _hasInitialised = true;

            }

            var command =  CommandList.Where(x => x.CommandType == commandType).SingleOrDefault();
            return command;
        }


    }
}

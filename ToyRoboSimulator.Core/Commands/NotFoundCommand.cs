using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public class NotFoundCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction direction) PreviousPosition {
            get ;
            set;
        }

        public CommandType CommandType => CommandType.NONE;

        public (byte newXAxis, byte newYAxis, Direction newDirection) Execute()
        {
            Console.WriteLine("Command Not found");
            return (default(byte), default(byte), Direction.SOUTH);
        }
    }
}

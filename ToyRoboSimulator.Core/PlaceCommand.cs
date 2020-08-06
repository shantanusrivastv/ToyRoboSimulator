using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public class PlaceCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction CurrentDirection) PreviousPosition
        { get; set; }

        public CommandType CommandType => CommandType.PLACE;

        public (byte XAxis, byte YAxis, Direction CurrentDirection) Execute()
        {
            return PreviousPosition;
        }
    }
}

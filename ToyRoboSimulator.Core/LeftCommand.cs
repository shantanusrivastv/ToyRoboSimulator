using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public class LeftCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction CurrentDirection) PreviousPosition
        { get; set; }

        public CommandType CommandType => CommandType.LEFT;

        public (byte XAxis, byte YAxis, Direction CurrentDirection) Execute()
        {
            int tempDirection = (int)PreviousPosition.CurrentDirection;
            tempDirection = tempDirection == 0 ? 3 : tempDirection - 1;

            return  (PreviousPosition.XAxis, PreviousPosition.YAxis, (Direction)tempDirection);
        }
    }
}

using System;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Commands
{
    public class MoveCommand : ICommand
    {
        public (byte XAxis, byte YAxis, Direction CurrentDirection) PreviousPosition
        {
            get;
            set;
        }

        public CommandType CommandType => CommandType.MOVE;

        public (byte XAxis, byte YAxis, Direction CurrentDirection) Execute()
        {
            switch (PreviousPosition.CurrentDirection)
            {
                case Direction.NORTH:
                    MoveUp();
                    break;

                case Direction.SOUTH:
                    MoveDown();
                    break;

                case Direction.EAST:
                    MoveRight();
                    break;

                case Direction.WEST:
                    MoveLeft();
                    break;
            }

            return PreviousPosition;
        }

        private void MoveUp()
        {
            if (PreviousPosition.YAxis < 4)
            {
                PreviousPosition = (PreviousPosition.XAxis, Convert.ToByte(PreviousPosition.YAxis + 1), PreviousPosition.CurrentDirection);
            }
        }

        private void MoveDown()
        {
            if (PreviousPosition.YAxis >= 1)
            {
                PreviousPosition = (PreviousPosition.XAxis, Convert.ToByte(PreviousPosition.YAxis - 1), PreviousPosition.CurrentDirection);
            }
        }

        private void MoveLeft()
        {
            if (PreviousPosition.XAxis >= 1)
            {
                PreviousPosition = (Convert.ToByte(PreviousPosition.XAxis - 1), PreviousPosition.YAxis, PreviousPosition.CurrentDirection);
            }
        }

        private void MoveRight()
        {
            if (PreviousPosition.XAxis < 4)
            {
                PreviousPosition = (Convert.ToByte(PreviousPosition.XAxis + 1), PreviousPosition.YAxis, PreviousPosition.CurrentDirection);
            }
        }
    }
}
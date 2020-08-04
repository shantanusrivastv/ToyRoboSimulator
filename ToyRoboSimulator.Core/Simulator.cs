using System;
using ToyRoboSimulator.Core.Helper;

namespace ToyRoboSimulator.Core
{
    public class Simulator
    {
        private readonly Validator validator;

        public (byte XAxis, byte YAxis, Direction CurrentDirection) CurrentPosition
        {
            get;
            private set;
        }

        public Simulator(string command)
        {
            validator = new Validator();
            if (validator.ValidateFirstCommand(command))
            {
                SetPosition(command);
                //continue
            }
            else
            {
                //Make Custom Exception
                throw new Exception("InValid Command");
            }
        }

        public void MoveRobo(string moveCommand)
        {
            if (validator.ValidateInputCommand(moveCommand))
            {
                Enum requestedMoveType = (MoveType)Enum.Parse(typeof(MoveType), moveCommand.Split(' ', ',')[0]);

                switch (requestedMoveType)
                {
                    case MoveType.MOVE:
                        PerformMove();
                        break;

                    default:
                        break;
                }
            }
        }

        private bool PerformMove()
        {
            switch (CurrentPosition.CurrentDirection)
            {
                case Direction.NORTH:
                    return MoveUP();

                default:
                    return false;
            }
        }

        private bool MoveUP()
        {
            if (CurrentPosition.YAxis < 4)
            {
                CurrentPosition = (CurrentPosition.XAxis, Convert.ToByte(CurrentPosition.YAxis + 1), CurrentPosition.CurrentDirection);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool SetPosition(string command)
        {
            try
            {
                string[] commandSplit = command.Split(' ', ',');
                var nextXAxis = byte.Parse(commandSplit[1]);
                var nextYAxis = byte.Parse(commandSplit[2]);
                var direction = (Direction)Enum.Parse(typeof(Direction), commandSplit[3]);
                CurrentPosition = (nextXAxis, nextYAxis, direction);
            }
            catch (Exception)
            {
                //Log Error

                return false;
            }

            return true;
        }
    }
}
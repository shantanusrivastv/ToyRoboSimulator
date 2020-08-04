using System;

namespace ToyRoboSimulator.Core.Helper
{
    public class Validator : IValidator
    {
        public bool ValidateFirstCommand(string command)
        {
            //TOdoMove to common Property to resuse it
            string[] commandSplit = command.Split(' ', ',');
            string commandType = commandSplit[0];

            if (commandType != MoveType.PLACE.ToString() && commandSplit.Length != 4)
            {
                return false;
            }
            else
            {
                return ValidateParameters(commandSplit);
            }
        }

        public bool ValidateInputCommand(string command)
        {
            string[] commandSplit = command.Split(' ', ',');
            string commandType = commandSplit[0];

            if (commandType != MoveType.PLACE.ToString() && commandSplit.Length > 1)
            {
                return false;
            }
            else if (commandType == MoveType.PLACE.ToString())
            {
                return ValidateParameters(commandSplit);
            }

            return Enum.IsDefined(typeof(MoveType), commandType);
        }

        private bool ValidateParameters(string[] commandSplit)
        {
            if (commandSplit.Length != 4)
            {
                return false;
            }
            else
            {
                return CoOrdinateValidator(commandSplit[1]) &&
                       CoOrdinateValidator(commandSplit[2]) &&
                       Enum.IsDefined(typeof(Direction), commandSplit[3]);
            }
        }

        private bool CoOrdinateValidator(string coOrdinate)
        {
            bool isValid = byte.TryParse(coOrdinate, out byte axisPoint);
            return isValid && WithinValidRange(axisPoint);
        }

        private bool WithinValidRange(byte axisPoint)
        {
            return axisPoint >= 0 && axisPoint <= 4;
        }
    }
}
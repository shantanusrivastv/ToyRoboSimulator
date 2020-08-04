using System;
using System.Collections.Generic;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Helper
{
    public class Validator : IValidator
    {
        public bool ValidateFirstCommand(string command)
        {

            var commandSplit = command.Split(' ', ',');
            string commandType = commandSplit[0];

            if (commandType != MoveType.PLACE.ToString() && commandSplit.Length != 4)
            {
                return false;
            }

            return ValidateParameters(commandSplit);
        }

        public bool ValidateInputCommand(string command)
        {
            var commandSplit = command.Split(' ', ',');
            string commandType = commandSplit[0];

            if (commandType != MoveType.PLACE.ToString() && commandSplit.Length > 1)
            {
                return false;
            }

            return commandType == MoveType.PLACE.ToString() ? ValidateParameters(commandSplit) : Enum.IsDefined(typeof(MoveType), commandType);
        }

        private bool ValidateParameters(IReadOnlyList<string> commandSplit)
        {
            if (commandSplit.Count != 4)
            {
                return false;
            }

            return CoOrdinateValidator(commandSplit[1]) &&
                   CoOrdinateValidator(commandSplit[2]) &&
                   Enum.IsDefined(typeof(Direction), commandSplit[3]);
        }

        private bool CoOrdinateValidator(string coOrdinate)
        {
            bool isValid = byte.TryParse(coOrdinate, out byte axisPoint);
            return isValid && WithinValidRange(axisPoint);
        }

        private bool WithinValidRange(byte axisPoint)
        {
            return axisPoint <= 4;
        }
    }
}
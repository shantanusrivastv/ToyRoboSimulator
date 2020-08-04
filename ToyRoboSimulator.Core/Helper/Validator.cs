using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToyRoboSimulator.Core.Helper
{
    public class Validator
    {
        public bool ValidateFirstCommand(string command)
        {
            string[] commandSplit = command.Split(' ', ',');
            string commandType = commandSplit[0];

            if (commandType != MoveType.PLACE.ToString() && commandSplit.Length != 4)
            {
                return false;
            }

            else
            {
                return ValidateParmeters(commandSplit);

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
                return ValidateParmeters(commandSplit);


            }

            return Enum.IsDefined(typeof(MoveType), commandType);
        }



        private bool ValidateParmeters(string[] commandSplit)
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

        public bool WithinValidRange(byte axisPoint)
        {
            return axisPoint >= 0 && axisPoint <= 4;
        }
    }
}

using System;
using ToyRoboSimulator.Core.Helper;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public class Simulator : ISimulator
    {
        private readonly IValidator _validator;
        private readonly IRoboMover _roboMover;
        private bool _hasApplicationInitialised;
        private ICommandFactory _commandFactory;

        private (byte XAxis, byte YAxis, Direction CurrentDirection) CurrentPosition
        {
            get;
            set;
        }

        public Simulator(IValidator validator, IRoboMover roboMover, ICommandFactory commandFactory)
        {
            _validator = validator;
            _roboMover = roboMover;
            _commandFactory = commandFactory;
        }

        public (byte XAxis, byte YAxis, Direction CurrentDirection) MoveRobo(string inputCommand)
        {
            if (_hasApplicationInitialised)
            {
                if (_validator.ValidateInputCommand(inputCommand))
                {
                    string[] commandSplit = inputCommand.Split(' ', ',');
                    Enum.TryParse(commandSplit[0], out CommandType commandType);

                    ICommand command = _commandFactory.CreateCommand(commandType);
                    command.PreviousPosition = CommandType.PLACE != commandType ? CurrentPosition : SetPostion(inputCommand);
                    CurrentPosition = command.Execute();
                    //NewPosition = _roboMover.PerformMove(commandText);
                }
            }
            else
            {
                CheckIfFirstCommandIsPlace(inputCommand);
            }

            return CurrentPosition;
        }

        private (byte, byte, Direction) SetPostion(string inputCommand)
        {
            string[] commandSplit = inputCommand.Split(' ', ',');
            var nextXAxis = byte.Parse(commandSplit[1]);
            var nextYAxis = byte.Parse(commandSplit[2]);
            var direction = (Direction)Enum.Parse(typeof(Direction), commandSplit[3]);
            return (nextXAxis, nextYAxis, direction);
        }

        private void CheckIfFirstCommandIsPlace(string command)
        {
            if (_validator.ValidateFirstCommand(command))
            {
                _roboMover.SetPosition(command);
                _hasApplicationInitialised = true;
            }
            else
            {
                _hasApplicationInitialised = false;
            }
        }
    }
}
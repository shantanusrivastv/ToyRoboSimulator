using ToyRoboSimulator.Core.Helper;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public class Simulator : ISimulator
    {
        private readonly IValidator _validator;
        private readonly IRoboMover _roboMover;
        private bool _hasApplicationInitialised;

        private (byte XAxis, byte YAxis, Direction CurrentDirection) NewPosition
        {
            get;
            set;
        }

        public Simulator(IValidator validator, IRoboMover roboMover)
        {
            _validator = validator;
            _roboMover = roboMover;
        }

        public (byte XAxis, byte YAxis, Direction CurrentDirection) MoveRobo(string moveCommand)
        {
            if (_hasApplicationInitialised)
            {
                if (_validator.ValidateInputCommand(moveCommand))
                {
                    NewPosition = _roboMover.PerformMove(moveCommand);
                }
            }
            else
            {
                CheckIfFirstCommandIsPlace(moveCommand);
            }

            return NewPosition;
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
using System;
using ToyRoboSimulator.Core.Helper;

namespace ToyRoboSimulator.Core
{
    public class Simulator : ISimulator
    {
        private readonly IValidator _validator;
        private bool hasApplicationInitialised = false;

        private (byte XAxis, byte YAxis, Direction CurrentDirection) CurrentPosition
        {
            get;
            set;
        }

        public Simulator(IValidator validator)
        {
            _validator = validator;
        }

        //public Simulator(string command, IValidator validator)
        //{
        //    _validator = validator;
        //    if (validator.ValidateFirstCommand(command))
        //    {
        //        SetPosition(command);
        //        //continue
        //    }
        //    else
        //    {
        //        //Make Custom Exception
        //        throw new Exception("InValid Command");
        //    }
        //}

        public (byte XAxis, byte YAxis, Direction CurrentDirection) MoveRobo(string moveCommand)
        {
            if (hasApplicationInitialised)
            {
                if (_validator.ValidateInputCommand(moveCommand))
                {
                    Enum RequestedMoveType = (MoveType)Enum.Parse(typeof(MoveType), moveCommand.Split(' ', ',')[0]);

                    switch (RequestedMoveType)
                    {
                        case MoveType.MOVE:
                            PerformMove();
                            break;

                        case MoveType.PLACE:
                            PerformPlaceMove(moveCommand);
                            break;

                        case MoveType.RIGHT:
                            TurnRight();
                            break;

                        case MoveType.LEFT:
                            TurnLeft();
                            break;

                        case MoveType.REPORT:
                            break;

                        default:
                            break;
                    }

                    return CurrentPosition;
                }
                else
                {
                    throw new Exception("Invalid Move Command");
                }
            }
            else
            {
                CheckIfFirstCommandIsPLACE(moveCommand);
                return CurrentPosition;
            }
        }

        private void CheckIfFirstCommandIsPLACE(string command)
        {
            if (_validator.ValidateFirstCommand(command))
            {
                SetPosition(command);
                hasApplicationInitialised = true;
                //continue
            }
            else
            {
                //Make Custom Exception
                throw new Exception("InValid Command");
            }
        }

        private void TurnRight()
        {
            int tempDirection = (int)CurrentPosition.CurrentDirection;
            tempDirection = tempDirection == 3 ? 0 : tempDirection + 1;

            CurrentPosition = (CurrentPosition.XAxis, CurrentPosition.YAxis, (Direction)tempDirection);
        }

        private void TurnLeft()
        {
            int tempDirection = (int)CurrentPosition.CurrentDirection;
            tempDirection = tempDirection == 0 ? 3 : tempDirection - 1;

            CurrentPosition = (CurrentPosition.XAxis, CurrentPosition.YAxis, (Direction)tempDirection);
        }

        private void PerformMove()
        {
            switch (CurrentPosition.CurrentDirection)
            {
                case Direction.NORTH:
                    MoveUP();
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

                default:
                    break;
            }
        }

        private void PerformPlaceMove(string command)
        {
            if (_validator.ValidateFirstCommand(command))
            {
                SetPosition(command);
            }
        }

        private void MoveUP()
        {
            if (CurrentPosition.YAxis < 4)
            {
                CurrentPosition = (CurrentPosition.XAxis, Convert.ToByte(CurrentPosition.YAxis + 1), CurrentPosition.CurrentDirection);
            }
        }

        private void MoveDown()
        {
            if (CurrentPosition.YAxis >= 1)
            {
                CurrentPosition = (CurrentPosition.XAxis, Convert.ToByte(CurrentPosition.YAxis - 1), CurrentPosition.CurrentDirection);
            }
        }

        private void MoveLeft()
        {
            if (CurrentPosition.XAxis >= 1)
            {
                CurrentPosition = (Convert.ToByte(CurrentPosition.XAxis - 1), CurrentPosition.YAxis, CurrentPosition.CurrentDirection);
            }
        }

        private void MoveRight()
        {
            if (CurrentPosition.XAxis < 4)
            {
                CurrentPosition = (Convert.ToByte(CurrentPosition.XAxis + 1), CurrentPosition.YAxis, CurrentPosition.CurrentDirection);
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
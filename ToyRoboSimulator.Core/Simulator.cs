﻿using System;
using ToyRoboSimulator.Core.Helper;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public class Simulator : ISimulator
    {
        private readonly IValidator _validator;
        private bool _hasApplicationInitialised;

        private (byte XAxis, byte YAxis, Direction CurrentDirection) CurrentPosition
        {
            get;
            set;
        }

        public Simulator(IValidator validator)
        {
            _validator = validator;
        }

        public (byte XAxis, byte YAxis, Direction CurrentDirection) MoveRobo(string moveCommand)
        {
            if (_hasApplicationInitialised)
            {
                if (_validator.ValidateInputCommand(moveCommand))
                {
                    Enum requestedMoveType = (MoveType)Enum.Parse(typeof(MoveType), moveCommand.Split(' ', ',')[0]);

                    switch (requestedMoveType)
                    {
                        case MoveType.MOVE:
                            PerformForwordMove();
                            break;

                        case MoveType.PLACE:
                            PerformPlacementMove(moveCommand);
                            break;

                        case MoveType.RIGHT:
                            PerformRightRotation();
                            break;

                        case MoveType.LEFT:
                            PerformLeftRotation();
                            break;

                        case MoveType.REPORT:
                            //Already returning the CurrentPosition
                            break;
                    }
                }
                return CurrentPosition;
            }
            else
            {
                CheckIfFirstCommandIsPlace(moveCommand);
                return CurrentPosition;
            }
        }

        private void CheckIfFirstCommandIsPlace(string command)
        {
            if (_validator.ValidateFirstCommand(command))
            {
                SetPosition(command);
                _hasApplicationInitialised = true;
            }
            else
            {
                _hasApplicationInitialised = false;
            }
        }

        private void PerformRightRotation()
        {
            int tempDirection = (int)CurrentPosition.CurrentDirection;
            tempDirection = tempDirection == 3 ? 0 : tempDirection + 1;

            CurrentPosition = (CurrentPosition.XAxis, CurrentPosition.YAxis, (Direction)tempDirection);
        }

        private void PerformLeftRotation()
        {
            int tempDirection = (int)CurrentPosition.CurrentDirection;
            tempDirection = tempDirection == 0 ? 3 : tempDirection - 1;

            CurrentPosition = (CurrentPosition.XAxis, CurrentPosition.YAxis, (Direction)tempDirection);
        }

        private void PerformForwordMove()
        {
            switch (CurrentPosition.CurrentDirection)
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
        }

        private void PerformPlacementMove(string command)
        {
            if (_validator.ValidateFirstCommand(command))
            {
                SetPosition(command);
            }
        }

        private void MoveUp()
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

        private void SetPosition(string command)
        {
            string[] commandSplit = command.Split(' ', ',');
            var nextXAxis = byte.Parse(commandSplit[1]);
            var nextYAxis = byte.Parse(commandSplit[2]);
            var direction = (Direction)Enum.Parse(typeof(Direction), commandSplit[3]);
            CurrentPosition = (nextXAxis, nextYAxis, direction);
        }
    }
}
using System;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core
{
    public class RoboMover : IRoboMover
    {
        private (byte XAxis, byte YAxis, Direction CurrentDirection) CurrentPosition
        {
            get;
            set;
        }

        public (byte XAxis, byte YAxis, Direction CurrentDirection) PerformMove(string moveCommand)
        {
            Enum requestedMoveType = (CommandType)Enum.Parse(typeof(CommandType), moveCommand.Split(' ', ',')[0]);

            switch (requestedMoveType)
            {
                case CommandType.MOVE:
                    PerformForwordMove();
                    break;

                case CommandType.PLACE:
                    PerformPlacementMove(moveCommand);
                    break;

                case CommandType.RIGHT:
                    PerformRightRotation();
                    break;

                case CommandType.LEFT:
                    PerformLeftRotation();
                    break;

                case CommandType.REPORT:
                    //Already returning the CurrentPosition
                    break;
            }

            return CurrentPosition;
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

        private void PerformPlacementMove(string command)
        {
            SetPosition(command);
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

        public void SetPosition(string command)
        {
            string[] commandSplit = command.Split(' ', ',');
            var nextXAxis = byte.Parse(commandSplit[1]);
            var nextYAxis = byte.Parse(commandSplit[2]);
            var direction = (Direction)Enum.Parse(typeof(Direction), commandSplit[3]);
            CurrentPosition = (nextXAxis, nextYAxis, direction);
        }
    }
}
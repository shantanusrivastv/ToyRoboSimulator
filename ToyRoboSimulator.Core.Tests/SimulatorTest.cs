using Microsoft.Extensions.Logging;
using NUnit.Framework;
using ToyRoboSimulator.Core.Commands;
using ToyRoboSimulator.Core.Helper;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Core.Tests
{
    [TestFixture]
    public class SimulatorTest
    {
        private Simulator _sut;
        private readonly byte _xPosition = 0;
        private readonly byte _yPosition = 0;

        [SetUp]
        public void Setup()
        {
            using var loggerFactory = LoggerFactory.Create(x=>x.AddConsole());
            var logger = loggerFactory.CreateLogger<Simulator>();
            _sut = new Simulator(new Validator(),  new CommandFactory(), logger);
        }

        [TestCase("MOVE")]
        public void ShouldNotMoveIfFirstCommandIsNotPlace(string command)
        {
            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo(command);
            Assert.AreEqual(default(byte), xAxis);
            Assert.AreEqual(default(byte), yAxis);
            Assert.AreEqual(Direction.SOUTH, currentDirection);
        }

        [TestCase("MOVE", 4)]
        public void ShouldMoveUpByGivenNoOfSteps(string moveCommand, int steps)
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");
            for (int i = 0; i <= steps; i++)
            {
                _sut.MoveRobo(moveCommand);
            }

            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(_xPosition, xAxis);
            Assert.AreEqual(_yPosition + steps, yAxis);
            Assert.AreEqual(Direction.NORTH, currentDirection);
        }

        [TestCase("MOVE", 5)]
        public void ShouldMoveUpButNotFall(string moveCommand, int steps)
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");
            for (int i = 0; i <= steps; i++)
            {
                _sut.MoveRobo(moveCommand);
            }

            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(_xPosition, xAxis);
            Assert.AreEqual(4, yAxis);
            Assert.AreEqual(Direction.NORTH, currentDirection);
        }

        [TestCase("RIGHT")]
        public void ShouldRotateRight(string moveCommand)
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");
            _sut.MoveRobo(moveCommand);

            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(_xPosition, xAxis);
            Assert.AreEqual(_yPosition, yAxis);
            Assert.AreEqual(Direction.EAST, currentDirection);
        }

        [TestCase("RIGHT", "MOVE")]
        public void ShouldRotateRightAndMove(string rotationCommand, string moveCommand)
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");
            _sut.MoveRobo(rotationCommand);
            _sut.MoveRobo(moveCommand);

            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(_xPosition + 1, xAxis);
            Assert.AreEqual(_yPosition, yAxis);
            Assert.AreEqual(Direction.EAST, currentDirection);
        }

        [TestCase("LEFT")]
        public void ShouldRotateLeft(string moveCommand)
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");
            _sut.MoveRobo(moveCommand);
            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(_xPosition, xAxis);
            Assert.AreEqual(0, yAxis);
            Assert.AreEqual(Direction.WEST, currentDirection);
        }

        [TestCase("LEFT", "MOVE")]
        public void ShouldRotateLeftAndMove(string rotationCommand, string moveCommand)
        {
            _sut.MoveRobo("PLACE 1,0,NORTH");
            _sut.MoveRobo(rotationCommand);
            _sut.MoveRobo(moveCommand);
            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(_xPosition, xAxis);
            Assert.AreEqual(_yPosition, yAxis);
            Assert.AreEqual(Direction.WEST, currentDirection);
        }

        [TestCase("PLACE 2,4,WEST")]
        public void ShoulPlaceRoboInPosition(string command)
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");
            _sut.MoveRobo(command);
            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(2, xAxis);
            Assert.AreEqual(4, yAxis);
            Assert.AreEqual(Direction.WEST, currentDirection);
        }

        [Test]
        public void ShouldMoveMultipleSteps()
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(0, xAxis);
            Assert.AreEqual(2, yAxis);
            Assert.AreEqual(Direction.NORTH, currentDirection);
        }

        [Test]
        public void ShouldRotateAndMoveMultipleSteps()
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");

            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("RIGHT");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("RIGHT");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("LEFT");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("RIGHT");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("RIGHT");
            _sut.MoveRobo("MOVE");

            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(2, xAxis);
            Assert.AreEqual(1, yAxis);
            Assert.AreEqual(Direction.WEST, currentDirection);
        }

        [Test]
        public void ShouldMoveMultipleStepsWithoutFalling()
        {
            _sut.MoveRobo("PLACE 0,0,NORTH");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");
            _sut.MoveRobo("MOVE");

            var (xAxis, yAxis, currentDirection) = _sut.MoveRobo("REPORT");
            Assert.AreEqual(0, xAxis);
            Assert.AreEqual(4, yAxis);
            Assert.AreEqual(Direction.NORTH, currentDirection);
        }
    }
}
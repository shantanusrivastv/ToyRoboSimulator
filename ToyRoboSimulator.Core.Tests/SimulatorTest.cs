using NUnit.Framework;
using System;
using ToyRoboSimulator.Core.Helper;

namespace ToyRoboSimulator.Core.Tests
{
    [TestFixture]
    public class SimulatorTest
    {
        private Simulator sut;
        private readonly byte xPostion = 0;
        private readonly byte yPostion = 0;

        [SetUp]
        public void Setup()
        {
            sut = new Simulator(new Validator());
        }

        [TestCase("MOVE")]
        public void ShouldThrowExceptionIfFirstCommandIsNotPLACE(string command)
        {
            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo(command);
            Assert.AreEqual(default(byte), XAxis);
            Assert.AreEqual(default(byte), YAxis);
            Assert.AreEqual(Direction.SOUTH, CurrentDirection);
        }

        [TestCase("MOVE", 4)]
        public void ShouldMoveUpByGivenNoOfSteps(string moves, int steps)
        {
            sut.MoveRobo("PLACE 0,0,NORTH");
            for (int i = 0; i <= steps; i++)
            {
                sut.MoveRobo(moves);
            }

            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo("REPORT");
            Assert.AreEqual(xPostion, XAxis);
            Assert.AreEqual(yPostion + steps, YAxis);
            Assert.AreEqual(Direction.NORTH, CurrentDirection);
        }

        [TestCase("MOVE", 5)]
        public void ShouldMoveUpButNotFall(string moves, int steps)
        {
            sut.MoveRobo("PLACE 0,0,NORTH");
            for (int i = 0; i <= steps; i++)
            {
                sut.MoveRobo(moves);
            }

            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo("REPORT");
            Assert.AreEqual(xPostion, XAxis);
            Assert.AreEqual(4, YAxis);
            Assert.AreEqual(Direction.NORTH, CurrentDirection);
        }

        [TestCase("RIGHT")]
        public void ShouldMoveRight(string moves)
        {
            sut.MoveRobo("PLACE 0,0,NORTH");
            sut.MoveRobo(moves);

            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo("REPORT");
            Assert.AreEqual(xPostion, XAxis);
            Assert.AreEqual(yPostion, YAxis);
            Assert.AreEqual(Direction.EAST, CurrentDirection);
        }

        [TestCase("LEFT")]
        public void ShouldMoveLeft(string moves)
        {
            sut.MoveRobo("PLACE 0,0,NORTH");
            sut.MoveRobo(moves);
            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo("REPORT");
            Assert.AreEqual(xPostion, XAxis);
            Assert.AreEqual(0, YAxis);
            Assert.AreEqual(Direction.WEST, CurrentDirection);
        }

        [TestCase("PLACE 2,4,WEST")]
        public void ShoulPlaceRoboInPosition(string command)
        {
            sut.MoveRobo("PLACE 0,0,NORTH");
            sut.MoveRobo(command);
            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo("REPORT");
            Assert.AreEqual(2, XAxis);
            Assert.AreEqual(4, YAxis);
            Assert.AreEqual(Direction.WEST, CurrentDirection);
        }

        [Test]
        public void ShouldMoveMulipleSteps()
        {
            sut.MoveRobo("PLACE 0,0,NORTH");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo("REPORT");
            Assert.AreEqual(0, XAxis);
            Assert.AreEqual(2, YAxis);
            Assert.AreEqual(Direction.NORTH, CurrentDirection);
        }

        [Test]
        public void ShouldRotateAndMoveMulipleSteps()
        {
            sut.MoveRobo("PLACE 0,0,NORTH");

            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("RIGHT");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo("REPORT");
            Assert.AreEqual(2, XAxis);
            Assert.AreEqual(4, YAxis);
            Assert.AreEqual(Direction.EAST, CurrentDirection);
        }

        [Test]
        public void ShouldMoveMulipleStepsWithoutFalling()
        {
            sut.MoveRobo("PLACE 0,0,NORTH");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");
            sut.MoveRobo("MOVE");

            var (XAxis, YAxis, CurrentDirection) = sut.MoveRobo("REPORT");
            Assert.AreEqual(0, XAxis);
            Assert.AreEqual(4, YAxis);
            Assert.AreEqual(Direction.NORTH, CurrentDirection);
        }
    }
}
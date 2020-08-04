using NUnit.Framework;

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
            sut = new Simulator("PLACE 0,0,NORTH");
        }

        [TestCase("MOVE", 4)]
        public void ShouldMoveUpByGivenNoOfSteps(string moves, int steps)
        {
            for (int i = 0; i <= steps; i++)
            {
                sut.MoveRobo(moves);
            }

            var (XAxis, YAxis, CurrentDirection) = sut.CurrentPosition;
            Assert.AreEqual(xPostion, XAxis);
            Assert.AreEqual(yPostion + steps, YAxis);
            Assert.AreEqual(Direction.NORTH, CurrentDirection);
        }

        [TestCase("MOVE", 5)]
        public void ShouldMoveUpButNotFall(string moves, int steps)
        {
            for (int i = 0; i <= steps; i++)
            {
                sut.MoveRobo(moves);
            }

            var (XAxis, YAxis, CurrentDirection) = sut.CurrentPosition;
            Assert.AreEqual(xPostion, XAxis);
            Assert.AreEqual(4, YAxis);
            Assert.AreEqual(Direction.NORTH, CurrentDirection);
        }
    }
}
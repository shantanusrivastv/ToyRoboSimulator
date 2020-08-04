using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using ToyRoboSimulator.Core.Helper;

namespace ToyRoboSimulator.Core.Tests
{
    [TestFixture]
    public class ValidatorTest
    {

        private Validator sut;
        [SetUp]
        public void Setup()
        {
            sut = new Validator();
        }

        [TestCase("PLACE 0,0,NORTH")]
        [TestCase("PLACE 4,0,SOUTH")]
        [TestCase("PLACE 4,4,EAST")]
        [TestCase("PLACE 2,4,WEST")]
        [TestCase("MOVE")]
        [TestCase("LEFT")]
        [TestCase("RIGHT")]
        [TestCase("REPORT")]
        public void ShouldReturnTrueForValidCommand(string command)
        {
            var res = sut.ValidateInputCommand(command);
            Assert.IsTrue(res);
        }

        [TestCase("PLACE1 0,0,NORTH")]
        [TestCase("PLACE -2,8,3")]
        [TestCase("PLACE 4,-4,EAST")]
        [TestCase("PLACE ,WEST")]
        [TestCase("MOVE 1,2")]
        [TestCase("MOVE LEFT")]

        public void ShouldReturnFalseForInvalidValidCommand(string command)
        {
            var res = sut.ValidateInputCommand(command);
            Assert.IsFalse(res);
        }
    }
}

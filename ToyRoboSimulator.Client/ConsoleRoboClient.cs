using System;
using ToyRoboSimulator.Core;

namespace ToyRoboSimulator.Client
{
    public class ConsoleRoboClient
    {
        private readonly ISimulator _simulator;

        public ConsoleRoboClient(ISimulator simulator)
        {
            _simulator = simulator;
        }

        public void Run()
        {
            _simulator.MoveRobo("PLACE 1,2,EAST");
            var (XAxis, YAxis, CurrentDirection) = _simulator.MoveRobo("RIGHT");
            Console.WriteLine($"The X-Axis is {XAxis}, Y-Axis is {YAxis} and the direction facing is {CurrentDirection}");
        }
    }
}
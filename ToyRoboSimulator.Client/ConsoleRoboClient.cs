using System;
using ToyRoboSimulator.Core;

namespace ToyRoboSimulator.Client
{
    public class ConsoleRoboClient
    {
        private readonly ISimulator _simulator;

        private readonly string instruction = "Welcome! to Robo Simulator, please note the first command has to be PLACE command " +
                              "for the appliaction to initiate, E.g: PLACE 0,0,NORTH" +
                              "please enter your command shown below";

        public ConsoleRoboClient(ISimulator simulator)
        {
            _simulator = simulator;
        }

        public void Run()
        {
            Console.Clear();
            Console.WriteLine(instruction);
            DisplayCommands();

            while (true)
            {
                try
                {
                    string choosenCommand = Console.ReadLine();
                    MoveType moveChoice = (MoveType)Enum.Parse(typeof(MoveType), choosenCommand);
                    if (moveChoice == MoveType.PLACE)
                    {
                        Console.WriteLine("You have selected PLACE command please enter the X-Axis, Y-Axis and Facing Direction");
                        try
                        {
                            string placeCommand = string.Concat(nameof(MoveType.PLACE), " ", Console.ReadLine());
                            _simulator.MoveRobo(placeCommand);
                            Console.Clear();
                            Console.Write("Successfully placed the ROBO!! ");
                            DisplayCommands();
                        }
                        catch (Exception ex)
                        {
                            Console.Clear();
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("Error while parsing the command ");
                            DisplayCommands();
                        }
                    }
                    else if (moveChoice == MoveType.REPORT)
                    {
                        Console.Clear();
                        var (XAxis, YAxis, CurrentDirection) = _simulator.MoveRobo(moveChoice.ToString());
                        Console.WriteLine($"The X-Axis is {XAxis}, Y-Axis is {YAxis} and the direction facing is {CurrentDirection}");
                        DisplayCommands();
                    }
                    else if (choosenCommand == "0")
                    {
                        break;
                    }
                    else
                    {
                        Console.Clear();
                        _simulator.MoveRobo(moveChoice.ToString());
                        Console.Write($"Successfully performed the {moveChoice} command!! ");
                        DisplayCommands();
                    }
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    Console.WriteLine($" {ex.Message} {Environment.NewLine} Error while parsing the command, please try again ");
                    DisplayCommands();
                }
            }
        }

        private void DisplayCommands()
        {
            Console.WriteLine($"{Environment.NewLine}1: For PLACE Command");
            Console.WriteLine("2: For MOVE Command");
            Console.WriteLine("3: For LEFT Command");
            Console.WriteLine("4: For RIGHT Command");
            Console.WriteLine("5: For REPORT Command");
            Console.WriteLine("0: For Exit");
            Console.WriteLine("Please select a Command between 0-5 and hit enter");
        }
    }
}
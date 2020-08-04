using System;
using System.Linq;
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
                   bool validNumber = int.TryParse(Console.ReadLine(), out int chosenCommand);

                    if (validNumber &&  Enumerable.Range(0, 5).Contains(chosenCommand))
                    {

                        MoveType moveChoice = (MoveType)chosenCommand;
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
                            var (xAxis, yAxis, currentDirection) = _simulator.MoveRobo(moveChoice.ToString());
                            Console.WriteLine($"The X-Axis is {xAxis}, Y-Axis is {yAxis} and the direction facing is {currentDirection}");
                            DisplayCommands();
                        }
                        else if (chosenCommand == 0)
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

                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid Selection please try again");
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

        private static void DisplayCommands()
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
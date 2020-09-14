using Microsoft.Extensions.Logging;
using System;
using ToyRoboSimulator.Core;
using ToyRoboSimulator.Enums;

namespace ToyRoboSimulator.Client
{
    public class ConsoleRoboClient
    {
        private readonly ISimulator _simulator;
        private readonly ILogger<ConsoleRoboClient> _logger;

        private readonly string _instruction = "Welcome! to Robo Simulator, please note the first command has to be PLACE command, " +
                                                 "for the application to initiate," + Environment.NewLine +
                                                 "Only for PLACE command coordinates and direction are required , other commands can be selected from the list ." +
                                                 Environment.NewLine + "Invalid commands will be ignored " +
                                                 Environment.NewLine +
                                                "please enter your command shown below";

        public ConsoleRoboClient(ISimulator simulator, ILogger<ConsoleRoboClient> logger)
        {
            _logger = logger;
            _simulator = simulator;
        }

        public void Run()
        {
            //_logger.LogInformation("Sample Log");
            Console.Clear();
            Console.WriteLine(_instruction);
            DisplayCommands();

            while (true)
            {
                try
                {
                    bool validNumber = byte.TryParse(Console.ReadLine(), out byte chosenCommand);

                    if (validNumber && chosenCommand <= 5)
                    {
                        CommandType moveChoice = (CommandType)chosenCommand;
                        switch (moveChoice)
                        {
                            case CommandType.PLACE:
                                PlaceCommand();
                                break;

                            case CommandType.REPORT:
                                var (xAxis, yAxis, currentDirection) = _simulator.MoveRobo(moveChoice.ToString());
                                Console.Clear();
                                Console.WriteLine($"The X-Axis is {xAxis}, Y-Axis is {yAxis} and the direction facing is {currentDirection}");
                                DisplayCommands();
                                break;

                            case CommandType.LEFT:
                            case CommandType.RIGHT:
                            case CommandType.MOVE:
                                _simulator.MoveRobo(moveChoice.ToString());
                                Console.Clear();
                                Console.Write($"Successfully performed the {moveChoice} command!! ");
                                DisplayCommands();
                                break;

                            default:
                                return;
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

        private void PlaceCommand()
        {
            Console.WriteLine("You have selected PLACE command please enter the X-Axis, Y-Axis and Facing Direction");
            Console.WriteLine("Example 0,0,NORTH");
            try
            {
                string placeCommand = string.Concat(nameof(CommandType.PLACE), " ", Console.ReadLine());
                _simulator.MoveRobo(placeCommand);
                Console.Clear();
                Console.Write("Successfully placed the ROBOT!! ");
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
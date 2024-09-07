using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Battleship___Luke_Campbell.Ship;
using System.IO;
using System.Globalization;

namespace Battleship___Luke_Campbell
{
    public static class ShipFactory
    {
        /// <summary>
        /// regex that matches ship's type, startign coords ( x,y ), direction ( v,h ), length (any positive value)
        /// </summary>
        private static readonly Regex ShipRegex = new Regex(
            @"^(Carrier|Battleship|Destroyer|Submarine|Patrol\sBoat)\s(\d+),(\d+)\s(h|v)\s(\d+)$",
            RegexOptions.Compiled
        );

        /// <summary>
        /// This matches the ship's data from the file to the regex and checks if it is legit
        /// </summary>
        /// <param name="description">Ship's row data from the .txt file</param>
        /// <returns>Bool whether the row data is good or bad</returns>
        public static bool VerifyShipString(string description)
        {
            // Debugging output
            Console.WriteLine($"Verifying: {description}");

            // Match the input string with the regex
            var match = ShipRegex.Match(description);

            // If the string doesn't match the pattern, return false
            if (!match.Success)
            {
                Console.WriteLine("Regex match failed.");
                return false;
            }

            // set the matched groups
            string shipType = match.Groups[1].Value;
            int x = int.Parse(match.Groups[2].Value);
            int y = int.Parse(match.Groups[3].Value);
            string direction = match.Groups[4].Value;
            int length = int.Parse(match.Groups[5].Value);

            // Debugging output
            Console.WriteLine($"Parsed Values: Type={shipType}, x={x}, y={y}, direction={direction}, length={length}");

            // Check if the ship's length is valid (ships longer than 5 or shorter than 1 should not exist)
            if (length < 1 || length > 5)
            {
                Console.WriteLine("Invalid length.");
                return false;
            }

            // Check if the ship's position and length stay within the 10x10 grid
            if (direction == "h")
            {
                if (x < 0 || x + length - 1 >= 10 || y < 0 || y >= 10)
                {
                    Console.WriteLine("Horizontal boundary check failed.");
                    return false;
                }
            }
            // check if the ship stays in the grid vertically
            else if (direction == "v")
            {
                if (y < 0 || y + length - 1 >= 10 || x < 0 || x >= 10)
                {
                    Console.WriteLine("Vertical boundary check failed.");
                    return false;
                }
            }
            // catches the ships that do not have a valid direction
            else
            {
                Console.WriteLine("Invalid direction.");
                return false; // Invalid direction
            }

            // If all checks passed, return true
            Console.WriteLine("Ship is valid.");
            return true;
        }

        /// <summary>
        /// Takes the Ship's row from the file and if its verified then gets created using the ship's type.
        /// Then matching that to the appropriate constructor
        /// </summary>
        /// <param name="description">Ship's row data from the parsed .txt file</param>
        /// <returns>Ship object as long as it is verified</returns>
        /// <exception cref="FormatException">Ship did not get verified by <see cref="VerifyShipString(string)"/></exception>
        /// <exception cref="ArgumentException">Invalid ship type</exception>
        public static Ship ParseShipString(string description)
        {
            Console.WriteLine($"Parsing ship: {description}");

            if (!VerifyShipString(description))
            {
                throw new FormatException($"Failed to create a Ship...\n\tHere is your description: {description}." +
                                          $"\n\tPlease follow the format.\n\tOr make sure you are inside bounds...");
            }
            else // this is for the ships that are good:
            {
                // Extract ship details using the regex
                var match = ShipRegex.Match(description);

                string shipType = match.Groups[1].Value;
                int x = int.Parse(match.Groups[2].Value);
                int y = int.Parse(match.Groups[3].Value);
                string directionStr = match.Groups[4].Value;
                DirectionType direction = (DirectionType)Enum.Parse(typeof(DirectionType), directionStr);
                int length = int.Parse(match.Groups[5].Value);

                // Create and return the appropriate ship type
                switch (shipType)
                {
                    case "Carrier":
                        return new Carrier(new Coord2D(x, y), direction);
                    case "Battleship":
                        return new Battleship(new Coord2D(x, y), direction);
                    case "Destroyer":
                        return new Destroyer(new Coord2D(x, y), direction);
                    case "Submarine":
                        return new Submarine(new Coord2D(x, y), direction);
                    case "Patrol Boat":
                        return new PatrolBoat(new Coord2D(x, y), direction);
                    default:
                        throw new ArgumentException($"Invalid ship type: {shipType}");
                }
            }
        }

        /// <summary>
        /// Reads the file path and creates descriptions based on each row containing ship data
        /// </summary>
        /// <param name="filePath">string of the .txt files</param>
        /// <returns>array of ships containing each ship's row data which is verified and parsed first</returns>
        public static Ship[] ParseShipFile(string filePath)
        {
            List<Ship> ships = new List<Ship>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                // Read each line from the file
                while ((line = reader.ReadLine()) != null) 
                {
                    // Remove leading/trailing whitespace
                    line = line.Trim(); 

                    // Skips lines that start with "#"
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    {
                        continue;
                    }

                    try
                    {
                        // Split the line by comma to get the individual components
                        var parts = line.Split(',');

                        // Ensure the line has the correct number of parts
                        if (parts.Length != 5)
                        {
                            throw new FormatException($"Invalid format: {line}");
                        }

                        // sets the matched groups
                        string shipType = parts[0].Trim();
                        int length = int.Parse(parts[1].Trim());
                        string direction = parts[2].Trim();
                        int x = int.Parse(parts[3].Trim());
                        int y = int.Parse(parts[4].Trim());

                        // Construct the ship description in the expected format
                        string description = $"{shipType} {x},{y} {direction} {length}";

                        // Parse and add the ship
                        Ship ship = ParseShipString(description);
                        ships.Add(ship);
                    }
                    catch (FormatException ex)
                    {
                        // Handle format exceptions (optional logging)
                        Console.WriteLine($"Error parsing line: {ex.Message}");
                    }
                }
            }

            // this makes sure the correct amount of ships get added, dynamically. 
            // then parsed to an array because it will not be changing
            return ships.ToArray();
        }
    }
}

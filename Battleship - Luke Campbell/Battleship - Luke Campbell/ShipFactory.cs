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
        private static readonly Regex ShipRegex = new Regex(
            @"^(Carrier|Battleship|Destroyer|Submarine|Patrol\sBoat)\s(\d+),(\d+)\s(h|v)\s(\d+)$",
            RegexOptions.Compiled
        );


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

            // Extract the matched groups
            string shipType = match.Groups[1].Value;
            int x = int.Parse(match.Groups[2].Value);
            int y = int.Parse(match.Groups[3].Value);
            string direction = match.Groups[4].Value;
            int length = int.Parse(match.Groups[5].Value);

            // Debugging output
            Console.WriteLine($"Parsed Values: Type={shipType}, x={x}, y={y}, direction={direction}, length={length}");

            // Check if the ship's length is valid (ships longer than 5 should not exist)
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
            else if (direction == "v")
            {
                if (y < 0 || y + length - 1 >= 10 || x < 0 || x >= 10)
                {
                    Console.WriteLine("Vertical boundary check failed.");
                    return false;
                }
            }
            else
            {
                Console.WriteLine("Invalid direction.");
                return false; // Invalid direction
            }

            // If all checks passed, return true
            Console.WriteLine("Ship is valid.");
            return true;
        }

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

        public static Ship[] ParseShipFile(string filePath)
        {
            List<Ship> ships = new List<Ship>();

            using (var reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null) // Read each line from the file
                {
                    line = line.Trim(); // Remove leading/trailing whitespace

                    // Skip empty lines or comments
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

            return ships.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Battleship___Luke_Campbell.Ship;
using CsvHelper;
using System.IO;
using System.Globalization;

namespace Battleship___Luke_Campbell
{
    public static class ShipFactory
    {
        private static readonly Regex ShipRegex = new Regex(
            @"^(Carrier|Battleship|Destroyer|Submarine|PatrolBoat) \((\d+),(\d+)\) (Horizontal|Vertical) (\d+)$",
            RegexOptions.Compiled
        );

        public static bool VerifyShipString(string description)
        {
            // Debugging output
            //Console.WriteLine($"Verifying: {description}");

            // Match the input string with the regex
            var match = ShipRegex.Match(description);

            // Debugging output
            //Console.WriteLine(match.Success ? "Regex matched" : "Regex did not match");

            /*
            // If the string doesn't match the pattern, return false
            if (!match.Success)
            {
                Console.WriteLine("Regex match failed.");
                return false;
            }
            */

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
            if (direction == "Horizontal")
            {
                if (x < 0 || x + length - 1 >= 10 || y < 0 || y >= 10)
                {
                    Console.WriteLine("Horizontal boundary check failed.");
                    return false;
                }
            }
            else if (direction == "Vertical")
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

            return true;
        }

        public static Ship ParseShipString(string description)
        {
            Console.WriteLine($"Parsing ship: {description}");

            if (!VerifyShipString(description))
            {
                throw new FormatException($"Failed to create a Ship...\nHere is your description: {description}. Please follow the format.");
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
                    case "PatrolBoat":
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
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<ShipRecord>();

                foreach (var record in records)
                {
                    // Convert ShipRecord to string format expected by ParseShipString
                    string description = $"{record.ShipType} ({record.X},{record.Y}) {record.Direction} {record.Length}";

                    /*
                        Had to use ChatGPT to check how to skip .csv rows
                        https://chatgpt.com/share/4761e68c-2084-4263-9d5f-5eeb7ff02edc
                     */

                    if (description.StartsWith("#"))
                    {
                        continue;
                    }

                    // Now parse the description string
                    ships.Add(ParseShipString(description));
                }
            }

            return ships.ToArray();
        }
    }
}

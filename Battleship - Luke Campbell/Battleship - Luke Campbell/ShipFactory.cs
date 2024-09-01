using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Battleship___Luke_Campbell.Ship;
using CsvHelper;

namespace Battleship___Luke_Campbell
{
    public static class ShipFactory
    {
        private static readonly Regex ShipRegex = new Regex(
            @"^(Carrier|Battleship|Destroyer|Submarine|PatrolBoat) \((\d+),(\d+)\) (Horizontal|Vertical)",
            RegexOptions.Compiled
        );

        public static bool VerifyShipString(string description)
        {
            // Match the input string with the regex
            var match = ShipRegex.Match(description);

            // If the string doesn't match the pattern, return false
            if (!match.Success)
            {
                return false;
            }

            // Extract the matched groups
            string shipType = match.Groups[1].Value;
            int x = int.Parse(match.Groups[2].Value);
            int y = int.Parse(match.Groups[3].Value);
            string direction = match.Groups[4].Value;
            int length = int.Parse(match.Groups[5].Value);

            // Check if the length of the ship is less than 6
            if (length >= 6)
            {
                return false;
            }

            // Check if the ship's position and length stay within the 10x10 grid
            if (direction == "Horizontal")
            {
                if (x + length > 10 || x < 0 || y < 0 || y > 9)
                {
                    return false;
                }
            }
            else if (direction == "Vertical")
            {
                if (y + length > 10 || y < 0 || x < 0 || x > 9)
                {
                    return false;
                }
            }

            // If all checks passed, return true
            return true;
        }

        public static Ship ParseShipString(string description)
        {
            if (!VerifyShipString(description))
            {
                throw new FormatException($"Failed to create a Ship...\nHere is your description: {description}. Please follow the format.");
            }

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

        public static Ship[] ParseShipFile(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecord<Record>().ToList();
                foreach (var record in records)
                {

                }
            }

        }
    }
}

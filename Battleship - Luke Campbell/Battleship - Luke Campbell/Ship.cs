using CsvHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    /// <summary>
    /// This is the abstract Ship class that handles the default variables that each sub-class should contain
    /// </summary>
    public abstract class Ship : IHealth, IInfomatic
    {
        /// <summary>
        /// Ship's position
        /// </summary>
        public Coord2D Position { get; set; }
        /// <summary>
        /// Ship's length
        /// </summary>
        public byte Length { get; set; }
        /// <summary>
        /// Ship's Direction (h,v)
        /// </summary>
        public DirectionType Direction { get; set; }
        /// <summary>
        /// Each ship's coordinate points
        /// </summary>
        public Coord2D[] Points { get; set; }
        /// <summary>
        /// The points the user inputs to attack a ship
        /// gets checked to see if the ship's points match the inputted point
        /// </summary>
        public List<Coord2D> DamagedPoints { get; set; } = new List<Coord2D>();
        /// <summary>
        /// Ship's health
        /// ( basically length)
        /// </summary>
        public int health;

        /// <summary>
        /// Ship constructor that creates the abstract ship
        /// pushed to the sub-classes to create a more specific ship
        /// </summary>
        /// <param name="position">Ship's starting position</param>
        /// <param name="direction">Ship's direction</param>
        /// <param name="length">Ship's length</param>
        protected Ship(Coord2D position, DirectionType direction, byte length)
        {
            Position = position;
            Direction = direction;
            Length = length;

            // This sets each ship's coordinates based on their direction
            Points = new Coord2D[length];
            for (int i = 0; i < length; i++)
            {
                Points[i] = new Coord2D(
                    position.x + (direction == DirectionType.h ? i : 0),
                    position.y + (direction == DirectionType.v ? i : 0)
                );
            }

            DamagedPoints = new List<Coord2D>();
        }

        /// <summary>
        /// Checks if the user inputted coordinate matches a ship's coordinate point
        /// </summary>
        /// <param name="Point">User input (coordinate point)</param>
        /// <returns>
        /// Bool
        /// if hit then true
        /// if no match then false
        /// </returns>
        public bool CheckHit(Coord2D Point)
        {
            // Iterate over the points and check for a hit
            for (int i = 0; i < Points.Length; i++)
            {
                if (Point.Equals(Points[i])) // Check if the points are equal
                {
                    if (DamagedPoints.Contains(Points[i]))
                    {
                        Console.WriteLine("You have already attacked this coordinate.");
                        return false; // Return false since it's already been hit, no new hit registered
                    }

                    DamagedPoints.Add(Points[i]); // Add the damaged point to the list
                    Console.WriteLine($"You hit a ship at ({Point.x}, {Point.y})");
                    return true; // Return true since a new hit was registered
                }
            }

            // If no hit was found, return false
            return false;
        }


        /// <summary>
        /// This outputs the result from <see cref="CheckHit(Coord2D)"/> to the user
        /// </summary>
        /// <param name="Point">the user inputted coordinate point <param>
        public void TakeDamage(Coord2D Point)
        {
            if(CheckHit(Point))
            {
                Console.WriteLine($"You hit a ship at ({Point.x}, {Point.y})"); 
            }
            else
            {
                Console.WriteLine("You missed...");
            }
        }

        /// <summary>
        /// This method is pushed to the sub-classes, but is set for the specific type of ship
        /// </summary>
        /// <returns>string containing the ship's type</returns>
        public abstract string GetName();

        /// <summary>
        /// returns the ship's length which his health
        /// </summary>
        /// <returns>int which is the ship's length which his health</returns>
        public int GetMaxHealth()
        {
            return Length;
        }

        /// <summary>
        /// Updates the ship's health by subtracting it's length by its damagedpoints
        /// </summary>
        /// <returns>int of the ship's health</returns>
        public int GetCurrentHealth()
        {
            return Length - DamagedPoints.Count;
        }

        /// <summary>
        /// The Ship's damaged points if they are = to the ship's length
        /// </summary>
        /// <returns>Bool true if the ship's damagedpoints are equal to it's length</returns>
        public bool IsDead()
        {
            return DamagedPoints.Count == Length;
        }

        /// <summary>
        /// Essentially a debugging method
        /// Uses a ternary operator to see whether the directiontype is v or h then displays vertical or horizontal accordingly
        /// </summary>
        /// <returns>String message containing the newly made ship's info</returns>
        public string GetInfo ()
        {
            return $"{GetName()} at ({Position.x}, {Position.y}) facing {(Direction == DirectionType.v ? "vertical" : "horizontal")} with {GetCurrentHealth()} health remaining.";

        }

        private Coord2D[] RemovePointAtIndex(Coord2D[] inputArray, int indexToRemove)
        {
            var tempArray = new Coord2D[inputArray.Length - 1];

            // Copy all elements before the index to remove
            Array.Copy(inputArray, 0, tempArray, 0, indexToRemove);

            // Copy all elements after the index to remove
            Array.Copy(inputArray, indexToRemove + 1, tempArray, indexToRemove, inputArray.Length - indexToRemove - 1);

            return tempArray;
        }
    }
}

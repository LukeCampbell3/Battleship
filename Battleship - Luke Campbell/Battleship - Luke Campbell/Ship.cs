using CsvHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    public abstract class Ship : IHealth, IInfomatic
    {
        public Coord2D Position { get; set; }
        public byte Length { get; set; }
        public DirectionType Direction { get; set; }
        public Coord2D[] Points { get; set; }
        public List<Coord2D> DamagedPoints { get; set; } = new List<Coord2D>();


        public int health;

        protected Ship(Coord2D position, DirectionType direction, byte length)
        {
            Position = position;
            Direction = direction;
            Length = length;
            Points = CalculatePoints(position, direction, length);
            DamagedPoints = new List<Coord2D>();
        }

        private Coord2D[] CalculatePoints(Coord2D position, DirectionType direction, byte length)
        {
            Coord2D[] points = new Coord2D[length];
            for (int i = 0; i < length; i++)
            {
                points[i] = new Coord2D(
                    position.x + (direction == DirectionType.Horizontal ? i : 0),
                    position.y + (direction == DirectionType.Vertical ? i : 0)
                );
            }
            return points;
        }

        public bool CheckHit(Coord2D Point)
        {
            if (DamagedPoints.Contains(Point))
            {
                Console.WriteLine("You have already chosen this coordinate...");
                return false;
            }

            for (int i = 0; i < Points.Length; i++)
            {
                if (Point.Equals(Points[i])) // Check if the points are equal
                {
                    DamagedPoints.Add(Points[i]); // Add the damaged point to the list
                    Points[i] = default(Coord2D);
                    return true; // Return true since a hit was detected
                }
            }
            return false;
        }

        public void TakeDamage(Coord2D Point)
        {
            if(CheckHit(Point))
            {
                Console.WriteLine($"You hit a ship at ({Point.x}, {Point.y})"); 
            }
        }
        public abstract string GetName();

        public int GetMaxHealth()
        {
            return Length;
        }

        public int GetCurrentHealth()
        {
            return Length - DamagedPoints.Count;
        }

        public bool IsDead()
        {
            return DamagedPoints.Count == Length;
        }

        public string GetInfo ()
        {
            return $"{GetName()} at ({Position.x}, {Position.y}) facing {Direction} with {GetCurrentHealth()} health remaining.";
        }
    }
}

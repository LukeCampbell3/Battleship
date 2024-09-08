using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    /// <summary>
    /// PatrolBoat sub-class
    /// </summary>
    public class PatrolBoat : Ship
    {
        /// <summary>
        /// this is the class constructor that creates a PatrolBoat ship
        /// </summary>
        /// <param name="position">this is the file-parsed starting coordinate</param>
        /// <param name="direction">this is the file-parsed direction</param>
        public PatrolBoat(Coord2D position, DirectionType direction)
            : base(position, direction, 2) { }

        /// <summary>
        /// this overrides the GetName() so that for a PatrolBoat ship, "Patrol Boat" is shown as the type
        /// </summary>
        /// <returns>string of Patrol Boat for the PatrolBoat ship type</returns>
        public override string GetName() => "Patrol Boat";
    }
}

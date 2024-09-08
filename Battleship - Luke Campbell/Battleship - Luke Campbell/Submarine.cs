using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    /// <summary>
    /// Submarine sub-class
    /// </summary>
    public class Submarine : Ship
    {
        /// <summary>
        /// this is the class constructor that creates a Submarine ship
        /// </summary>
        /// <param name="position">this is the file-parsed starting coordinate</param>
        /// <param name="direction">this is the file-parsed direction</param>
        public Submarine(Coord2D position, DirectionType direction)
            : base(position, direction, 3) { }

        /// <summary>
        /// this overrides the GetName() so that for a Submarine ship, "Submarine" is shown as the type
        /// </summary>
        /// <returns>string of Submarine for the Submarine ship type</returns>
        public override string GetName() => "Submarine";
    }
}

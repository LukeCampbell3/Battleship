using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    /// <summary>
    /// carrier sub-class
    /// </summary>
    public class Carrier : Ship
    {
        /// <summary>
        /// this is the class constructor that creates a carrier ship
        /// </summary>
        /// <param name="position">this is the file-parsed starting coordinate</param>
        /// <param name="direction">this is the file-parsed direction</param>
        public Carrier(Coord2D position, DirectionType direction)
            : base(position, direction, 5) { }

        /// <summary>
        /// this overrides the GetName() so that for a carrier ship, "Carrier" is shown as the type
        /// </summary>
        /// <returns>string of Carrier for the carrier ship type</returns>
        public override string GetName() => "Carrier";
    }
}

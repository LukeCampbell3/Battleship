using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    /// <summary>
    /// Destroyer sub-class
    /// </summary>
    public class Destroyer : Ship
    {
        /// <summary>
        /// this is the class constructor that creates a Destroyer ship
        /// </summary>
        /// <param name="position">this is the file-parsed starting coordinate</param>
        /// <param name="direction">this is the file-parsed direction</param>
        public Destroyer(Coord2D position, DirectionType direction)
            : base(position, direction, 3) { }

        /// <summary>
        /// this overrides the GetName() so that for a Destroyer ship, "Destroyer" is shown as the type
        /// </summary>
        /// <returns>string of Destroyer for the carrier ship type</returns>
        public override string GetName() => "Destroyer";
    }
}

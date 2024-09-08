using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    /// <summary>
    /// BattleShip sub-class
    /// </summary>
    public class Battleship : Ship
    {
        /// <summary>
        /// this is the class constructor that creates a BattleShip ship
        /// </summary>
        /// <param name="position">this is the file-parsed starting coordinate</param>
        /// <param name="direction">this is the file-parsed direction</param>
        public Battleship(Coord2D position, DirectionType direction)
            : base(position, direction, 4) { }

        /// <summary>
        /// this overrides the GetName() so that for a Battleship ship, "BattleShip" is shown as the type
        /// </summary>
        /// <returns>string of Battleship for the BattleShip ship type</returns>
        public override string GetName() => "Battleship";
    }
}

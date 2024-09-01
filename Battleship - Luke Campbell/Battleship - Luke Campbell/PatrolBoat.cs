using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    public class PatrolBoat : Ship
    {
        public PatrolBoat(Coord2D position, DirectionType direction)
            : base(position, direction, 2) { }

        public override string GetName() => "Patrol Boat";
    }
}

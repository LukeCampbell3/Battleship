using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    public class Carrier : Ship
    {
        public Carrier(Coord2D position, DirectionType direction)
            : base(position, direction, 5) { }

        public override string GetName() => "Carrier";
    }
}

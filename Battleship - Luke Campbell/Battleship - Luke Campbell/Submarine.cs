using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    public class Submarine : Ship
    {
        public Submarine(Coord2D position, DirectionType direction)
            : base(position, direction, 3) { }

        public override string GetName() => "Submarine";
    }
}

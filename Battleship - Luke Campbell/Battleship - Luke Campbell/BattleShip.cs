using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    public class Battleship : Ship
    {
        public Battleship(Coord2D position, DirectionType direction)
            : base(position, direction, 4) { }

        public override string GetName() => "Battleship";
    }
}

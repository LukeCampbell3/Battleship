using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    public struct Coord2D
    {
        // x and y coordinates
        public int x, y;

        /// <summary>
        /// Creates a Coord2d object for coordinate handling
        /// </summary>
        /// <param name="x">ship / user's x coord</param>
        /// <param name="y">ship / user's y coord</param>
        public Coord2D(int x , int y)
        {
            this.x = x;
            this.y = y;
        }

    }
}

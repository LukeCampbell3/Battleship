using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    internal interface IInfomatic
    {
        /// <summary>
        /// essentially a .toString()
        /// This gets the status of the ship, this includes:
        /// ship type, location, , direction, health
        /// </summary>
        /// <returns>string containing the information above</returns>
        string GetInfo();
    }
}

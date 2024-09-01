using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship___Luke_Campbell
{
    internal interface IHealth
    {
        int GetMaxHealth();
        int GetCurrentHealth();
        bool IsDead();
    }
}

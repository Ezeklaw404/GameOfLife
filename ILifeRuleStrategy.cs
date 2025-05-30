using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    //Strategy Pattern, easy switch from default and other rules like the "Day & Night" ruleset
    public interface ILifeRuleStrategy
    {
        bool GetNextState(bool isAlive, byte liveNeighbors);
    }
}

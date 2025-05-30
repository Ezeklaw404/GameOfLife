using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class DayNightLifeStrategy : ILifeRuleStrategy
    {
        public bool GetNextState(bool isAlive, byte liveNeighbors)
        {
            if (isAlive)
                return liveNeighbors == 3 || liveNeighbors == 4 || liveNeighbors == 6 || liveNeighbors == 7 || liveNeighbors == 8;
            else
                return liveNeighbors == 3 || liveNeighbors == 6 || liveNeighbors == 7 || liveNeighbors == 8;
        }
    }
}

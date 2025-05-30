using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class ConwayStrategy : ILifeRuleStrategy
    {
        public bool GetNextState(bool isAlive, byte liveNeighbors)
        {
            if (isAlive)
                return liveNeighbors == 2 || liveNeighbors == 3;
            else
                return liveNeighbors == 3;
            
        }
    }
}

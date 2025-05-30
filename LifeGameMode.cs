using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class LifeGameMode
    {
        public ILifeRuleStrategy Strategy { get; set; }

        public LifeGameMode(ILifeRuleStrategy strategy)
        {
            Strategy = strategy;
        }

        public bool GetNextState(bool isAlive, byte neighbors)
        {
            return Strategy.GetNextState(isAlive, neighbors);
        }

    }
}

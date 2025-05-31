using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public static class CellFactory
    {
        private static readonly Dictionary<bool, ICellState> _states = new();
        public static ICellState GetState(bool isAlive)
        {
            if (!_states.TryGetValue(isAlive, out var state))
            {
                state = new CellState(isAlive);
                _states[isAlive] = state;
            }
            return state;
        }
    }
}

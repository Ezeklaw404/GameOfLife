using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class CellState : ICellState
    {
        public SolidColorBrush CellColor { get; }
        public bool IsAlive { get; }

        public CellState(bool isAlive)
        {
            IsAlive = isAlive;
            CellColor = IsAlive ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.WhiteSmoke);
        }

        public static readonly CellState Alive = new CellState(true);
        public static readonly CellState Dead = new CellState(false);
    }
}

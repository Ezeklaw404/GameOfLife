﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public interface ICellState
    {
        SolidColorBrush CellColor { get;}
        bool IsAlive { get;}
    }
}

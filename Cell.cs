using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class Cell : INotifyPropertyChanged
    {
        //Flyweight design pattern to help with the performance issues
        public ICellState State { get; private set; }

        public byte Row { get; private set; }
        public byte Col { get; private set; }
        private bool isAlive { get; set; }
        public bool IsAlive
        {
            get => isAlive;
            set
            {
                if (isAlive != value)
                {
                    isAlive = value;
                    State = CellFactory.GetState(IsAlive);
                    OnPropertyChanged(nameof(IsAlive));
                    OnPropertyChanged(nameof(CellColor)); // Notify that color has changed
                }
            }
        }

        public SolidColorBrush CellColor => State.CellColor;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Cell(byte row, byte col)
        {
            IsAlive = false;
            Row = row;
            Col = col;
            State = CellFactory.GetState(IsAlive);
        }




    }
}

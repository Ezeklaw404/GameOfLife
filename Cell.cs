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
        private bool isAlive { get; set; }


        public byte Row { get; private set; }
        public byte Col { get; private set; }
        public bool IsAlive
        {
            get => isAlive;
            set
            {
                if (isAlive != value)
                {
                    isAlive = value;
                    OnPropertyChanged(nameof(IsAlive));
                    OnPropertyChanged(nameof(CellColor)); // Notify that color has changed
                }
            }
        }

        public SolidColorBrush CellColor => IsAlive ? new SolidColorBrush(Colors.Black) : new SolidColorBrush(Colors.WhiteSmoke);

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public Cell(byte row, byte col)
        {
            IsAlive = false;
            Row = row;
            Col = col;
        }




    }
}

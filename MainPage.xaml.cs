using System.Collections.ObjectModel;


namespace GameOfLife
{
    public partial class MainPage : ContentPage
    {
        //change the "gameMode" here
        private ILifeRuleStrategy gameMode =
                    new ConwayStrategy();    //default
            //new DayNightLifeStrategy();



        private List<List<Cell>> LifeGridList = new();
        private GameOfLifeFacade game;

        private bool isPlaying = false;
        private Timer timer;


        public MainPage()
        {
            InitializeComponent();
            OnInit();
        }

        #region Setup
        public void OnInit()
        {
            ChangeGridSize();
            GenerateGrid();
            game = new GameOfLifeFacade(LifeGridList, gameMode);
        }

        public void ChangeGridSize()
        {
            for (int j = 0; j < (int)_GridRow.Value; j++)
            {
                _LifeGrid.AddRowDefinition(new RowDefinition { Height = new GridLength(15) });
                
                
            }
            for (int i = 0; i < (int)_GridColumn.Value; i++)
                _LifeGrid.AddColumnDefinition(new ColumnDefinition { Width = new GridLength(15) });

        }

        public void GenerateGrid()
        {
            for (int row = 0; row < (int)_GridRow.Value; row++) 
            {
                LifeGridList.Add(new List<Cell>());
                for (int col = 0; col < (int)_GridColumn.Value; col++) 
                {
                    AddButton(row, col);
                }
            }
        }

        private void AddButton(int row, int col)
        {
            var cell = new Cell((byte)row,(byte)col);
            var button = new Button
            {
                AutomationId = $"Life{row}_{col}",
                BorderColor = Color.FromArgb("#333333"),
                CornerRadius = 0,
                BorderWidth = .5,
                MinimumHeightRequest = 15,
                MinimumWidthRequest = 15
            };
            button.Clicked += (s, e) => cell.IsAlive = !cell.IsAlive;

            Grid.SetRow(button, row);
            Grid.SetColumn(button, col);

            button.BindingContext = cell;

            button.SetBinding(Button.BackgroundProperty, new Binding(nameof(Cell.CellColor), mode: BindingMode.OneWay));

            LifeGridList[row].Add(cell);
            _LifeGrid.Children.Add(button);
        }

        #endregion



        private void _GridSizeValueChanged(object sender, ValueChangedEventArgs e)
        {
            if (sender is Stepper stepper)
                if (stepper == _GridColumn) 
                    AdjustGridColumns((int)e.OldValue);
                else if (stepper == _GridRow) 
                    AdjustGridRows((int)e.OldValue);
        }

        private void AdjustGridRows(int oldRowCount)
        {
            int newRowCount = (int)_GridRow.Value;

            if (oldRowCount < newRowCount) // Add rows
            {
                for (int i = oldRowCount; i < newRowCount; i++)
                {
                    _LifeGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(15) });
                    LifeGridList.Add(new List<Cell>());

                    for (int j = 0; j < (int)_GridColumn.Value; j++)
                    {
                        AddButton(i, j);
                    }
                }
            }
            else if (oldRowCount > newRowCount) // Remove rows
            {
                for (int i = oldRowCount - 1; i >= newRowCount; i--)
                    RemoveRow(i);
            }
        }

        private void AdjustGridColumns(int oldColCount)
        {
            int newColCount = (int)_GridColumn.Value;

            if (oldColCount < newColCount) // Add columns
            {
                for (int j = oldColCount; j < newColCount; j++)
                {
                    _LifeGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(15) });

                    for (int i = 0; i < (int)_GridRow.Value; i++)
                    {
                        AddButton(i, j);
                    }
                }
            }
            else if (oldColCount > newColCount) // Remove columns
            {
                for (int j = oldColCount - 1; j >= newColCount; j--)
                    RemoveColumn(j);
            }
        }

        private void RemoveRow(int row)
        {
            _LifeGrid.Children
                .OfType<Button>()
                .Where(b => Grid.GetRow(b) == row)
                .ToList()
                .ForEach(b => _LifeGrid.Children.Remove(b));

            LifeGridList.RemoveAt(row);

            _LifeGrid.RowDefinitions.RemoveAt(row);
        }

        private void RemoveColumn(int col)
        {
            _LifeGrid.Children
                .OfType<Button>()
                .Where(b => Grid.GetColumn(b) == col)
                .ToList()
                .ForEach(b => _LifeGrid.Children.Remove(b));

            foreach (var row in LifeGridList)
            {
                if (col < row.Count)
                    row.RemoveAt(col);
            }

            if (col < _LifeGrid.ColumnDefinitions.Count)
                _LifeGrid.ColumnDefinitions.RemoveAt(col);
        }






        private void btnStep_Clicked(object sender, EventArgs e)
        {
            game.Step();
        }


        private void btnPlay_Clicked(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                timer?.Change(Timeout.Infinite, Timeout.Infinite); // Pause the timer
                timer?.Dispose();
                timer = null;
                btnPlay.Text = "Play";
            }
            else
            {
                timer = new Timer(_ => Device.BeginInvokeOnMainThread(game.Step),
                                  null, 0, 1000 / 15);
                btnPlay.Text = "Pause";
            }

            isPlaying = !isPlaying;
        }


        //    not yet implemented
        //private void btnClear_Clicked(object sender, EventArgs e)
        //{
        //    game.Clear();
        //}

        private void btn_Random(object sender, EventArgs e)
        {
            game.Randomize();
        }



    }

}

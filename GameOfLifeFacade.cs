using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    public class GameOfLifeFacade
    {
        private List<List<Cell>> lifeGrid;
        private LifeGameMode gameMode;


        // game logic hidden away here, seperating the UI and the logic
        public GameOfLifeFacade(List<List<Cell>> grid, ILifeRuleStrategy strategy)
        {
            this.lifeGrid = grid;
            gameMode = new LifeGameMode(strategy);
        }

        public void Step()
        {
            List<List<bool>> copy = new();

            foreach (var i in lifeGrid)
            {
                List<bool> copyInside = new();
                copy.Add(copyInside);

                foreach (var cell in i)
                {
                    copyInside.Add(cell.IsAlive);
                }
            }
            foreach (var i in lifeGrid)
            {
                foreach (var cell in i)
                {

                    byte neighbourCount = CheckNeighborState(cell, copy);
                    cell.IsAlive = gameMode.GetNextState(cell.IsAlive, neighbourCount);
                }
            }
        }

        public byte CheckNeighborState(Cell cell, List<List<bool>> copy)
        {
            byte count = 0;
            int rowCount = copy.Count;
            int colCount = copy[0].Count;

            if (cell.Row != 0 && cell.Col != 0 && copy[cell.Row - 1][cell.Col - 1]) count++; // top left
            if (cell.Row != 0 && copy[cell.Row - 1][cell.Col]) count++; // top
            if (cell.Row != 0 && cell.Col != colCount - 1 && copy[cell.Row - 1][cell.Col + 1]) count++; // top right
            if (cell.Col != colCount - 1 && copy[cell.Row][cell.Col + 1]) count++; // right
            if (cell.Row != rowCount - 1 && cell.Col != colCount - 1 && copy[cell.Row + 1][cell.Col + 1]) count++; // bottom right
            if (cell.Row != rowCount - 1 && copy[cell.Row + 1][cell.Col]) count++; // bottom
            if (cell.Row != rowCount - 1 && cell.Col != 0 && copy[cell.Row + 1][cell.Col - 1]) count++; // bottom left
            if (cell.Col != 0 && copy[cell.Row][cell.Col - 1]) count++; // left

            return count;
        }

        public void Clear()
        {
            foreach (var row in lifeGrid)
                foreach (var cell in row)
                    cell.IsAlive = false;
        }

        public void Randomize()
        {
            Random rand = new Random();
            foreach (var item in lifeGrid)
            {
                foreach (var item1 in item)
                {
                    item1.IsAlive = rand.NextDouble() < 0.35;
                }
            }
        }


    }
}

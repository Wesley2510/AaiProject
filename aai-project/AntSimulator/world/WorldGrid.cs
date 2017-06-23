using AntSimulator.entity;
using AntSimulator.util;
using System;
using System.Collections.Generic;

namespace AntSimulator.world
{
    public class WorldGrid
    {
        public List<BaseGameEntity>[][] Grid;
        public int GameColumns;
        public int GameRows;
        public int GridWidth;
        public int GridHeight;

        public WorldGrid(int gameColumns, int gameRows, int width, int height)
        {
            GameColumns = gameColumns;
            GameRows = gameRows;
            GridWidth = width / GameColumns;
            GridHeight = height / GameRows;
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            Grid = new List<BaseGameEntity>[GameColumns][];
            for (var x = 0; x < GameColumns; x++)
            {
                Grid[x] = new List<BaseGameEntity>[GameRows];
                for (var y = 0; y < GameRows; y++)
                {
                    Grid[x][y] = new List<BaseGameEntity>();
                }
            }
        }

        public List<BaseGameEntity> FindNeighbours(Vector2D pos, double size)
        {
            var neighbours = new List<BaseGameEntity>();
            var cellsDown =
                (int)Math.Ceiling((pos.Y + size) / GridWidth - pos.Y / GridHeight);
            var cellsUp =
                (int)Math.Floor((pos.Y - size) / GridHeight - pos.Y / GridHeight);
            var cellsLeft =
                (int)Math.Floor((pos.X - size) / GridWidth - pos.X / GridWidth);
            var cellsRight =
                (int)Math.Ceiling((pos.X + size) / GridWidth - pos.X / GridWidth);
            int collumn = ToCollumn(pos.X);
            int row = ToRow(pos.Y);
            for (var j = cellsUp + collumn; j <= cellsDown + collumn; j++)
            {
                if (j < 0 || j >= GameColumns) continue;
                for (var i = cellsLeft + row; i <= cellsRight + row; i++)
                {
                    if (i < 0 || i >= GameRows) continue;
                    neighbours.AddRange(Grid[j][i]);
                }
            }
            return neighbours;
        }

        private int ToRow(double position)
        {
            var result = (int)Math.Floor(position / GridHeight);
            result = result >= GameRows ? GameRows - 1 : result;
            result = result < 0 ? 0 : result;
            return result;
        }

        private int ToCollumn(double position)
        {
            var result = (int)Math.Floor(position / GridWidth);
            result = result >= GameColumns ? GameColumns - 1 : result;
            result = result < 0 ? 0 : result;
            return result;
        }

        public void Add(BaseGameEntity entity)
        {
            var column = (int)(entity.Pos.X / GridWidth);
            var row = (int)(entity.Pos.Y / GridHeight);
            Grid[column][row].Add(entity);
        }

        public void Remove(BaseGameEntity entity)
        {
            var column = (int)(entity.Pos.X / GridWidth);
            var row = (int)(entity.Pos.Y / GridHeight);
            Grid[column][row].Remove(entity);
        }
    }
}
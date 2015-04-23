﻿/*
 * Author : Yannick R. Brodard
 * File name : HMap.cs
 * Version : 0.1.201504231041
 * Description : The map class, creates a map
 */
/* Helped by : http://www.csharpprogramming.tips/2013/07/Rouge-like-dungeon-generation.html */

#region USING STATEMENTS
using HelProject.Tools;
using System;
#endregion

namespace HelProject.GameWorld
{
    /// <summary>
    /// Used the create a map for the game
    /// </summary>
    public class HMap
    {
        #region CONSTANTS
        private const int WALKABLE_AJDACENT_WALL_QUANTITY_LIMIT = 5;
        private const int NONWALKABLE_AJDACENT_WALL_QUANTITY_LIMIT = 4;
        private const int DEFAULT_NONWALKABLE_CELLS_PERCENTAGE = 40;
        private const int MINIMUM_HEIGHT = 10;
        private const int MINIMUM_WIDTH = 10;
        private const int MAXIMUM_HEIGHT = 200;
        private const int MAXIMUM_WIDTH = 200;
        #endregion

        #region ATTRIBUTES
        private Random rand = new Random(); // Randomizer
        private HObject[,] _cells; // Cells of the map
        private int _height; // Height of the map
        private int _width; // Width of the map
        private int _walkableSpacePercentage; // Percentage of walkable area in the map
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// Percentage of walkable area in the map
        /// </summary>
        public int WalkableSpacePercentage
        {
            get { return _walkableSpacePercentage; }
            private set { _walkableSpacePercentage = value; }
        }

        /// <summary>
        /// Height of the map
        /// </summary>
        public int Height
        {
            get { return _height; }
            private set { _height = value; }
        }

        /// <summary>
        /// Width of the map
        /// </summary>
        public int Width
        {
            get { return _width; }
            private set { _width = value; }
        }

        /// <summary>
        /// Cells of the map
        /// </summary>
        private HObject[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Creates a map full of non-walkable cells
        /// </summary>
        /// <param name="height">Height of the map</param>
        /// <param name="width">Width of the map</param>
        /// <param name="walkableSpacePercentage">Amount (percentage) of non-walkable area in the map for random filling</param>
        /// <remarks>
        /// Use the 'Make' methods to transform the map
        /// </remarks>
        public HMap(int height, int width, int walkableSpacePercentage = HMap.DEFAULT_NONWALKABLE_CELLS_PERCENTAGE)
        {
            this.Height = Math.Min(HMap.MAXIMUM_HEIGHT, Math.Max(height, HMap.MINIMUM_HEIGHT));
            this.Width = Math.Min(HMap.MAXIMUM_WIDTH, Math.Max(width, HMap.MINIMUM_WIDTH));
            this.WalkableSpacePercentage = walkableSpacePercentage;

            this.ClearMap();
            this.MakeFullMap();
        }
        #endregion

        #region PUBLIC METHODS
        /// <summary>
        /// Fills the map randomly with borders
        /// </summary>
        public void MakeRandomlyFilledMap()
        {
            this.ClearMap();

            int mapMiddle = 0; // tmp variable

            // X is only created once
            for (int x = 0, y = 0; y < this.Height; y++)
            {
                for (x = 0; x < this.Width; x++)
                {
                    // Fills the edges with walls
                    if (x == 0)
                    {
                        this.Cells[x, y] = new HObject(false, new FPosition(x, y));
                    }
                    else if (y == 0)
                    {
                        this.Cells[x, y] = new HObject(false, new FPosition(x, y));
                    }
                    else if (x == this.Width - 1)
                    {
                        this.Cells[x, y] = new HObject(false, new FPosition(x, y));
                    }
                    else if (y == this.Height - 1)
                    {
                        this.Cells[x, y] = new HObject(false, new FPosition(x, y));
                    }
                    else
                    {
                        mapMiddle = (this.Height / 2);
                        // the middle always has a walkable cell for space logic
                        if (y == mapMiddle)
                        {
                            this.Cells[x, y] = new HObject(true, new FPosition(x, y));
                        }
                        else
                        {
                            // Fills the rest with a random ratio
                            this.Cells[x, y] = new HObject(!this.RandomPercent(this.WalkableSpacePercentage), new FPosition(x, y));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Clears the map, all the cells are null
        /// </summary>
        public void ClearMap()
        {
            this.Cells = new HObject[this.Width, this.Height];
        }

        /// <summary>
        /// Creates a map full of non-walkable cells
        /// </summary>
        public void MakeFullMap()
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    this.Cells[x, y] = new HObject(false, new FPosition(x, y));
                }
            }
        }

        /// <summary>
        /// Transforms the cells in the map to correspond to a cavern
        /// </summary>
        /// <remarks>
        /// It is best to call the RandomFillMap method before this one to
        /// get the best results
        /// </remarks>
        public void MakeCaverns()
        {
            for (int x = 0, y = 0; y < this.Height; y++)
            {
                for (x = 0; x < this.Width; x++)
                {
                    this.SetCell(x, y, PlaceCellLogic(x, y));
                }
            }
        }

        /// <summary>
        /// Places a walkable cell depending on it's neighbors
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y position of the cell</param>
        /// <param name="cell"></param>
        public bool PlaceCellLogic(int x, int y)
        {
            int nbUnwalkableCells = this.GetNumberOfAdjacentUnwalkableCells(x, y, 1, 1);

            HObject cell = this.GetCell(x, y);
            // Checks if the cell is non-walkable
            if (cell.IsWalkable == false)
            {
                // if their is too much non-walkable cells around it
                if (nbUnwalkableCells >= HMap.NONWALKABLE_AJDACENT_WALL_QUANTITY_LIMIT)
                {
                    return false;
                }

                if (nbUnwalkableCells < 2)
                {
                    return true;
                }

            }
            else // if it's walkable
            {
                // if their is too much walls around it, smooth it
                if (nbUnwalkableCells >= HMap.WALKABLE_AJDACENT_WALL_QUANTITY_LIMIT)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Gets the number of adjacent non-walkable cells around the specified cell
        /// </summary>
        /// <param name="x">X position of the specified cell</param>
        /// <param name="y">Y position of the specified cell</param>
        /// <param name="scopeX">X scope to scan around the specified cell</param>
        /// <param name="scopeY">Y scope to scan around the specified cell</param>
        /// <returns>numbers of non-walkable cells around the specified cell</returns>
        public int GetNumberOfAdjacentUnwalkableCells(int x, int y, int scopeX, int scopeY)
        {
            // INITIALISATION
            int startX = x - scopeX;
            int startY = y - scopeY;
            int endX = x + scopeX;
            int endY = y + scopeY;

            int iX = startX;
            int iY = startY;

            int wallCounter = 0;

            for (iY = startY; iY <= endY; iY++)
            {
                for (iX = startX; iX <= endX; iX++)
                {
                    if (!(iX == x && iY == y))
                    {
                        if (this.IsCellNonwalkable(iX, iY))
                        {
                            wallCounter += 1;
                        }
                    }
                }
            }

            return wallCounter;
        }

        /// <summary>
        /// Verifies if the specified cell is walkable
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y position of the cell</param>
        /// <returns>Result in boolean</returns>
        public bool IsCellNonwalkable(int x, int y)
        {
            // Verifies if the cell is out of bounds
            if (this.IsCellOutOfBounds(x, y))
            {
                return true; // Consider it non-walkable if it is
            }

            HObject cell = this.GetCell(x, y);

            if (cell.IsWalkable == false)
            {
                return true;
            }

            if (cell.IsWalkable == true)
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Verifies if the specified cell is out of bound
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y position of the cell</param>
        /// <returns>Result in boolean</returns>
        public bool IsCellOutOfBounds(int x, int y)
        {
            if (x < 0 || y < 0)
            {
                return true;
            }
            else if (x > this.Width - 1 || y > this.Height - 1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets a copy of the specified cell
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y position of the cell</param>
        /// <returns>Copy of the cell</returns>
        public HObject GetCellCopy(int x, int y)
        {
            return new HObject(this.Cells[x, y].IsWalkable, this.Cells[x, y].Position);
        }

        /// <summary>
        /// Gets the specified cell
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y position of the cell</param>
        /// <returns>Specified cell</returns>
        public HObject GetCell(int x, int y)
        {
            return this.Cells[x, y];
        }

        /// <summary>
        /// Sets the cell
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y position of the cell</param>
        /// <param name="isWalkable">Specify the walkability of the cell</param>
        public void SetCell(int x, int y, bool isWalkable)
        {
            this.Cells[x, y].IsWalkable = isWalkable;
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Returns a bool depending on a given percentage
        /// </summary>
        /// <param name="percent">Percentage for it to be true</param>
        /// <returns></returns>
        private bool RandomPercent(int percent)
        {
            if (percent >= rand.Next(1, 101))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}

/*
 * Author : Yannick R. Brodard
 * File name : HMap.cs
 * Version : 0.4.201505120916
 * Description : The map class, creates a map
 */
/* Helped by : http://www.csharpprogramming.tips/2013/07/Rouge-like-dungeon-generation.html */

#region USING STATEMENTS
using HelProject.Tools;
using HelProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
#endregion

namespace HelProject.GameWorld.Map
{
    /// <summary>
    /// Used the create a map for the game
    /// </summary>
    public class HMap
    {
        #region CONSTANTS
        protected const int WALKABLE_AJDACENT_WALL_QUANTITY_LIMIT = 5;
        protected const int NONWALKABLE_AJDACENT_WALL_QUANTITY_LIMIT = 4;
        protected const int DEFAULT_NONWALKABLE_CELLS_PERCENTAGE = 45;
        protected const int DEFAULT_SMOOTHNESS = 5;
        protected const int MINIMUM_SMOOTHNESS = 1;
        protected const int MINIMUM_HEIGHT = 10;
        protected const int MINIMUM_WIDTH = 10;
        protected const int MAXIMUM_HEIGHT = 800;
        protected const int MAXIMUM_WIDTH = 800;
        #endregion

        #region ATTRIBUTES
        private Random rand = new Random(); // Randomizer
        private HCell[,] _cells; // Cells of the map
        private int _height; // Height of the map
        private int _width; // Width of the map
        private int _nonWalkableSpacePercentage; // Percentage of walkable area in the map
        private ContentManager _content; // content manager
        private float _scale;
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// Scale of the map
        /// </summary>
        public float Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        /// <summary>
        /// Percentage of walkable area in the map
        /// </summary>
        public int NonWalkableSpacePercentage
        {
            get { return _nonWalkableSpacePercentage; }
            private set { _nonWalkableSpacePercentage = value; }
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
        private HCell[,] Cells
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
        /// <param name="nonWalkableSpacePercentage">Amount (percentage) of non-walkable area in the map for random filling</param>
        /// <remarks>
        /// Use the 'Make' methods to transform the map
        /// </remarks>
        public HMap(int height, int width, float scale = 1.0f, int nonWalkableSpacePercentage = HMap.DEFAULT_NONWALKABLE_CELLS_PERCENTAGE)
        {
            this.Height = Math.Min(HMap.MAXIMUM_HEIGHT, Math.Max(height, HMap.MINIMUM_HEIGHT));
            this.Width = Math.Min(HMap.MAXIMUM_WIDTH, Math.Max(width, HMap.MINIMUM_WIDTH));
            this.NonWalkableSpacePercentage = nonWalkableSpacePercentage;
            this.Scale = scale;

            this.ClearMap();
            this.MakeRandomlyFilledMap();
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
                        this.Cells[x, y] = new HCell(false, new Vector2(x, y));
                    }
                    else if (y == 0)
                    {
                        this.Cells[x, y] = new HCell(false, new Vector2(x, y));
                    }
                    else if (x == this.Width - 1)
                    {
                        this.Cells[x, y] = new HCell(false, new Vector2(x, y));
                    }
                    else if (y == this.Height - 1)
                    {
                        this.Cells[x, y] = new HCell(false, new Vector2(x, y));
                    }
                    else
                    {
                        mapMiddle = (this.Height / 2);
                        // the middle always has a walkable cell for space logic
                        if (y == mapMiddle)
                        {
                            this.Cells[x, y] = new HCell(true, new Vector2(x, y));
                        }
                        else
                        {
                            // Fills the rest with a random ratio
                            this.Cells[x, y] = new HCell(!this.RandomPercent(this.NonWalkableSpacePercentage), new Vector2(x, y));
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
            this.Cells = new HCell[this.Width, this.Height];
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
                    this.Cells[x, y] = new HCell(false, new Vector2(x, y));
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
        public void MakeCaverns(int smoothness = DEFAULT_SMOOTHNESS)
        {
            //smoothness = Math.Max(MINIMUM_SMOOTHNESS, smoothness);

            for (int i = 0; i < smoothness; i++) // repeating the carverns algo makes the caverns smoother on the edges
            {                                    // and gives a more natural look
                HCell[,] grid = new HCell[this.Width, this.Height];
                for (int x = 0, y = 0; y < this.Height; y++)
                {
                    for (x = 0; x < this.Width; x++)
                    {
                        grid[x, y] = new HCell(PlaceCellLogic(x, y), this.Cells[x, y].Position);
                        //this.SetCell(x, y, PlaceCellLogic(x, y));
                    }
                }
                for (int x = 0, y = 0; y < this.Height; y++)
                {
                    for (x = 0; x < this.Width; x++)
                    {
                        this.Cells[x, y] = grid[x, y];
                    }
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

            HCell cell = this.GetCell(x, y);
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

        public List<HCell> GetAdjacentUnwalkableCells(int x, int y, int scopeX, int scopeY)
        {
            List<HCell> unwalkableCells = new List<HCell>();

            int startX = x - scopeX;
            int startY = y - scopeY;
            int endX = x + scopeX;
            int endY = y + scopeY;

            int iX = startX;
            int iY = startY;

            for (iY = startY; iY <= endY; iY++)
            {
                for (iX = startX; iX <= endX; iX++)
                {
                    if (!(iX == x && iY == y))
                    {
                        if (this.IsCellNonwalkable(iX, iY))
                        {
                            unwalkableCells.Add(this.GetCell(iX, iY));
                        }
                    }
                }
            }

            return unwalkableCells;
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

            HCell cell = this.GetCell(x, y);

            if (cell.IsWalkable == false)
            {
                return true;
            }
            else
            {
                return false;
            }
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
        public HCell GetCellCopy(int x, int y)
        {
            return new HCell(this.Cells[x, y].IsWalkable, this.Cells[x, y].Position);
        }

        /// <summary>
        /// Gets the specified cell
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y position of the cell</param>
        /// <returns>Specified cell</returns>
        public HCell GetCell(int x, int y)
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

        /// <summary>
        /// Loads the content
        /// </summary>
        public void LoadContent()
        {
            this._content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");
        }

        /// <summary>
        /// Unloads the content
        /// </summary>
        public void UnloadContent()
        {
            this._content.Unload();
        }

        /// <summary>
        /// Draws the map
        /// </summary>
        /// <param name="spriteBatch">Spritebatch for drawing</param>
        /// <param name="camera">Camera to determine where to draw</param>
        public void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            int sizeOfSprites = HCell.TILE_SIZE;
            Texture2D _floor = TextureManager.Instance.LoadedTextures["floor"];
            Texture2D _wall = TextureManager.Instance.LoadedTextures["wall"];

            // determins the start point for the drawing, so it doesn't draw useless cells
            Point startPoint = new Point((int)camera.Position.X - (int)(camera.Width / 2 / sizeOfSprites + 1),
                                     (int)camera.Position.Y - (int)(camera.Height / 2 / sizeOfSprites) - 1);

            // determins the end point for the drawing, so it doesn't draw useless cells
            Point endPoint = new Point((int)camera.Position.X + (int)(camera.Width / 2 / sizeOfSprites + 1),
                                     (int)camera.Position.Y + (int)(camera.Height / 2 / sizeOfSprites + 2));

            // For each cell from the start to end point, it draws it
            for (int y = startPoint.Y; y < endPoint.Y; y++)
            {
                for (int x = startPoint.X; x < endPoint.X; x++)
                {
                    if (!this.IsCellOutOfBounds(x, y))
                    {
                        HCell cell = this.GetCell(x, y);
                        Vector2 position = ScreenManager.Instance.GetCorrectScreenPosition(cell.Position, camera.Position);

                        if (cell.IsWalkable)
                        {
                            spriteBatch.Draw(_floor, position, null, null, null, 0.0f, new Vector2(this.Scale, this.Scale), Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(_wall, position, null, null, null, 0.0f, new Vector2(this.Scale, this.Scale), Color.White);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a random walkable area
        /// </summary>
        /// <returns></returns>
        public Vector2 GetRandomSpawnPoint()
        {
            bool foundPosition = false;
            Vector2 position = Vector2.One;
            while (!foundPosition)
            {
                int rX = rand.Next(0, this.Width);
                int rY = rand.Next(0, this.Height);

                HCell foundCell = this.GetCell(rX, rY);

                if (foundCell.IsWalkable)
                {
                    int unWalkableCells = this.GetNumberOfAdjacentUnwalkableCells(rX, rY, 1, 1);
                    if (unWalkableCells == 0)
                    {
                        foundPosition = true;
                        position = foundCell.Position;
                    }
                }
            }

            return position;
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

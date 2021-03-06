﻿/*
 * Author : Yannick R. Brodard
 * File name : HMap.cs
 * Version : 0.5.201505151012
 * Description : The map class, creates a map
 */
/* Helped by : http://www.csharpprogramming.tips/2013/07/Rouge-like-dungeon-generation.html */

#region USING STATEMENTS
using HelProject.GameWorld.Entities;
using HelProject.Tools;
using HelProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
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
        private List<HHostile> _hostiles; // enemies of the map
        private List<HItem> _onFloorItems;
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// Items currently on the floor
        /// </summary>
        public List<HItem> OnFloorItems
        {
            get { return _onFloorItems; }
            set { _onFloorItems = value; }
        }

        /// <summary>
        /// Enemies present in the map
        /// </summary>
        public List<HHostile> Hostiles
        {
            get { return _hostiles; }
            set { _hostiles = value; }
        }

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
        public HCell[,] Cells
        {
            get { return _cells; }
            private set { _cells = value; }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Creates a map from given cells
        /// </summary>
        /// <param name="cells">Cells of the map</param>
        /// <param name="scale">Scale of the map</param>
        public HMap(HCell[,] cells, float scale = 1.0f)
        {
            this.Width = cells.GetLength(0);
            this.Height = cells.GetLength(1);
            this.NonWalkableSpacePercentage = 0;
            this.Scale = scale;
            this.Cells = cells;
        }

        /// <summary>
        /// Creates a map full of non-walkable cells
        /// </summary>
        /// <param name="height">Height of the map</param>
        /// <param name="width">Width of the map</param>
        /// <param name="nonWalkableSpacePercentage">Amount (percentage) of non-walkable area in the map for random filling</param>
        /// <remarks>
        /// Use the 'Make' methods to transform the map
        /// </remarks>
        public HMap(int width, int height, float scale = 1.0f, int nonWalkableSpacePercentage = HMap.DEFAULT_NONWALKABLE_CELLS_PERCENTAGE)
        {
            this.Height = Math.Min(HMap.MAXIMUM_HEIGHT, Math.Max(height, HMap.MINIMUM_HEIGHT));
            this.Width = Math.Min(HMap.MAXIMUM_WIDTH, Math.Max(width, HMap.MINIMUM_WIDTH));
            this.NonWalkableSpacePercentage = nonWalkableSpacePercentage;
            this.Scale = scale;
            this.Hostiles = new List<HHostile>();
            this.OnFloorItems = new List<HItem>();

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

        /// <summary>
        /// Gets the adjacent non-walkable cells around the given point and scope
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="scopeX">Scope on the X axis</param>
        /// <param name="scopeY">Scope on the Y axis</param>
        /// <returns>A list of the adjacent non-walkable cells</returns>
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
        /// Gets the adjacent cells of the position
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        /// <param name="scopeX">X scope</param>
        /// <param name="scopeY">Y scope</param>
        /// <param name="includeDesignatedCell">Include the specified cell ?</param>
        /// <returns>Adjacent cells</returns>
        public List<HCell> GetAdjacentCells(int x, int y, int scopeX, int scopeY, bool includeDesignatedCell = false)
        {
            List<HCell> adjacentcells = new List<HCell>();

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
                        adjacentcells.Add(this.GetCell(iX, iY));
                    }
                    else
                    {
                        if (includeDesignatedCell)
                        {
                            adjacentcells.Add(this.GetCell(iX, iY));
                        }
                    }
                }
            }

            return adjacentcells;
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
            if (this.Hostiles == null)
            {
                this.Hostiles = new List<HHostile>();
            }

            if (this.OnFloorItems == null)
                this.OnFloorItems = new List<HItem>();
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

                        spriteBatch.Draw(TextureManager.Instance.GetTexture(cell.Type), position, null, null, null, 0.0f, new Vector2(this.Scale, this.Scale), Color.White);
                    }
                }
            }

            FRectangle limits = new FRectangle(startPoint.X, startPoint.Y, endPoint.X - startPoint.X, endPoint.Y - endPoint.Y);
            this.DrawItems(spriteBatch, camera, limits);
        }

        /// <summary>
        /// Draws all the items that are on the floor of the map
        /// </summary>
        /// <param name="spriteBatch">Sprite batch</param>
        /// <param name="camera">Camera of the game</param>
        /// <param name="limits">Limits where the item will be drawn</param>
        public void DrawItems(SpriteBatch spriteBatch, Camera camera, FRectangle limits)
        {
            int nbrItem = this.OnFloorItems.Count;
            for (int i = 0; i < nbrItem; i++)
            {
                this.OnFloorItems[i].Draw(spriteBatch);
            }
        }

        /// <summary>
        /// Gets a random walkable area
        /// </summary>
        /// <returns>Position of the walkable position</returns>
        public Vector2 GetRandomFloorPoint()
        {
            bool foundPosition = false;
            Vector2 position = Vector2.One;
            while (!foundPosition)
            {
                int rX = rand.Next(0, this.Width);
                int rY = rand.Next(0, this.Height);

                HCell foundCell = this.GetCell(rX, rY);

                if (foundCell.IsWalkable && foundCell.Type.Contains("floor"))
                {
                    int unWalkableCells = this.GetNumberOfAdjacentUnwalkableCells(rX, rY, 1, 1);
                    if (unWalkableCells == 0)
                    {
                        int nbHostiles = this.Hostiles.Count;
                        bool noIntersection = true;
                        for (int i = 0; i < nbHostiles; i++)
                        {
                            if (foundCell.Bounds.Intersects(this.Hostiles[i].Bounds))
                            {
                                noIntersection = false;
                            }
                        }

                        if (PlayScreen.Instance.PlayableCharacter != null)
                        {
                            if (PlayScreen.Instance.PlayableCharacter.Bounds.Intersects(foundCell.Bounds))
                                noIntersection = false;
                        }

                        if (noIntersection)
                        {
                            foundPosition = true;
                            position = foundCell.Position;
                        }
                    }
                }
            }

            return position;
        }

        /// <summary>
        /// Decorates the map
        /// </summary>
        public void DecorateMap()
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    if (this.GetNumberOfAdjacentUnwalkableCells(x, y, 1, 1) >= 8)
                    {
                        this.Cells[x, y].Type = "wallblack";
                    }
                    else
                    {
                        if (this.Cells[x, y].Type == "wall")
                        {
                            if (this.GetLeftCell(x, y) != null && this.GetLeftCell(x, y).IsWalkable == false &&
                                this.GetRightCell(x, y) != null && this.GetRightCell(x, y).IsWalkable == false)
                            {
                                this.Cells[x, y].Type = "wallnoborders";

                                if (this.GetBottomCell(x, y) != null && this.GetBottomCell(x, y).IsWalkable == false)
                                    this.Cells[x, y].Type = "wallnobordersndb";
                            }
                            else
                            {
                                if (this.GetLeftCell(x, y) != null && this.GetLeftCell(x, y).IsWalkable == false)
                                {
                                    this.Cells[x, y].Type = "wallnoleftborder";

                                    if (this.GetBottomCell(x, y) != null && this.GetBottomCell(x, y).IsWalkable == false)
                                        this.Cells[x, y].Type = "wallnoleftborderndb";
                                }
                                else
                                {
                                    if (this.GetRightCell(x, y) != null && this.GetRightCell(x, y).IsWalkable == false)
                                    {
                                        this.Cells[x, y].Type = "wallnorightborder";

                                        if (this.GetBottomCell(x, y) != null && this.GetBottomCell(x, y).IsWalkable == false)
                                            this.Cells[x, y].Type = "wallnorightborderndb";
                                    }
                                }
                            }
                        }
                        if (this.Cells[x, y].Type == "wall" && this.GetBottomCell(x, y) != null && this.GetBottomCell(x, y).IsWalkable == false)
                        {
                            this.Cells[x, y].Type = "wallndb";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets the top cell
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y postiion of the cell</param>
        /// <returns>Top cell</returns>
        public HCell GetTopCell(int x, int y)
        {
            if (this.IsCellOutOfBounds(x, y - 1))
                return null;
            return this.GetCell(x, y - 1);
        }

        /// <summary>
        /// Gets the bottom cell
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y postiion of the cell</param>
        /// <returns>Bottom cell</returns>
        public HCell GetBottomCell(int x, int y)
        {
            if (this.IsCellOutOfBounds(x, y + 1))
                return null;
            return this.GetCell(x, y + 1);
        }

        /// <summary>
        /// Gets the Left cell
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y postiion of the cell</param>
        /// <returns>Left cell</returns>
        public HCell GetLeftCell(int x, int y)
        {
            if (this.IsCellOutOfBounds(x - 1, y))
                return null;
            return this.GetCell(x - 1, y);
        }

        /// <summary>
        /// Gets the Right cell
        /// </summary>
        /// <param name="x">X position of the cell</param>
        /// <param name="y">Y postiion of the cell</param>
        /// <returns>Right cell</returns>
        public HCell GetRightCell(int x, int y)
        {
            if (this.IsCellOutOfBounds(x + 1, y))
                return null;
            return this.GetCell(x + 1, y);
        }
        #endregion

        #region PRIVATE METHODS

        /// <summary>
        /// Returns a bool depending on a given percentage
        /// </summary>
        /// <param name="percent">Percentage for it to be true</param>
        /// <returns>True or false depending on the given percentage</returns>
        private bool RandomPercent(int percent)
        {
            if (percent >= rand.Next(1, 101))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region STATIC METHODS
        /// <summary>
        /// Save the cells in an XML file
        /// </summary>
        /// <param name="path">Path of the file</param>
        public static void SaveToXml(HMap map, string path)
        {
            XmlTextWriter writer = null;
            writer = new XmlTextWriter(path, UTF8Encoding.Default);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartElement("Map");
            writer.WriteStartElement("Dimensions");
            writer.WriteElementString("Width", map.Width.ToString());
            writer.WriteElementString("Height", map.Height.ToString());
            writer.WriteEndElement();
            writer.WriteStartElement("Cells");

            for (int y = 0; y < map.Height; y++)
            {
                for (int x = 0; x < map.Width; x++)
                {
                    writer.WriteStartElement("Cell");
                    writer.WriteElementString("X", map.GetCell(x, y).Position.X.ToString());
                    writer.WriteElementString("Y", map.GetCell(x, y).Position.Y.ToString());
                    writer.WriteElementString("IsWalkable", map.GetCell(x, y).IsWalkable.ToString());
                    writer.WriteElementString("Type", map.GetCell(x, y).Type);
                    writer.WriteEndElement();
                }
            }

            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Close();
        }

        /// <summary>
        /// Loads a map from an xml file
        /// </summary>
        /// <param name="path">Path of the file</param>
        /// <returns>Cells of the map</returns>
        public static HCell[,] LoadFromXml(string path)
        {
            XmlTextReader reader = new XmlTextReader(path);

            string currentElement = String.Empty;
            int w = 0;
            int h = 0;
            List<int> posXs = new List<int>();
            List<int> posYs = new List<int>();
            List<bool> isWalkables = new List<bool>();
            List<string> types = new List<string>();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        currentElement = reader.Name;
                        break;
                    case XmlNodeType.Text:
                        if (currentElement == "Width")
                            w = Convert.ToInt32(reader.Value);
                        if (currentElement == "Height")
                            h = Convert.ToInt32(reader.Value);
                        if (currentElement == "X")
                            posXs.Add(Convert.ToInt32(reader.Value));
                        if (currentElement == "Y")
                            posYs.Add(Convert.ToInt32(reader.Value));
                        if (currentElement == "IsWalkable")
                            isWalkables.Add(Convert.ToBoolean(reader.Value));
                        if (currentElement == "Type")
                            types.Add(reader.Value);
                        break;
                    default:
                        break;
                }
            }

            HCell[,] cells = new HCell[w, h];

            for (int i = 0; i < w * h; i++)
            {
                int x = posXs[i];
                int y = posYs[i];
                bool isWalkable = isWalkables[i];
                string type = types[i];
                cells[x, y] = new HCell(isWalkable, new Vector2(x, y), type);
            }

            return cells;
        }
        #endregion
    }
}

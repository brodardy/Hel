using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.GameWorld
{
    public class HMap
    {
        private HObject[,] _cells;

        public HObject[,] Cells
        {
            get { return _cells; }
            set { _cells = value; }
        }

        public HMap(int height, int width, int maximumNumberOfRooms, int maximumSizeOfRoom, int minimumSizeOfRoom)
        {
            this.Cells = new HObject[height, width];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    this.Cells[x, y] = null;
                }
            }


        }
    }
}

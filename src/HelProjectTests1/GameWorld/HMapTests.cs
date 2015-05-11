using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelProject.GameWorld;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HelProject.Tools;
using HelProject.GameWorld.Map;

namespace HelProject.GameWorld.Tests
{
    [TestClass()]
    public class HMapTests
    {
        [TestMethod()]
        public void HMapTest()
        {
            HMap target = new HMap(10, 10);
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Assert.AreEqual(false, target.GetCell(x, y).IsWalkable);
                    Assert.AreEqual((float)x, target.GetCell(x, y).Position.X);
                    Assert.AreEqual((float)y, target.GetCell(x, y).Position.Y);
                }
            }

            target = new HMap(-10, -10);
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Assert.AreEqual(false, target.GetCell(x, y).IsWalkable);
                    Assert.AreEqual((float)x, target.GetCell(x, y).Position.X);
                    Assert.AreEqual((float)y, target.GetCell(x, y).Position.Y);
                }
            }

            target = new HMap(210, 210);
            for (int y = 0; y < target.Height; y++)
            {
                for (int x = 0; x < target.Width; x++)
                {
                    Assert.AreEqual(false, target.GetCell(x, y).IsWalkable);
                    Assert.AreEqual((float)x, target.GetCell(x, y).Position.X);
                    Assert.AreEqual((float)y, target.GetCell(x, y).Position.Y);
                }
            }
        }

        [TestMethod()]
        public void RandomFillMapTest()
        {
            HMap target = new HMap(100, 100, 40);
            int nonwalkableCounter = 0;
            target.MakeRandomlyFilledMap();
            for (int y = 0; y < 100; y++)
            {
                for (int x = 0; x < 100; x++)
                {
                    if (y == 0 || x == 0 || y == 99 || x == 99)
                    {
                        Assert.AreEqual(false, target.GetCell(x, y).IsWalkable);
                    }
                    else
                    {
                        nonwalkableCounter += (target.GetCell(x, y).IsWalkable ? 0 : 1);
                    }
                }
            }
            float nonwalkableRatio = (float)nonwalkableCounter / (9600.0f); // removed the edges from total
            if (!(nonwalkableRatio > 0.375f && nonwalkableRatio < 0.425f))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void ClearMapTest()
        {
            HMap target = new HMap(10, 10);
            target.ClearMap();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Assert.AreEqual(null, target.GetCell(x, y));
                }
            }
        }

        [TestMethod()]
        public void MakeFullMapTest()
        {
            HMap target = new HMap(10, 10);
            target.MakeFullMap();
            for (int y = 0; y < 10; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    Assert.AreEqual(false, target.GetCell(x, y).IsWalkable);
                }
            }
        }

        [TestMethod()]
        public void MakeCavernsTest()
        {
            for (int i = 0; i < 10; i++) // do the test 10 times
            {
                HMap target = new HMap(30, 30, 40);
                target.MakeRandomlyFilledMap();
                bool hasFreeCells = false;
                for (int y = 0; y < 30; y++)
                {
                    for (int x = 0; x < 30; x++)
                    {
                        if (y == 0 || x == 0 || y == 29 || x == 29)
                        {
                            Assert.AreEqual(false, target.GetCell(x, y).IsWalkable);
                        }

                        if (target.GetCell(x, y).IsWalkable)
                            hasFreeCells = true;
                    }
                }
                Assert.AreEqual(true, hasFreeCells);
            }
        }

        [TestMethod()]
        public void PlaceUnwalkableCellLogicTest()
        {
            
        }

        [TestMethod()]
        public void GetNumberOfAdjacentUnwalkableCellsTest()
        {
            
        }

        [TestMethod()]
        public void IsCellWalkableTest()
        {
            
        }

        [TestMethod()]
        public void IsCellOutOfBoundsTest()
        {
            
        }
    }
}

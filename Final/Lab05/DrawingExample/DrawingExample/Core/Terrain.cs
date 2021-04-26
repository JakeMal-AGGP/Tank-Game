using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using LineDraw;

namespace Tools
{
    public class Terrain
    {

        float minX = 0;
        public float maxX;
        float minY = 0;
        float maxY;
        int Density;
        int disc;
        int xfac;
        int yfac;
        public Vector2[] terrainPoints;
        public float[] sector_rr;

        Random rand = new Random();

        public Terrain(Vector2 screenSize, int density, int discrepancy, int xFactor, int yFactor)
        {
            maxX = screenSize.X;
            maxY = screenSize.Y;
            disc = discrepancy;
            Density = density;
            xfac = xFactor;
            yfac = yFactor;

            terrainPoints = new Vector2[calibratePoints()];
            sector_rr = new float[terrainPoints.Length];

            generateTerrain();
        }

        private int calibratePoints()
        {
            int calibrateCount = 0;
            bool keepGenerating = true;

            while (keepGenerating)
            {

                Vector2 calibrate = new Vector2();

                calibrate.X = calibrateCount * xfac;
                calibrate.Y = 350 + ((float)Math.Sin(calibrateCount) * yfac);

                if (calibrate.X < maxX)
                {
                    calibrateCount++;
                }
                else
                {
                    keepGenerating = false;
                }
            }

            //Console.WriteLine(calibrateCount);

            return calibrateCount;
        }

        private bool generateTerrain()
        {

            int i = 0;
            bool keepGenerating = true;

            while(keepGenerating)
            {
                if(i > terrainPoints.Length - 1)
                {
                    keepGenerating = false;
                    break;
                }

                terrainPoints[i].X = i * xfac;
                terrainPoints[i].Y = 350 + ((float)Math.Sin(i) * yfac);

                if (terrainPoints[i].X < maxX)
                {
                    i++;
                }
                else
                {
                    keepGenerating = false;
                }
            }

            randomizeTerrain();
            randomizeTerrain();
            randomizeTerrain();

            populateSectors();

            return false;
        }

        private void randomizeTerrain()
        {
            int randomizeSelectAmount = rand.Next(1, terrainPoints.Length);
            //int randomizeSelectAmount = terrainPoints.Length;

            int[] tempIndexList = new int[randomizeSelectAmount];

            for(int i = 0; i < randomizeSelectAmount; i++)
            {
                int selectedIndex = rand.Next(1, terrainPoints.Length);

                //terrainPoints[selectedIndex].X += rand.Next(-Density, Density);
                terrainPoints[selectedIndex].Y += rand.Next(-disc, disc);
            }
        }

        private void populateSectors()
        {
            for(int i = 0; i < terrainPoints.Length - 1; i++)
            {
                sector_rr[i] = ((terrainPoints[i].Y - terrainPoints[i + 1].Y) / (terrainPoints[i].X - terrainPoints[i + 1].X));
                Console.WriteLine(sector_rr[i]);
            }
        }

        public void drawTerrain(SpriteBatch spriteBatch)
        {

            for (int i = 0; i < terrainPoints.Length - 1; i++)
            {
                //LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), 5, 15);
                LineDrawer.DrawLine(spriteBatch, 3, Color.White, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), new Vector2(terrainPoints[i + 1].X, terrainPoints[i + 1].Y));
            }
            for (int i = 0; i < terrainPoints.Length - 1; i++)
            {
                //LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), 5, 15);
                LineDrawer.DrawLine(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y + 50), new Vector2(terrainPoints[i + 1].X, terrainPoints[i + 1].Y + 50));
            }
            for (int i = 0; i < terrainPoints.Length - 1; i++)
            {
                //LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), 5, 15);
                LineDrawer.DrawLine(spriteBatch, 3, Color.Orange, new Vector2(terrainPoints[i].X, terrainPoints[i].Y + 100), new Vector2(terrainPoints[i + 1].X, terrainPoints[i + 1].Y + 100));
            }
            for (int i = 0; i < terrainPoints.Length - 1; i++)
            {
                //LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), 5, 15);
                LineDrawer.DrawLine(spriteBatch, 3, Color.Yellow, new Vector2(terrainPoints[i].X, terrainPoints[i].Y + 150), new Vector2(terrainPoints[i + 1].X, terrainPoints[i + 1].Y + 150));
            }
            for (int i = 0; i < terrainPoints.Length - 1; i++)
            {
                //LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), 5, 15);
                LineDrawer.DrawLine(spriteBatch, 3, Color.LimeGreen, new Vector2(terrainPoints[i].X, terrainPoints[i].Y + 200), new Vector2(terrainPoints[i + 1].X, terrainPoints[i + 1].Y + 200));
            }
            for (int i = 0; i < terrainPoints.Length - 1; i++)
            {
                //LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), 5, 15);
                LineDrawer.DrawLine(spriteBatch, 3, Color.SkyBlue, new Vector2(terrainPoints[i].X, terrainPoints[i].Y + 250), new Vector2(terrainPoints[i + 1].X, terrainPoints[i + 1].Y + 250));
            }
            for (int i = 0; i < terrainPoints.Length - 1; i++)
            {
                //LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), 5, 15);
                LineDrawer.DrawLine(spriteBatch, 3, Color.Blue, new Vector2(terrainPoints[i].X, terrainPoints[i].Y + 300), new Vector2(terrainPoints[i + 1].X, terrainPoints[i + 1].Y + 300));
            }
            for (int i = 0; i < terrainPoints.Length - 1; i++)
            {
                //LinePrimatives.DrawCircle(spriteBatch, 3, Color.Red, new Vector2(terrainPoints[i].X, terrainPoints[i].Y), 5, 15);
                LineDrawer.DrawLine(spriteBatch, 3, Color.Purple, new Vector2(terrainPoints[i].X, terrainPoints[i].Y + 350), new Vector2(terrainPoints[i + 1].X, terrainPoints[i + 1].Y + 350));
            }
        }

    }
}

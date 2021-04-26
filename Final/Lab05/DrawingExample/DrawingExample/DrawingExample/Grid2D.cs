using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using LineDraw; 

namespace DrawingExample
{
    class Grid2D
    {
        public Vector2 Origin = Vector2.Zero;
        public Vector2 ScreenSize = Vector2.Zero;

      
        public int DivisionCount = 5;

        public Color AxisColor = Color.White;
        public Color LineColor = Color.DarkGray;
        public Color DivisionColor = Color.Yellow;

        public float AxisWidth = 3f;
        public float LineWidth = 1f;
        public float DivisionWidth = 2f;

        public bool IsDrawingAxis = false;
        public bool isDrawingDivsions = true;
        public bool isDrawingLines = false; 
        public bool isDrawingOrigin = false;


        private float _GridSize = 20f;
        private Vector2 _GridScale = Vector2.One;

        public float GridSize
        {
            get { return _GridSize; }
            set {
                    if (value <= 0) { return; }
                    _GridSize = value;
                    _GridScale = new Vector2(_GridSize, _GridSize);
                }
        }

        public Vector2 GridScale
        {
            get { return _GridScale; }
            set
            {
                _GridScale = value; 
            }
        }

        private Vector2 _Grid2ScreenScale = new Vector2(1,-1);
        public Vector2 Grid2ScreenScale
        {
            get { return _Grid2ScreenScale;  }
        }
  

        public Grid2D()
        {
            ScreenSize = new Vector2(
                GameApp.instance.graphics.PreferredBackBufferWidth,
                GameApp.instance.graphics.PreferredBackBufferHeight
                );
            Origin = ScreenSize * .5f; 
        }

        public void Draw(SpriteBatch sb)
        {
            int index = 0;
            Color drawColor = LineColor;
            float drawWidth = LineWidth;
            bool StillDrawing = true;
            bool MustDraw = true; 

            while (StillDrawing)
            {
                // Is this an Axis... 
                if ( (index == 0) && IsDrawingAxis )
                {
                    drawColor = AxisColor;
                    drawWidth = AxisWidth;
                    MustDraw = true;
                }
                // Is this a Division Line
                else if (( ( index % DivisionCount) == 0 ) && isDrawingDivsions )
                {
                    drawColor = DivisionColor;
                    drawWidth = DivisionWidth;
                    MustDraw = true;
                }
                // Or is this just every other line
                else
                {
                    drawColor = LineColor;
                    drawWidth = LineWidth;
                    MustDraw = false; 
                }

                if (!isDrawingLines && !MustDraw)
                {
                   

                }
                else
                {
                    _GridScale = new Vector2(GridSize, GridSize);
                    Vector2 PositivePoint = Origin + (_GridScale * index);
                    Vector2 NegativePoint = Origin - (_GridScale * index);

                    LineDrawer.DrawLine(sb, drawWidth, drawColor, new Vector2(0, PositivePoint.Y), new Vector2(ScreenSize.X, PositivePoint.Y));
                    LineDrawer.DrawLine(sb, drawWidth, drawColor, new Vector2(PositivePoint.X, 0), new Vector2(PositivePoint.X, ScreenSize.Y));

                    LineDrawer.DrawLine(sb, drawWidth, drawColor, new Vector2(0, NegativePoint.Y), new Vector2(ScreenSize.X, NegativePoint.Y));
                    LineDrawer.DrawLine(sb, drawWidth, drawColor, new Vector2(NegativePoint.X, 0), new Vector2(NegativePoint.X, ScreenSize.Y));   
                }

                index++;

                if (index > 50)
                {
                    StillDrawing = false;
                }
            }

        }

        public float ScaleGrid2Screen(float value)
        {
            return (value * GridSize);
        }

        public float ScaleScreen2Grid(float value)
        {
            return (value / GridSize);
        }

        public Vector2 Grid2ScreenPoint(Vector2 gridPoint, bool useGridScale = true)
            {
            if (!useGridScale) { return Origin + (gridPoint * _Grid2ScreenScale); }
            else { return Origin + (gridPoint * _Grid2ScreenScale * _GridScale); }
        }

        public Vector2 Screen2GridPoint(Vector2 screenPoint, bool useGridScale = true)
        {
            if (!useGridScale) { return Origin+ screenPoint * _Grid2ScreenScale; }
            else { return (screenPoint- Origin) / _Grid2ScreenScale * _GridScale; }
        }

        public void DrawSprite(SpriteBatch sb, Sprite sprite, bool DrawinGridScale = true)
        {
            if (sprite.dontDraw) { return; } // don't draw if the switch one
            sb.Draw(sprite.texture, Grid2ScreenPoint(sprite.position + sprite.positionOffset, DrawinGridScale), sprite.sourceRec, sprite.tint, sprite.rotation, sprite.origin, sprite.scale, sprite.effect, sprite.layerDepth);
        }

        public void DrawLine(SpriteBatch sb, Line line, Vector2? location = null, bool DrawinGridScale = true)
        {
            Vector2 drawlocation = Vector2.Zero; 
            if (location != null) { drawlocation = location.Value; }

            LineDrawer.DrawLine(sb, line.width, line.color, Grid2ScreenPoint(drawlocation + line.startPoint, DrawinGridScale), Grid2ScreenPoint(drawlocation + line.endPoint, DrawinGridScale)); 
        }

        public void DrawLineObject(SpriteBatch sb, LineObject lineObj, bool DrawinGridScale = true)
        {
            foreach (Line line in lineObj.Lines)
            {
               DrawLine(sb, line, lineObj.Location, DrawinGridScale);
            }
        }
    }
}

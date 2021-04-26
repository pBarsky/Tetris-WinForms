using System.Drawing;
using System.Windows.Forms;
using GameEngine.AbstractClasses;
using GameEngine.Utilities;
using TetrisGame.Utilities.Colors;

namespace TetrisGame.Utilities
{
    public static class Drawer
    {
        private const int BoxHeight = 10;
        private const int BoxMarginHorizontal = 10;
        private const int BoxMarginVertical = 10;
        private const int BoxWidth = 10;
        private const int ScaleFactor = 2;
        private static readonly SolidBrush DefaultBrush = new SolidBrush(Color.White);
        private static readonly Font MyFont = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold);
        private static readonly Pen Pen = new Pen(new SolidBrush(Color.White), 2);

        public static void DrawBox(PaintEventArgs e, int x, int y, EngineColor engineColor = EngineColor.White)
        {
            var color = ColorHandler.TranslateEngineColorToColor(engineColor);

            var lighterColor = new HslColor(color);
            lighterColor.Luminosity += 70;
            var darkerColor = new HslColor(color);
            darkerColor.Luminosity -= 70;

            var startingX = (x + 1) * BoxMarginHorizontal * ScaleFactor;
            var startingY = y * BoxMarginVertical * ScaleFactor;

            var oldPenColor = Pen.Color;

            Pen.Color = lighterColor;
            DrawUpperLine(e, startingX, startingY);

            DrawLeftLine(e, startingX, startingY);

            Pen.Color = darkerColor;
            DrawRightLine(e, startingX, startingY);

            DrawBottomLine(e, startingX, startingY);

            Pen.Color = oldPenColor;
        }

        public static void DrawBrick(PaintEventArgs e, int brickNumber, int posY, Brick brick, int offset)
        {
            DrawString($"Brick {brickNumber}:", e, 20, posY + 20);
            for (var i = 0; i < brick.Width; i++)
            {
                for (var j = 0; j < brick.Height; j++)
                {
                    if (brick.Shape[j, i].Item1 == 0) continue;
                    DrawRectangle(e, i, j + offset + 2 * brickNumber, brick.Color);
                    DrawBox(e, i, j + offset + 2 * brickNumber, brick.Color);
                }
            }
        }

        public static void DrawRectangle(PaintEventArgs e, int x, int y, EngineColor engineColor = EngineColor.White)
        {
            var brush = new SolidBrush(ColorHandler.TranslateEngineColorToColor(engineColor));
            e.Graphics.FillRectangle(brush, (x + 1) * BoxMarginHorizontal * ScaleFactor, y * BoxMarginVertical * ScaleFactor, BoxWidth * ScaleFactor, BoxHeight * ScaleFactor);
        }

        private static void DrawBottomLine(PaintEventArgs e, int startingX, int startingY)
        {
            e.Graphics.DrawLine(Pen, startingX, startingY + BoxHeight * ScaleFactor, startingX + BoxWidth * ScaleFactor,
                startingY + BoxHeight * ScaleFactor);
        }

        private static void DrawLeftLine(PaintEventArgs e, int startingX, int startingY)
        {
            e.Graphics.DrawLine(Pen, startingX, startingY, startingX, startingY + BoxHeight * ScaleFactor);
        }

        private static void DrawRightLine(PaintEventArgs e, int startingX, int startingY)
        {
            e.Graphics.DrawLine(Pen, startingX + BoxWidth * ScaleFactor, startingY, startingX + BoxWidth * ScaleFactor,
                startingY + BoxHeight * ScaleFactor);
        }

        private static void DrawString(string text, PaintEventArgs e, int x, int y)
        {
            e.Graphics.DrawString(text, MyFont, DefaultBrush, x, y);
        }

        private static void DrawUpperLine(PaintEventArgs e, int startingX, int startingY)
        {
            e.Graphics.DrawLine(Pen, startingX, startingY, startingX + BoxWidth * ScaleFactor, startingY);
        }
    }
}
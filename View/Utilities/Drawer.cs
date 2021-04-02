using System.Drawing;
using System.Windows.Forms;
using GameEngine.AbstractClasses;

namespace TetrisGame.Utilities
{
    public class Drawer
    {
        private const int BoxMarginVertical = 10;
        private const int BoxMarginHorizontal = 10;
        private const int BoxWidth = 10;
        private const int BoxHeight = 10;
        private readonly SolidBrush _brush = new SolidBrush(Color.White);
        private readonly Pen _pen = new Pen(new SolidBrush(Color.Red), 2);
        private const int ScaleFactor = 2;
        private readonly Font _myFont = new Font(FontFamily.GenericSansSerif, 11, FontStyle.Bold);

        public void DrawRectangle(PaintEventArgs e, int x, int y)
        {
            e.Graphics.FillRectangle(_brush, (x + 1) * BoxMarginHorizontal * ScaleFactor, (y + 1) * BoxMarginVertical * ScaleFactor, BoxWidth * ScaleFactor, BoxHeight * ScaleFactor);
        }

        public void DrawBox(PaintEventArgs e, int x, int y)
        {
            e.Graphics.DrawRectangle(_pen, (x + 1) * BoxMarginHorizontal * ScaleFactor, (y + 1) * BoxMarginVertical * ScaleFactor, BoxWidth * ScaleFactor, BoxHeight * ScaleFactor);
        }

        private void DrawString(string text, PaintEventArgs e, int x, int y)
        {
            e.Graphics.DrawString(text, _myFont, _brush, x, y);
        }

        public void DrawBrick(PaintEventArgs e, int brickNumber, int posY, Brick brick, int offset)
        {
            DrawString($"Brick {brickNumber}:", e, 20, posY + 20);
            for (var i = 0; i < brick.Width; i++)
            {
                for (var j = 0; j < brick.Height; j++)
                {
                    if (brick.Shape[j, i] != 0)
                    {
                        DrawRectangle(e, i, j + offset + 2 * brickNumber - 1);
                    }

                    DrawBox(e, i, j + offset + 2 * brickNumber - 1);
                }
            }
        }
    }
}
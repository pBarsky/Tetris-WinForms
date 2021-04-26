using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using GameEngine.Utilities;

namespace TetrisGame.Utilities
{
    public static class ColorHandler
    {
        public static Color TranslateEngineColorToColor(EngineColor color)
        {
            switch (color)
            {
                case EngineColor.Blank:
                    return Color.Transparent;

                case EngineColor.Black:
                    return Color.FromArgb(67, 85, 96);

                case EngineColor.Red:
                    return Color.FromArgb(205, 93, 125);

                case EngineColor.Yellow:
                    return Color.FromArgb(227, 209, 138);

                case EngineColor.Green:
                    return Color.FromArgb(112, 175, 133);

                case EngineColor.Purple:
                    return Color.FromArgb(139, 94, 131);

                case EngineColor.White:
                    return Color.FromArgb(205, 201, 195);

                case EngineColor.Blue:
                    return Color.FromArgb(112, 159, 176);

                case EngineColor.Orange:
                    return Color.FromArgb(246, 158, 123);

                case EngineColor.Pink:
                    return Color.FromArgb(239, 187, 207);

                default:
                    throw new ArgumentOutOfRangeException(nameof(color), color, @"Could not translate EngineColor to Color");
            }
        }
    }
}
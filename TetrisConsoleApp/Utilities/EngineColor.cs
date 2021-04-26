using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace GameEngine.Utilities
{
    public enum EngineColor
    {
        Blank,
        White,
        Black,
        Red,
        Yellow,
        Green,
        Blue,
        Purple,
        Orange,
        Pink
    }

    public static class ColorHelper
    {
        private static readonly EngineColor[] BrickColors = (EngineColor[])Enum.GetValues(typeof(EngineColor));

        private static int _lastColorIndex = 2;

        public static EngineColor GetNextColor()
        {
            var res = BrickColors[_lastColorIndex];
            _lastColorIndex = 2 + (_lastColorIndex - 1) % 7;
            return res;
        }
    }
}
using System;
using GameEngine.AbstractClasses;
using GameEngine.Utilities;

namespace GameEngine.Bricks
{
    internal class ZigZagBrick : Brick
    {
        public ZigZagBrick(int size = 3, int posX = 0, int posY = 0) : base(size, posX, posY)
        {
            Shape[0, 0] = (1, Color);
            Shape[0, 1] = (1, Color);
            Shape[1, 1] = (1, Color);
            Shape[1, 2] = (1, Color);
        }

        public ZigZagBrick() : this(3, 0, 0)
        {
        }
    }
}
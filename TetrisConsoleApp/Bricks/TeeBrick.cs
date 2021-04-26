using System;
using GameEngine.AbstractClasses;
using GameEngine.Utilities;

namespace GameEngine.Bricks
{
    internal class TeeBrick : Brick
    {
        public TeeBrick(int size = 3, int x = 0, int y = 0) : base(size, x, y)
        {
            for (int i = 0; i < size; i++)
            {
                Shape[0, i] = (1, Color);
            }

            for (int i = 0; i < size - 1; i++)
            {
                Shape[i, size / 2] = (1, Color);
            }
        }

        public TeeBrick() : this(3, 0, 0)
        {
        }
    }
}
﻿using System;
using GameEngine.AbstractClasses;
using GameEngine.Utilities;

namespace GameEngine.Bricks
{
    internal class SquareBrick : Brick
    {
        public SquareBrick(int size = 2, int x = 0, int y = 0) : base(size, x, y)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Shape[i, j] = (1, Color);
                }
            }
        }

        public SquareBrick() : this(2, 0, 0)
        {
        }
    }
}
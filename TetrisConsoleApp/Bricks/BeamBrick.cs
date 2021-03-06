﻿using System;
using GameEngine.AbstractClasses;
using GameEngine.Utilities;

namespace GameEngine.Bricks
{
    internal class BeamBrick : Brick
    {
        public BeamBrick(int size = 3, int x = 0, int y = 0) : base(size, x, y)
        {
            for (int i = 0; i < size; i++)
            {
                Shape[size / 2, i] = (1, Color);
            }
        }

        public BeamBrick() : this(3)
        {
        }
    }
}
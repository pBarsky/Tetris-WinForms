using GameEngine.AbstractClasses;

namespace GameEngine.Bricks
{
    internal class ZigZagBrick : Brick
    {
        public ZigZagBrick(int size = 3, int posX = 0, int posY = 0) : base(size, posX, posY)
        {
            shape[0, 0] = 1;
            shape[0, 1] = 1;
            shape[1, 1] = 1;
            shape[1, 2] = 1;
        }

        public ZigZagBrick() : this(3, 0, 0)
        {
        }
    }
}
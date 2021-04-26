using System;
using System.Windows.Forms;
using GameEngine.Utilities;

namespace GameEngine.AbstractClasses
{
    public abstract class Brick
    {
        protected Brick(int size = 1, int posX = 0, int posY = 0)
        {
            Shape = new ValueTuple<int, EngineColor>[size, size];
            this.PosX = posX;
            this.PosY = posY;
            Color = ColorHelper.GetNextColor();
        }

        public EngineColor Color { get; private set; }

        public int Height => Shape.GetLength(0);

        public int PosX { get; protected set; }

        public int PosY { get; protected set; }

        public (int, EngineColor)[,] Shape { get; protected set; }

        public int Width => Shape.GetLength(1);

        public Brick DeepCopy()
        {
            Brick outputBrick = (Brick)MemberwiseClone();
            outputBrick.Shape = ((int, EngineColor)[,])Shape.Clone();
            return outputBrick;
        }

        public void DoRotate(bool right = true)
        {
            Shape = Rotate(right);
        }

        public void MoveDown()
        {
            Move(0, 1);
        }

        public void MoveLeft()
        {
            Move(-1, 0);
        }

        public void MoveRight()
        {
            Move(1, 0);
        }

        public void RestartPosition(int newPosX)
        {
            PosY = 0;
            PosX = newPosX;
        }

        private void Move(int offsetX, int offsetY)
        {
            PosX += offsetX;
            PosY += offsetY;
        }

        private (int, EngineColor)[,] Rotate(bool clockDirection)
        {
            var size = Shape.GetLength(0);
            var result = new (int, EngineColor)[size, size];
            return clockDirection ? RotateRight(size, result) : RotateLeft(size, result);
        }

        private (int, EngineColor)[,] RotateLeft(int size, (int, EngineColor)[,] result)
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    result[size - 1 - j, i] = Shape[i, j];
                }
            }

            return result;
        }

        private (int, EngineColor)[,] RotateRight(int size, (int, EngineColor)[,] result)
        {
            for (var i = 0; i < size; i++)
            {
                for (var j = size - 1; j >= 0; j--)
                {
                    result[i, size - 1 - j] = Shape[j, i];
                }
            }

            return result;
        }
    }
}
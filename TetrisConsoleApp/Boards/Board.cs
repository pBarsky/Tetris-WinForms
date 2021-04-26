using System;
using GameEngine.AbstractClasses;
using GameEngine.Utilities;

namespace GameEngine.Boards
{
    public class Board
    {
        public Board(int width = 10, int height = 20)
        {
            Width = width;
            Height = height;
            Tab = new (int, EngineColor)[height, width];
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tab[i, j] = (0, EngineColor.Blank);
                }
            }
        }

        public int Height { get; }
        public (int, EngineColor)[,] Tab { get; }
        public int Width { get; }

        public int CheckBoard()
        {
            int counter = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Tab[i, j].Item1 != 2)
                    {
                        break;
                    }

                    counter++;
                }
                if (counter == Width)
                {
                    return i;
                }

                counter = 0;
            }
            return -1;
        }

        public void DeepClear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tab[i, j] = (0, Tab[i, j].Item2);
                }
            }
        }

        public void FreezeBrick(Brick brick)
        {
            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Shape[i, j].Item1 != 0)
                    {
                        Tab[brick.PosY + i, brick.PosX + j] = (2, brick.Color);
                    }
                }
            }
        }

        public int Gravitate(int multiplier = 10)
        {
            int level = CheckBoard();
            int score = 1;
            while (level != -1)
            {
                MoveDown(level);
                level = CheckBoard();
                score *= multiplier;
                multiplier++;
            }
            return score;
        }

        public void InsertBrick(Brick brick)
        {
            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Shape[i, j].Item1 != 0)
                    {
                        Tab[brick.PosY + i, brick.PosX + j] = brick.Shape[i, j];
                    }
                }
            }
        }

        public bool IsColliding(Brick brick, int offsetX, int offsetY)
        {
            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Shape[i, j].Item1 != 1)
                    {
                        continue;
                    }

                    if (i + brick.PosY + offsetY < 0 ||
                       i + brick.PosY + offsetY >= Height ||
                       j + brick.PosX + offsetX < 0 ||
                       j + brick.PosX + offsetX >= Width ||
                       Tab[brick.PosY + offsetY + i, brick.PosX + offsetX + j].Item1 == 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public void ShallowClear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (Tab[i, j].Item1 == 1)
                    {
                        Tab[i, j] = (0, EngineColor.Blank);
                    }
                }
            }
        }

        private void MoveDown(int level)
        {
            for (int i = level - 1; i >= 0; i--)
            {
                for (int j = 0; j < Width; j++)
                {
                    Tab[i + 1, j] = Tab[i, j];
                }
            }
        }
    }
}
using GameEngine.AbstractClasses;

namespace GameEngine.Boards
{
    public class Board
    {
        protected int[,] tab;
        public int Width { get; }
        public int Height { get; }

        public int[,] Tab => tab;


        public Board(int width = 10, int height = 20)
        {
            Width = width;
            Height = height;
            tab = new int[height, width];
        }

        public void InsertBrick(Brick brick)
        {
            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Shape[i, j] != 0)
                    {
                        tab[brick.PosY + i, brick.PosX + j] = brick.Shape[i, j];
                    }
                }
            }
        }

        public void DeepClear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    tab[i, j] = 0;
                }
            }
        }

        public void ShallowClear()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (tab[i, j] == 1)
                    {
                        tab[i, j] = 0;
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
                    if (brick.Shape[i, j] != 1)
                    {
                        continue;
                    }

                    if (i + brick.PosY + offsetY < 0 ||
                       i + brick.PosY + offsetY >= Height ||
                       j + brick.PosX + offsetX < 0 ||
                       j + brick.PosX + offsetX >= Width ||
                       tab[brick.PosY + offsetY + i, brick.PosX + offsetX + j] == 2)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
        public void FreezeBrick(Brick brick)
        {
            for (int i = 0; i < brick.Height; i++)
            {
                for (int j = 0; j < brick.Width; j++)
                {
                    if (brick.Shape[i, j] != 0)
                    {
                        tab[brick.PosY + i, brick.PosX + j] = 2;
                    }
                }
            }
        }

        public int CheckBoard()
        {
            int counter = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (tab[i, j] != 2)
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

        public int Gravitate(int multiplier = 10)
        {
            int level = CheckBoard();
            int score = 1;
            while (level != -1)
            {
                for (int i = level - 1; i >= 0; i--)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        tab[i + 1, j] = tab[i, j];
                    }
                }

                level = CheckBoard();
                score *= multiplier;
                multiplier++;
            }
            return score;
        }
    }
}

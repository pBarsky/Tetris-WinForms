using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameEngine.AbstractClasses;

namespace GameEngine.Bricks
{
    public class BricksQueue : IEnumerable<Brick>
    {
        public Queue<Brick> BricksQueue1 { get; }

        public string[] Buffer
        {
            get
            {
                int height = BricksQueue1.Sum(brick => brick.Height) + BricksQueue1.Count;
                string[] buffer = new string[height];
                int lineCounter = 0;
                int brickCounter = 0;
                foreach (Brick brick in BricksQueue1)
                {
                    buffer[lineCounter++] = $"Brick {++brickCounter}.:";
                    foreach (string s in brick.Buffer)
                    {
                        buffer[lineCounter++] = '\t' + s;
                    }
                }
                return buffer;
            }
        }

        public BricksQueue()
        {
            BricksQueue1 = new Queue<Brick>();
        }

        public void Enqueue(Brick brick)
        {
            BricksQueue1.Enqueue(brick);
        }

        public Brick Dequeue()
        {
            return BricksQueue1.Dequeue();
        }

        public void Clear()
        {
            BricksQueue1.Clear();
        }

        public IEnumerator<Brick> GetEnumerator()
        {
            return BricksQueue1.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

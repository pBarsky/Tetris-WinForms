using GameEngine.AbstractClasses;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine.Bricks
{
    public class BricksQueue : IEnumerable<Brick>
    {
        public BricksQueue()
        {
            BricksQueue1 = new Queue<Brick>();
        }

        public Queue<Brick> BricksQueue1 { get; }

        public void Clear()
        {
            BricksQueue1.Clear();
        }

        public Brick Dequeue()
        {
            return BricksQueue1.Dequeue();
        }

        public void Enqueue(Brick brick)
        {
            BricksQueue1.Enqueue(brick);
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
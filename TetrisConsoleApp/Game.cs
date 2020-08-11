using GameEngine.AbstractClasses;
using GameEngine.Boards;
using GameEngine.Bricks;
using GameEngine.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameEngine
{
    // TODO reference form to build view from here

    public class Game
    {
        private Brick _currentBrick;
        private readonly Board _board;
        public readonly ScoreWriter ScoreWriter;
        public bool Alive { get; private set; } = true;
        public bool HasChanged { get; private set; }
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        public int Score { get; private set; }
        private static List<Brick> _allAvailableBricks;
        public BricksQueue QueueBricks { get; } = new BricksQueue();

        public string[] HelpStrings { get; } =
        {
            $"{"UpArrow",-10} -> ROTATE", $"{"DownArrow",-10} -> GO DOWN +1",
            $"{"LeftArrow",-10} -> MOVE LEFT", $"{"RightArrow",-10} -> MOVE RIGHT", $"{"ESC",-10} -> GIVE UP"
        };

        public Game(int boardHeight = 20, int boardWidth = 10)
        {
            _board = new Board(boardWidth, boardHeight);
            IEnumerable<Brick> bricks = typeof(Brick).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Brick)))
                .Select(t => (Brick)Activator.CreateInstance(t));
            _allAvailableBricks = bricks.ToList();
            ScoreWriter = new ScoreWriter();
        }

        private void ClearBricksQueueBuffer()
        {
            string clearLine = new string(' ', 20);
            for (int i = 1; i < _board.Height; i++)
            {
                Console.SetCursorPosition(_board.Width + 2, 1 + i);
                Console.Write(clearLine);
            }
        }

        public void Prepare()
        {
            SeedQueue();
            NextBrick();
        }

        public Board Step(bool down, KeyCommand key)
        {
            _board.ShallowClear();
            HasChanged = false;
            HandlePlayerMovement(key, true);
            if (down)
            {
                _board.ShallowClear();
                HandlePlayerMovement(KeyCommand.Down);
            }
            return _board;
        }

        public void RestartGame()
        {
            Score = 0;
            Alive = true;
            _board.DeepClear();
            QueueBricks.Clear();
            Prepare();
        }

        private void HandlePlayerMovement(KeyCommand direction, bool fastForward = false)
        {
            switch (direction)
            {
                case KeyCommand.Down:
                    if (!_board.IsColliding(_currentBrick, 0, 1))
                    {
                        HasChanged = true;
                        _currentBrick.MoveDown();
                        if (fastForward)
                        {
                            Score += 1;
                        }

                        _board.InsertBrick(_currentBrick);
                    }
                    else
                    {
                        _board.FreezeBrick(_currentBrick);
                        Score += _board.Gravitate(_board.Width);
                        NextBrick();
                    }
                    break;

                case KeyCommand.Left:
                    if (!_board.IsColliding(_currentBrick, -1, 0))
                    {
                        HasChanged = true;
                        _currentBrick.MoveLeft();
                        _board.InsertBrick(_currentBrick);
                    }
                    break;

                case KeyCommand.Right:
                    if (!_board.IsColliding(_currentBrick, 1, 0))
                    {
                        HasChanged = true;
                        _currentBrick.MoveRight();
                        _board.InsertBrick(_currentBrick);
                    }
                    break;

                case KeyCommand.Up:
                    _currentBrick.DoRotate(false);
                    if (!_board.IsColliding(_currentBrick, 0, 0))
                    {
                        HasChanged = true;
                        _board.InsertBrick(_currentBrick);
                    }
                    else
                    {
                        _currentBrick.DoRotate();
                    }
                    break;

                case KeyCommand.Escape:
                    Alive = false;
                    break;
            }
        }

        private void NextBrick()
        {
            _currentBrick = QueueBricks.Dequeue();
            EnqueueNewBrick();
            _currentBrick.RestartPosition(_random.Next(_board.Width - _currentBrick.Width));
            if (_board.IsColliding(_currentBrick, 0, 0))
            {
                Alive = false;
            }
        }

        private void SeedQueue(int size = 3)
        {
            for (int i = 0; i < size; i++)
            {
                EnqueueNewBrick();
            }
        }

        private void EnqueueNewBrick()
        {
            QueueBricks.Enqueue(_allAvailableBricks[_random.Next(_allAvailableBricks.Count)].DeepCopy());
        }
    }
}
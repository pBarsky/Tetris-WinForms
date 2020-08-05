using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GameEngine.AbstractClasses;
using GameEngine.Boards;
using GameEngine.Bricks;
using GameEngine.Utilities;

namespace GameEngine
{
    // TODO reference form to build view from here

    public class Game
    {
        private Brick _currentBrick;
        private readonly Board _board;
        private ScoreWriter _scoreWriter;
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
            _scoreWriter = new ScoreWriter();

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

        public void Play()
        {
            Console.Clear();
            ConsoleUtilities.HideCursor();
            SeedQueue();
            NextBrick();
            Alive = true;
            Stopwatch stopwatch = new Stopwatch();
            long millisecondsPassed = 0L;
            stopwatch.Start();
            while (Alive)
            {
                if (stopwatch.ElapsedMilliseconds <= 100) continue;
                _board.ShallowClear();
                HandlePlayerMovement(KeyboardHandler.GetDirection(), true);
                if (millisecondsPassed > 1000)
                {
                    _board.ShallowClear();
                    HandlePlayerMovement(KeyCommand.Down);
                    millisecondsPassed = 0L;
                }
                if (HasChanged)
                    HasChanged = false;
                millisecondsPassed += stopwatch.ElapsedMilliseconds;
                stopwatch.Restart();
            }
            GameOver();
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

        private void GameOver()
        {
            Console.WriteLine($"\n\nGAME OVER\n\tYOU'VE SCORED: {Score} POINTS!!");
            ConsoleUtilities.ShowCursor();
            Console.WriteLine("Please enter your name: ");
            ConsoleUtilities.HideCursor();
            _scoreWriter.SaveScore(Console.ReadLine(), Score);
            Console.WriteLine("RETRY? (y\\n)");
            while (true)
            {
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.Y:
                        RestartGame();
                        break;
                    case ConsoleKey.N:
                    case ConsoleKey.Escape:
                        return;
                    default:
                        Console.WriteLine("RETRY? (y\\n)");
                        break;
                }
            }

        }

        private void RestartGame()
        {
            Score = 0;
            _board.DeepClear();
            QueueBricks.Clear();
            Play();
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
                            Score += 1;
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
                Alive = false;
        }

        private void SeedQueue(int size = 3)
        {
            for (int i = 0; i < size; i++)
                EnqueueNewBrick();
        }

        private void EnqueueNewBrick()
        {
            QueueBricks.Enqueue(_allAvailableBricks[_random.Next(_allAvailableBricks.Count)].DeepCopy());
        }
    }
}

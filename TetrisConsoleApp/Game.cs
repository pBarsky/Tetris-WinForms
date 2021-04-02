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
        public readonly ScoreWriter ScoreWriter;
        public bool Alive { get; private set; } = true;
        public bool HasChanged { get; set; }
        private readonly Random _random = new Random(DateTime.Now.Millisecond);
        public int Score { get; set; }
        private static List<Brick> _allAvailableBricks;
        private readonly InputHandler _inputHandler;
        public BricksQueue QueueBricks { get; } = new BricksQueue();
        public Brick CurrentBrick { set; get; }
        public Board Board { get; }

        public string[] HelpStrings { get; } =
        {
            $"{"UpArrow",-10} -> ROTATE", $"{"DownArrow",-10} -> GO DOWN +1",
            $"{"LeftArrow",-10} -> MOVE LEFT", $"{"RightArrow",-10} -> MOVE RIGHT", $"{"ESC",-10} -> GIVE UP"
        };

        public Game(int boardHeight = 20, int boardWidth = 10)
        {
            Board = new Board(boardWidth, boardHeight);
            IEnumerable<Brick> bricks = typeof(Brick).Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Brick)))
                .Select(t => (Brick)Activator.CreateInstance(t));
            _allAvailableBricks = bricks.ToList();
            ScoreWriter = new ScoreWriter();
            _inputHandler = new InputHandler(this);
        }

        public void Prepare()
        {
            SeedQueue();
            NextBrick();
        }

        public Board Step(bool down, KeyCommand key)
        {
            Board.ShallowClear();
            HasChanged = false;
            HandlePlayerMovement(key, true);
            if (!down) return Board;
            Board.ShallowClear();
            HandlePlayerMovement(KeyCommand.Down);
            return Board;
        }

        public void RestartGame()
        {
            Score = 0;
            Alive = true;
            Board.DeepClear();
            QueueBricks.Clear();
            Prepare();
        }

        private void HandlePlayerMovement(KeyCommand direction, bool fastForward = false)
        {
            switch (direction)
            {
                case KeyCommand.Down:
                    _inputHandler.HandleDownPress(fastForward);
                    break;

                case KeyCommand.Left:
                    _inputHandler.HandleLeftPress();
                    break;

                case KeyCommand.Right:
                    _inputHandler.HandleRightPress();
                    break;

                case KeyCommand.Up:
                    _inputHandler.HandleUpPress();
                    break;

                case KeyCommand.Escape:
                    Alive = false;
                    break;
            }
        }

        public void NextBrick()
        {
            CurrentBrick = QueueBricks.Dequeue();
            EnqueueNewBrick();
            CurrentBrick.RestartPosition(_random.Next(Board.Width - CurrentBrick.Width));
            if (Board.IsColliding(CurrentBrick, 0, 0))
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
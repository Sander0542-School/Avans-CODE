using System;
using CODE_GameLib;

namespace CODE_Frontend
{
    public class InputView
    {
        private readonly Game _game;

        public InputView(Game game)
        {
            _game = game;
            _game.Updated += (sender, game1) => Quit = game1.Quit;
        }

        public bool Quit { get; set; }

        /// <summary>
        ///     Reads the input from the keyboard
        /// </summary>
        public void AskForInput()
        {
            //If the game is in progress, check which keys the user is using to move the player
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Escape:
                    _game.Quit = true;
                    break;
                case ConsoleKey.UpArrow:
                case ConsoleKey.W:
                case ConsoleKey.I:
                    _game.Move(Direction.NORTH);
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.A:
                case ConsoleKey.J:
                    _game.Move(Direction.WEST);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.S:
                case ConsoleKey.K:
                    _game.Move(Direction.SOUTH);
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.D:
                case ConsoleKey.L:
                    _game.Move(Direction.EAST);
                    break;
            }
        }
    }
}
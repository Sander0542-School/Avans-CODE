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
                case ConsoleKey.K:
                    _game.Move(Direction.NORTH);
                    break;
                case ConsoleKey.LeftArrow:
                case ConsoleKey.H:
                    _game.Move(Direction.WEST);
                    break;
                case ConsoleKey.DownArrow:
                case ConsoleKey.J:
                    _game.Move(Direction.SOUTH);
                    break;
                case ConsoleKey.RightArrow:
                case ConsoleKey.L:
                    _game.Move(Direction.EAST);
                    break;
                case ConsoleKey.Spacebar:
                    _game.Shoot();
                    break;
                case ConsoleKey.S:
                    _game.ToggleCheat(Cheat.NextStoneWin);
                    break;
                case ConsoleKey.T:
                    _game.ToggleCheat(Cheat.ClosingGatePortal);
                    break;
            }
        }
    }
}
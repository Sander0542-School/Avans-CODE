using CODE_FileSystem;
using CODE_GameLib;
using System;
using System.Text;

namespace CODE_Frontend
{
    class Program
    {
        public static ConsoleKey KeyPressed { get; private set; }
        

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WindowWidth = 200;
            Console.WindowHeight = 50;
            Console.CursorVisible = false;

            GameReader reader = new GameReader();
            Game game = reader.Read(@"./Levels/TempleOfDoom.json");

            GameView gameView = new GameView();
            game.Updated += (sender, game) => gameView.Draw(game);


            KeyPressed = Console.ReadKey().Key;
            Direction direction;
            while (!game.Quit)
            {
                switch (KeyPressed)
                {
                    case ConsoleKey.Escape:
                        game.Quit = true;
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        direction = Direction.WEST;
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        direction = Direction.NORTH;
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        direction = Direction.EAST;
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        direction = Direction.SOUTH;
                        break;
                }
                game.Move(direction);
            }

            
            
            
        }
    }
}
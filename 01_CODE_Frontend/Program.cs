using CODE_FileSystem;
using CODE_GameLib;
using System;
using System.Text;

namespace CODE_Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WindowWidth = 200;
            Console.WindowHeight = 50;
            Console.CursorVisible = false;

            var reader = new GameReader();
            var game = reader.Read(@"./Levels/TempleOfDoom.json");

            var gameView = new GameView();
            game.Updated += (sender, game1) => gameView.Draw(game1);
            
            while (!game.Quit)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        game.Quit = true;
                        break;
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                    case ConsoleKey.I:
                        game.Move(Direction.NORTH);
                        break;
                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                    case ConsoleKey.J:
                        game.Move(Direction.WEST);
                        break;
                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                    case ConsoleKey.K:
                        game.Move(Direction.SOUTH);
                        break;
                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                    case ConsoleKey.L:
                        game.Move(Direction.EAST);
                        break;
                }
            }
            
            Console.WriteLine("Thanks for playing Temple of Doom!");
        }
    }
}
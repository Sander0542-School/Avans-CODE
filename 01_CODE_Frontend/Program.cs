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

            var reader = new GameReader(); //TODO DI Container
            var game = reader.Read(@"./Levels/TempleOfDoom.json");
            
            var inputView = new InputView(game);
            var gameView = new GameView();
            
            game.Updated += (sender, game1) => gameView.Draw(game1);
            gameView.Draw(game);
            
            while (!inputView.Quit)
            {
                inputView.AskForInput();
            }
        }
    }
}
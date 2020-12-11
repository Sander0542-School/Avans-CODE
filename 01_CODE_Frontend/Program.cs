using CODE_FileSystem;
using CODE_GameLib;
using System;
using System.Text;
using CODE_FileSystem.Factories;
using CODE_FileSystem.Factories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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

            var serviceProvider = new ServiceCollection()
                .AddSingleton<IRoomFactory, RoomFactory>()
                .AddSingleton<IItemFactory, ItemFactory>()
                .AddSingleton<IDoorFactory, DoorFactory>()
                .AddTransient<GameReader>()
                .BuildServiceProvider();

            var reader = serviceProvider.GetService<GameReader>();
            var game = reader.Read(@"./Levels/TempleOfDoom.json");
            
            var inputView = new InputView(game);
            var gameView = new GameView();
            
            game.Updated += (sender, game1) => gameView.Draw(game1);
            gameView.Draw(game);
            
            while (!inputView.Quit)
            {
                inputView.AskForInput();
            }
            
            Console.Clear();
            Console.WriteLine("Thanks for playing Temple of Doom");
        }
    }
}
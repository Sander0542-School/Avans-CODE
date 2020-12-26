using System;

namespace CODE_Frontend.Tiles
{
    public interface ITileView
    {
        char GetIcon()
        {
            return ' ';
        }

        ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }

        ConsoleColor GetBackgroundColor()
        {
            return ConsoleColor.Black;
        }

        void Draw()
        {
            Console.BackgroundColor = GetBackgroundColor();
            Console.ForegroundColor = GetColor();
            Console.Write($" {GetIcon()} ");
            Console.ResetColor();
        }
    }
}
using System;

namespace CODE_Frontend.Tiles
{
    public interface ITileView
    {
        ConsoleColor BackgroundColor { get; set; }

        string GetIcon();
        ConsoleColor GetColor();

        void Draw()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.ForegroundColor = GetColor();
            Console.Write($" {GetIcon()} ");
            Console.ResetColor();
        }
    }
}
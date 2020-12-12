using System;

namespace CODE_Frontend.Tiles
{
    public class PlayerTileView : ITileView
    {
        public ConsoleColor BackgroundColor { get; set; }

        public string GetIcon()
        {
            return "P";
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.DarkBlue;
        }
    }
}
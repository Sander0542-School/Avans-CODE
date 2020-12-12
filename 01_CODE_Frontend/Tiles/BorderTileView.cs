using System;

namespace CODE_Frontend.Tiles
{
    public class BorderTileView : ITileView
    {
        public ConsoleColor BackgroundColor { get; set; }

        public string GetIcon()
        {
            return "#";
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.Yellow;
        }
    }
}
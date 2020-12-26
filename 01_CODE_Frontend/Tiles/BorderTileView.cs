using System;

namespace CODE_Frontend.Tiles
{
    public class BorderTileView : ITileView
    {
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
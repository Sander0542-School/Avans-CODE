using System;

namespace CODE_Frontend.Tiles
{
    public class PlayerTileView : ITileView
    {
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
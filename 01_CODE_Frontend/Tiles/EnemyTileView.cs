using System;

namespace CODE_Frontend.Tiles
{
    public class EnemyTileView : ITileView
    {
        public ConsoleColor BackgroundColor { get; set; }

        public string GetIcon()
        {
            return "E";
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.DarkBlue;
        }
    }
}
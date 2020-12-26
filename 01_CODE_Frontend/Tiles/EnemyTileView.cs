using System;

namespace CODE_Frontend.Tiles
{
    public class EnemyTileView : ITileView
    {
        public string GetIcon()
        {
            return "E";
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.DarkRed;
        }
    }
}
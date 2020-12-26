using System;

namespace CODE_Frontend.Tiles
{
    public class EnemyTileView : ITileView
    {
        public char GetIcon()
        {
            return 'E';
        }

        public ConsoleColor GetColor()
        {
            return ConsoleColor.DarkRed;
        }
    }
}
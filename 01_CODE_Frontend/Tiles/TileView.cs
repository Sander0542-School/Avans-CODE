using System;

namespace CODE_Frontend.Tiles
{
    public abstract class TileView<T> : ITileView
    {
        protected readonly T Tile;

        protected TileView(T tile)
        {
            Tile = tile;
        }

        public virtual char GetIcon()
        {
            return ' ';
        }

        public virtual ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }

        public virtual ConsoleColor GetBackgroundColor()
        {
            return ConsoleColor.Black;
        }
    }
}
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

        public ConsoleColor BackgroundColor { get; set; }

        public virtual string GetIcon()
        {
            return " ";
        }

        public virtual ConsoleColor GetColor()
        {
            return ConsoleColor.White;
        }
    }
}
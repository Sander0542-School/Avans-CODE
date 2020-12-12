using System;
using CODE_GameLib;

namespace CODE_Frontend.Tiles
{
    public abstract class TileView<T> : ITileView
    {
        protected readonly T Tile;

        public ConsoleColor BackgroundColor { get; set; }

        protected TileView(T tile)
        {
            Tile = tile;
        }

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
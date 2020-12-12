using System;
using CODE_GameLib;

namespace CODE_Frontend.Tiles
{
    public abstract class TileView<T> : ITileView
    {
        protected readonly T Tile;
        protected readonly Direction Direction;

        public ConsoleColor BackgroundColor { get; set; }
        
        public TileView(T tile, Direction direction)
        {
            Tile = tile;
            Direction = direction;
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
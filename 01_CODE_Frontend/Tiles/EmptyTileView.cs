using System;

namespace CODE_Frontend.Tiles
{
    public class EmptyTileView : ITileView
    {
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
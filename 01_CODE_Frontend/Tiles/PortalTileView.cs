using System;
using CODE_GameLib;
using CODE_GameLib.Doors;

namespace CODE_Frontend.Tiles
{
    public class PortalTileView : DoorTileView
    {
        public PortalTileView(IDoor tile) : base(tile)
        {
        }

        public override ConsoleColor GetBackgroundColor()
        {
            return ConsoleColor.Magenta;
        }
    }
}
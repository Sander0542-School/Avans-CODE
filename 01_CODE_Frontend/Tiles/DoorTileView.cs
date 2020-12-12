using System;
using CODE_GameLib;
using CODE_GameLib.Doors;

namespace CODE_Frontend.Tiles
{
    public class DoorTileView : TileView<IDoor>
    {
        public DoorTileView(IDoor tile, Direction direction) : base(tile, direction)
        {
        }

        public override string GetIcon()
        {
            return Tile switch
            {
                ClosingGateDoor _ => "∩",
                ColoredDoor _ => Direction == Direction.NORTH || Direction == Direction.SOUTH ? "=" : "|",
                ToggleDoor _ => "⊥",
                _ => base.GetIcon()
            };
        }

        public override ConsoleColor GetColor()
        {
            if (Tile is ColoredDoor coloredDoor)
            {
                return Enum.Parse<ConsoleColor>(coloredDoor.Color, true);
            }

            return base.GetColor();
        }
    }
}
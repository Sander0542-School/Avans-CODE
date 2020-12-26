using System;
using CODE_GameLib;
using CODE_GameLib.Doors;

namespace CODE_Frontend.Tiles
{
    public class DoorTileView : TileView<IDoor>
    {
        private readonly Direction _direction;

        public DoorTileView(IDoor tile, Direction direction = Direction.NORTH) : base(tile)
        {
            _direction = direction;
        }

        public override char GetIcon()
        {
            return Tile switch
            {
                ClosingGateDoor _ => '∩',
                ColoredDoor _ => _direction == Direction.NORTH || _direction == Direction.SOUTH ? '=' : '|',
                ToggleDoor _ => '⊥',
                _ => base.GetIcon()
            };
        }

        public override ConsoleColor GetColor()
        {
            if (Tile is ColoredDoor coloredDoor) return Enum.Parse<ConsoleColor>(coloredDoor.Color, true);

            return base.GetColor();
        }
    }
}
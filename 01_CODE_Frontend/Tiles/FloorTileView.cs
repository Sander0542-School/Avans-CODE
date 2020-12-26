using System;
using CODE_GameLib;
using CODE_GameLib.Floors;

namespace CODE_Frontend.Tiles
{
    public class FloorTileView : TileView<IFloor>
    {
        public FloorTileView(IFloor tile) : base(tile)
        {
        }

        public override char GetIcon()
        {
            return Tile switch
            {
                ConveyorBeltFloor conveyorBeltFloor => conveyorBeltFloor.Direction switch
                {
                    Direction.NORTH => '^',
                    Direction.EAST => '>',
                    Direction.SOUTH => 'v',
                    Direction.WEST => '<',
                    _ => base.GetIcon()
                },
                _ => base.GetIcon()
            };
        }

        public override ConsoleColor GetBackgroundColor()
        {
            return ConsoleColor.Gray;
        }
    }
}
using System;
using CODE_GameLib.Doors;
using CODE_GameLib.Rooms;

namespace CODE_GameLib.Connections
{
    public class Connection : IConnection
    {
        public RoomBase TargetRoom { get; set; }

        public Direction TargetDirection { get; set; }

        public IDoor Door { get; set; }

        public void GetExitLocation(out RoomBase room, out int x, out int y)
        {
            x = 0;
            y = 0;
            room = TargetRoom;

            switch (TargetDirection)
            {
                case Direction.NORTH:
                    x = room.Width / 2;
                    break;
                case Direction.EAST:
                    y = room.Height / 2;
                    x = room.Width - 1;
                    break;
                case Direction.SOUTH:
                    y = room.Height - 1;
                    x = room.Width / 2;
                    break;
                case Direction.WEST:
                    y = room.Height / 2;
                    break;
                default:
                    throw new NotImplementedException("This exit direction is not yet supported");
            }
        }
    }
}
using CODE_GameLib.Doors;
using CODE_GameLib.Rooms;

namespace CODE_GameLib
{
    public class Connection
    {
        public RoomBase TargetRoom { get; set; }
        public Direction TargetDirection { get; set; }

        public IDoor Door { get; set; }
    }
}
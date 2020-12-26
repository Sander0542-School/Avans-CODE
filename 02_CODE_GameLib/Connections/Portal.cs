using CODE_GameLib.Doors;
using CODE_GameLib.Rooms;

namespace CODE_GameLib.Connections
{
    public class Portal : IConnection
    {
        public RoomBase TargetRoom { get; set; }

        public int TargetX { get; set; }
        
        public int TargetY { get; set; }
        
        public IDoor Door { get; set; }

        public void GetExitLocation(out RoomBase room, out int x, out int y)
        {
            room = TargetRoom;
            x = TargetX;
            y = TargetY;
        }
    }
}
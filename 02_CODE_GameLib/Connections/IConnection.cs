using CODE_GameLib.Doors;
using CODE_GameLib.Rooms;

namespace CODE_GameLib.Connections
{
    public interface IConnection
    {
        public RoomBase TargetRoom { get; set; }

        public IDoor Door { get; set; }
        
        void GetExitLocation(out RoomBase room, out int x, out int y);
    }
}
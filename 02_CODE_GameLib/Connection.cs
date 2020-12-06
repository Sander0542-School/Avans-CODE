using CODE_GameLib.Doors;
using CODE_GameLib.Rooms;

namespace CODE_GameLib
{
    public class Connection
    {
        public RoomBase Room { get; set; }
        
        public IDoor Door { get; set; }
    }
}
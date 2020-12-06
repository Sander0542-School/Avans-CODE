using CODE_GameLib.Doors;
using CODE_GameLib.Rooms;

namespace CODE_GameLib
{
    public class Connection
    {
        public Room Room { get; set; }
        
        public IDoor Door { get; set; }
    }
}
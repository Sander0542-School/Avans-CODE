using CODE_GameLib.Rooms;

namespace CODE_PersistenceLib.Factories.Interfaces
{
    public interface IRoomFactory
    {
        RoomBase CreateRoom(string type, int id, int height, int width);
    }
}
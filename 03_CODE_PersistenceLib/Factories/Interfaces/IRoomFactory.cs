using CODE_GameLib.Rooms;

namespace CODE_FileSystem.Factories.Interfaces
{
    public interface IRoomFactory
    {
        RoomBase CreateRoom(string type, int id, int height, int width);
    }
}
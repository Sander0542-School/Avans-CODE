using System;

namespace CODE_GameLib.Rooms
{
    public interface IRoomStrategy
    {
        RoomBase CreateRoom(Type type);
    }
}
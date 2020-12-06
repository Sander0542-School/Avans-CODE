using System;

namespace CODE_GameLib.Rooms
{
    public interface IRoomFactory
    {
        RoomBase CreateRoom();
        bool AppliesTo(Type type);
    }
}
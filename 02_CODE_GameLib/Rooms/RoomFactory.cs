using System;

namespace CODE_GameLib.Rooms
{
    public class RoomFactory : IRoomFactory
    {
        public RoomFactory()
        {
        }

        public RoomBase CreateRoom()
        {
            throw new NotImplementedException();
        }

        public bool AppliesTo(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
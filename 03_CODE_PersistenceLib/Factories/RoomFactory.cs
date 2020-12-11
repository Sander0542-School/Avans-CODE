using System;
using CODE_FileSystem.Factories.Interfaces;
using CODE_GameLib.Rooms;

namespace CODE_FileSystem.Factories
{
    public class RoomFactory : IRoomFactory
    {
        //Creates a Room object according to the type
        public RoomBase CreateRoom(string type, int id, int height, int width)
        {
            switch (type)
            {
                case "room":
                    return new Room(id, height, width);
                default:
                    throw new NotImplementedException(message: "This room is not implemented yet");
            }
        }
    }
}
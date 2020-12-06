using System;

namespace CODE_GameLib.Rooms
{
    public class RoomFactory 
    {
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
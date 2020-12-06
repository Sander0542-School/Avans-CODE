using System;

namespace CODE_GameLib.Rooms
{
    public class RoomFactory 
    {
        public RoomBase CreateRoom(string type, int height, int width, int id)
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
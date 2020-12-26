using System;
using CODE_GameLib.Rooms;
using CODE_PersistenceLib.Factories.Interfaces;

namespace CODE_PersistenceLib.Factories
{
    public class RoomFactory : IRoomFactory
    {
        /// <summary>
        ///     Creates a Room object according to the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="height"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public RoomBase CreateRoom(string type, int id, int height, int width)
        {
            switch (type)
            {
                case "room":
                    return new Room(id, height, width);
                default:
                    throw new NotImplementedException("This room is not implemented yet");
            }
        }
    }
}
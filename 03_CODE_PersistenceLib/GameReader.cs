using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Doors;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        private readonly RoomFactory _roomFactory = new RoomFactory();
        private readonly ItemFactory _roomItemFactory = new ItemFactory();
        private readonly DoorFactory _doorFactory = new DoorFactory();

        public Game Read(string filePath)
        {
            var json = JObject.Parse(File.ReadAllText(filePath));

            var rooms = GetRooms(json["rooms"]);

            var player = GetPlayer(json["player"], rooms);

            ApplyConnection(json["connections"], rooms);

            return new Game(filePath, rooms, player);
        }

        private List<RoomBase> GetRooms(JToken rooms)
        {
            try
            {
                return rooms.Select(GetRoom).ToList();
            }
            catch (Exception exception)
            {
                if (exception is ArgumentException || exception is NotImplementedException)
                {
                    throw;
                }

                throw new ArgumentException($"The json file does not contain valid rooms");
            }
        }

        private RoomBase GetRoom(JToken jsonRoom)
        {
            try
            {
                var type = jsonRoom["type"].ToString();
                var id = int.Parse(jsonRoom["id"].ToString());
                var width = int.Parse(jsonRoom["width"].ToString());
                var height = int.Parse(jsonRoom["height"].ToString());

                var room = _roomFactory.CreateRoom(type, id, height, width);

                room.Items = jsonRoom["items"] != null ? GetRoomItems(jsonRoom["items"]) : new List<IItem>();

                return room;
            }
            catch (Exception exception)
            {
                if (exception is NotImplementedException)
                {
                    throw;
                }

                throw new ArgumentException($"The room ({jsonRoom["id"] ?? "null"}) does not contain valid json");
            }
        }

        private List<IItem> GetRoomItems(JToken jsonItems)
        {
            var items = new List<IItem>();

            foreach (var jsonItem in jsonItems)
            {
                var type = jsonItem["type"].ToString();
                var x = int.Parse(jsonItem["x"].ToString());
                var y = int.Parse(jsonItem["y"].ToString());

                var options = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonItem.ToString());
                options.Remove("type");
                options.Remove("x");
                options.Remove("y");

                items.Add(_roomItemFactory.CreateItem(type, x, y, options));
            }

            return items;
        }

        private Player GetPlayer(JToken jsonPlayer, IEnumerable<RoomBase> rooms)
        {
            try
            {
                var startRoomId = int.Parse(jsonPlayer["startRoomId"].ToString());
                var startX = int.Parse(jsonPlayer["startX"].ToString());
                var startY = int.Parse(jsonPlayer["startY"].ToString());
                var lives = int.Parse(jsonPlayer["lives"].ToString());

                var room = rooms.First(room1 => room1.Id == startRoomId);

                return new Player(lives, room, startX, startY);
            }
            catch (Exception)
            {
                throw new ArgumentException($"The json file does not contain a valid player");
            }
        }

        //Gets the connections from the json and adds them to the rooms
        private void ApplyConnection(JToken jsonConnections, IEnumerable<RoomBase> rooms)
        {
            try
            {
                //Each connection is added to a room
                foreach (var jsonConnection in jsonConnections)
                {
                    var directions = GetConnectionDirections(jsonConnection);

                    var roomOne = rooms.First(room => room.Id == directions.First().Value);
                    var roomTwo = rooms.First(room => room.Id == directions.Last().Value);

                    var door = GetConnectionDoor(jsonConnection);

                    //Each connection connects 2 rooms
                    roomOne.Connections.Add(directions.Last().Key, new Connection
                    {
                        TargetRoom = rooms.First(room => room.Id == directions.Last().Value),
                        TargetDirection = directions.First().Key,
                        Door = door
                    });

                    roomTwo.Connections.Add(directions.First().Key, new Connection
                    {
                        TargetRoom = rooms.First(room => room.Id == directions.First().Value),
                        TargetDirection = directions.Last().Key,
                        Door = door
                    });
                }
            }
            catch (Exception)
            {
                throw new ArgumentException($"The json file does not contain a valid connection");
            }
        }

        //Gets the door belonging to the connection
        private IDoor GetConnectionDoor(JToken jsonConnection)
        {
            //Connection does not always have a door
            if (jsonConnection["door"] == null)
            {
                return null;
            }

            var jsonDoor = jsonConnection["door"];

            var type = jsonDoor["type"].ToString();

            var options = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonDoor.ToString());
            options.Remove("type");

            return _doorFactory.CreateDoor(type, options);
        }

        //Retrieves the derection of the connection
        private IDictionary<Direction, int> GetConnectionDirections(JToken jsonConnection)
        {
            var directions = new Dictionary<Direction, int>();

            //Check for each possible direction if it is linked to the connection
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                if (jsonConnection[direction.ToString()] != null)
                {
                    directions.Add(direction, int.Parse(jsonConnection[direction.ToString()].ToString()));
                }
            }

            //Each connection must have a passageway and an exit
            if (directions.Count != 2)
            {
                throw new ArgumentException("The connection is not valid.");
            }

            return directions;
        }
    }
}
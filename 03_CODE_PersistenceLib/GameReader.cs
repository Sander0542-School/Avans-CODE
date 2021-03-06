using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Connections;
using CODE_GameLib.Doors;
using CODE_GameLib.Floors;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;
using CODE_PersistenceLib.Factories.Interfaces;
using CODE_TempleOfDoom_DownloadableContent;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CODE_PersistenceLib
{
    public class GameReader
    {
        private readonly IDoorFactory _doorFactory;
        private readonly IRoomFactory _roomFactory;
        private readonly IItemFactory _roomItemFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly IFloorFactory _floorFactory;

        public GameReader(IRoomFactory roomFactory, IItemFactory roomItemFactory, IDoorFactory doorFactory, IEnemyFactory enemyFactory, IFloorFactory floorFactory)
        {
            _roomFactory = roomFactory;
            _roomItemFactory = roomItemFactory;
            _doorFactory = doorFactory;
            _enemyFactory = enemyFactory;
            _floorFactory = floorFactory;
        }

        /// <summary>
        ///     Pares the json file to a Game object
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public Game Read(string filePath)
        {
            var json = JObject.Parse(File.ReadAllText(filePath));

            var rooms = GetRooms(json["rooms"]);

            var player = GetPlayer(json["player"], rooms);

            ApplyConnection(json["connections"], rooms);

            return new Game(filePath, rooms, player);
        }

        /// <summary>
        ///     Parses a Room object to a list of rooms from json
        /// </summary>
        /// <param name="rooms"></param>
        /// <returns></returns>
        private List<RoomBase> GetRooms(JToken rooms)
        {
            try
            {
                return rooms.Select(GetRoom).ToList();
            }
            catch (Exception exception)
            {
                if (exception is ArgumentException || exception is NotImplementedException) throw;

                throw new ArgumentException("The json file does not contain valid rooms");
            }
        }

        /// <summary>
        ///     Parses a Room object from json
        /// </summary>
        /// <param name="jsonRoom"></param>
        /// <returns></returns>
        private RoomBase GetRoom(JToken jsonRoom)
        {
            try
            {
                var type = jsonRoom["type"].Value<string>();
                var id = jsonRoom["id"].Value<int>();
                var width = jsonRoom["width"].Value<int>();
                var height = jsonRoom["height"].Value<int>();

                var room = _roomFactory.CreateRoom(type, id, height, width);

                room.Items = jsonRoom["items"] != null ? GetRoomItems(jsonRoom["items"]) : new List<IItem>();
                room.Enemies = jsonRoom["enemies"] != null ? GetRoomEnemies(jsonRoom["enemies"]) : new List<Enemy>();
                room.Floors = jsonRoom["specialFloorTiles"] != null ? GetRoomFloors(jsonRoom["specialFloorTiles"]) : new List<IFloor>();

                return room;
            }
            catch (Exception exception)
            {
                if (exception is NotImplementedException) throw;

                throw new ArgumentException($"The room ({jsonRoom["id"] ?? "null"}) does not contain valid json");
            }
        }

        /// <summary>
        ///     Parses the Room items from Json
        /// </summary>
        /// <param name="jsonItems"></param>
        /// <returns></returns>
        private List<IItem> GetRoomItems(JToken jsonItems)
        {
            var items = new List<IItem>();

            foreach (var jsonItem in jsonItems)
            {
                var type = jsonItem["type"].Value<string>();
                var x = jsonItem["x"].Value<int>();
                var y = jsonItem["y"].Value<int>();

                var options = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonItem.ToString());
                options.Remove("type");
                options.Remove("x");
                options.Remove("y");

                items.Add(_roomItemFactory.CreateItem(type, x, y, options));
            }

            return items;
        }

        /// <summary>
        ///     Parses the Room enemies from Json
        /// </summary>
        /// <param name="jsonEnemies"></param>
        /// <returns></returns>
        private List<Enemy> GetRoomEnemies(JToken jsonEnemies)
        {
            var enemies = new List<Enemy>();

            foreach (var jsonEnemy in jsonEnemies)
            {
                var type = jsonEnemy["type"].Value<string>();

                var x = jsonEnemy["x"].Value<int>();
                var y = jsonEnemy["y"].Value<int>();
                var minX = jsonEnemy["minX"].Value<int>();
                var minY = jsonEnemy["minY"].Value<int>();
                var maxX = jsonEnemy["maxX"].Value<int>();
                var maxY = jsonEnemy["maxY"].Value<int>();

                enemies.Add(_enemyFactory.CreateEnemy(type, x, y, minX, minY, maxX, maxY));
            }

            return enemies;
        }

        /// <summary>
        ///     Parses the Special Floor Tiles from Json
        /// </summary>
        /// <param name="jsonFloors"></param>
        /// <returns></returns>
        private List<IFloor> GetRoomFloors(JToken jsonFloors)
        {
            var floors = new List<IFloor>();

            foreach (var jsonFloor in jsonFloors)
            {
                var type = jsonFloor["type"].Value<string>();

                var x = jsonFloor["x"].Value<int>();
                var y = jsonFloor["y"].Value<int>();

                var options = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonFloor.ToString());
                options.Remove("type");
                options.Remove("x");
                options.Remove("y");

                floors.Add(_floorFactory.CreateFloor(type, x, y, options));
            }

            return floors;
        }

        /// <summary>
        ///     Parses the player and startroom object from Json
        /// </summary>
        /// <param name="jsonPlayer"></param>
        /// <param name="rooms"></param>
        /// <returns></returns>
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
                throw new ArgumentException("The json file does not contain a valid player");
            }
        }

        /// <summary>
        ///     Adds the connections from the json and adds them to the rooms
        /// </summary>
        /// <param name="jsonConnections"></param>
        /// <param name="rooms"></param>
        private void ApplyConnection(JToken jsonConnections, IEnumerable<RoomBase> rooms)
        {
            try
            {
                //Each connection is added to a room
                foreach (var jsonConnection in jsonConnections)
                {
                    var door = GetConnectionDoor(jsonConnection);

                    if (jsonConnection["portal"] != null)
                    {
                        var roomOne = rooms.First(room => room.Id == jsonConnection["portal"][0]["roomId"].Value<int>());
                        var roomTwo = rooms.First(room => room.Id == jsonConnection["portal"][1]["roomId"].Value<int>());

                        var roomOneX = jsonConnection["portal"][0]["x"].Value<int>();
                        var roomOneY = jsonConnection["portal"][0]["y"].Value<int>();
                        var roomTwoX = jsonConnection["portal"][1]["x"].Value<int>();
                        var roomTwoY = jsonConnection["portal"][1]["y"].Value<int>();

                        roomOne.Portals.Add(new Tuple<int, int>(roomOneX, roomOneY), new Portal
                        {
                            TargetRoom = roomTwo,
                            Door = door,
                            TargetX = roomTwoX,
                            TargetY = roomTwoY
                        });

                        roomTwo.Portals.Add(new Tuple<int, int>(roomTwoX, roomTwoY), new Portal
                        {
                            TargetRoom = roomOne,
                            Door = door,
                            TargetX = roomOneX,
                            TargetY = roomOneY
                        });
                    }
                    else
                    {
                        var directions = GetConnectionDirections(jsonConnection);

                        var roomOne = rooms.First(room => room.Id == directions.First().Value);
                        var roomTwo = rooms.First(room => room.Id == directions.Last().Value);

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
            }
            catch (Exception)
            {
                throw new ArgumentException("The json file does not contain a valid connection");
            }
        }

        /// <summary>
        ///     Gets the door belonging to the connection
        /// </summary>
        /// <param name="jsonConnection"></param>
        /// <returns></returns>
        private IDoor GetConnectionDoor(JToken jsonConnection)
        {
            //Connection does not always have a door
            if (jsonConnection["door"] == null) return null;

            var jsonDoor = jsonConnection["door"];

            var type = jsonDoor["type"].Value<string>();

            var options = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonDoor.ToString());
            options.Remove("type");

            return _doorFactory.CreateDoor(type, options);
        }

        /// <summary>
        ///     Retrieves the derection of the connection
        /// </summary>
        /// <param name="jsonConnection"></param>
        /// <returns></returns>
        private IDictionary<Direction, int> GetConnectionDirections(JToken jsonConnection)
        {
            var directions = new Dictionary<Direction, int>();

            //Check for each possible direction if it is linked to the connection
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                if (jsonConnection[direction.ToString()] != null)
                    directions.Add(direction, jsonConnection[direction.ToString()].Value<int>());

            //Each connection must have a passageway and an exit
            if (directions.Count != 2) throw new ArgumentException("The connection is not valid.");

            return directions;
        }
    }
}
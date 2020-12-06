using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CODE_GameLib;
using CODE_GameLib.Items;
using CODE_GameLib.Rooms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        private readonly RoomFactory _roomFactory = new RoomFactory();
        private readonly RoomItemFactory _roomItemFactory = new RoomItemFactory();

        public Game Read(string filePath)
        {
            var json = JObject.Parse(File.ReadAllText(filePath));

            var rooms = GetRooms(json["rooms"]);
            
            var player = GetPlayer(json["player"], rooms);

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

                room.Items = jsonRoom["items"] != null ? GetRoomItems(jsonRoom["items"]) : new List<IRoomItem>();

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

        private List<IRoomItem> GetRoomItems(JToken jsonItems)
        {
            var items = new List<IRoomItem>();

            foreach (var jsonItem in jsonItems)
            {
                var type = jsonItem["type"].ToString();
                var x = int.Parse(jsonItem["x"].ToString());
                var y = int.Parse(jsonItem["y"].ToString());

                var options = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonItem.ToString());
                options.Remove("type");
                options.Remove("x");
                options.Remove("y");

                items.Add(_roomItemFactory.CreateRoomItem(type, x, y, options));
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

                return new Player
                {
                    Lives = lives,
                    X = startX,
                    Y = startY,
                    Room = rooms.First(room => room.Id == startRoomId)
                };
            }
            catch (Exception exception)
            {
                throw new ArgumentException($"The json file does not contain a valid player");
            }
        }
    }
}
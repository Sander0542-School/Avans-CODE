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
            
            return new Game
            {
                Rooms = GetRooms(json["rooms"])
            };
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

                if (jsonRoom["items"] != null)
                {
                    room.Items = GetRoomItems(jsonRoom["items"]);
                }

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
    }
}
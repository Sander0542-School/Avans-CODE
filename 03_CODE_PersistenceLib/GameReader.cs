using System;
using System.Collections.Generic;
using System.IO;
using CODE_GameLib;
using CODE_GameLib.Rooms;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        private RoomFactory _roomFactory = new RoomFactory();
        private RoomBase _room;
        public Game Read(string filePath)
        {
            var json = JObject.Parse(File.ReadAllText(filePath));
            var game = new Game();
            List<RoomBase> rooms = new List<RoomBase>();
            foreach (var jsonRoom in json["rooms"])
            {
                _room = _roomFactory.CreateRoom(jsonRoom["type"].ToString(), int.Parse(jsonRoom["id"].ToString()), int.Parse(jsonRoom["height"].ToString()), int.Parse(jsonRoom["width"].ToString()));
                rooms.Add(_room);
            }

            game.Rooms = rooms;
            return game;
        }
    }
}
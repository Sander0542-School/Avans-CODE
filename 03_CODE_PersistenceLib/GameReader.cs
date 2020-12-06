using System;
using System.IO;
using CODE_GameLib;
using CODE_GameLib.Rooms;
using Newtonsoft.Json.Linq;

namespace CODE_FileSystem
{
    public class GameReader
    {
        public Game Read(string filePath)
        {
            var json = JObject.Parse(File.ReadAllText(filePath));
            var game = new Game();

            foreach (var jsonRoom in json["rooms"])
            {
                switch (jsonRoom["type"].ToString())
                {
                    case "room":
                        
                    default:
                        throw new NotImplementedException("This type of room is not yet implemented.");
                }
            }

            return game;
        }
    }
}
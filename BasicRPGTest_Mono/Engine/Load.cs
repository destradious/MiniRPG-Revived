﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using MonoGame.Extended.Tiled;
using Newtonsoft.Json.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using RPGEngine;
using BasicRPGTest_Mono.Engine.Maps;

namespace BasicRPGTest_Mono.Engine
{
    public static class Load
    {
        public static string path { get; set; }
        public static StreamReader reader { get; set; }

        static Load()
        {
        }

        public static Dictionary<string, Object> loadPlayer(string world)
        {
            Dictionary<string, Object> playerData = new Dictionary<string, object>();

            path = $"save\\{world}";
            reader = new StreamReader(path + "\\player.json");

            JObject playerJson = JObject.Parse(reader.ReadToEnd());
            JObject posData = playerJson.Value<JObject>("position");

            playerData.Add("position", new Vector2(posData.Value<int>("x"), posData.Value<int>("y")));

            reader.Close();

            return playerData;
        }
        public static List<TileLayer> loadMap(string world, string map) 
        {
            List<TileLayer> layers = new List<TileLayer>();

            path = $"save\\{world}\\maps";
            reader = new StreamReader($"save\\{world}\\world.json");

            DirectoryInfo dirInfo = new DirectoryInfo(path);
            FileInfo[] files = dirInfo.GetFiles();
            List<Tile> tiles;

            foreach (FileInfo file in files)
            {
                reader = new StreamReader($"{path}\\{file.Name}");
                JObject jsonLayer = JObject.Parse(reader.ReadToEnd());

                tiles = new List<Tile>();
                TileLayer layer = new TileLayer(jsonLayer.Value<string>("layer"));
                JArray tileArray = jsonLayer.Value<JArray>("tiles");
                Tile tile;

                foreach (JObject tileJson in tileArray)
                {
                    Tile template = TileManager.get(tileJson.Value<int>("id"));
                    if (template == null) continue;

                    int x = tileJson.Value<int>("x");
                    int y = tileJson.Value<int>("y");

                    tile = new Tile(template, new Vector2(x, y));

                    layer.setTile(tile.tilePos, tile);
                    tile = null;
                }

                layers.Add(layer);

                reader.Close();

                layer = null;

                GC.Collect();

            }

            return layers;

        }

    }
}

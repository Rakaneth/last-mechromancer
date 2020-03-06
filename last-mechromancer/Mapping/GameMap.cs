using System;
using System.Collections.Generic;
using GoRogue;
using GoRogue.MapGeneration;
using GoRogue.MapViews;
using GoRogue.GameFramework;
using Microsoft.Xna.Framework;
using Troschuetz.Random;
using Troschuetz.Random.Generators;

namespace last_mechromancer.Mapping {
    public enum MapType {
        DUNGEON,
        CAVES
    }
    public static class MapBuilder {
        private const int MIN_ROOMS = 10;
        private const int MAX_ROOMS = 50;
        private const int ROOM_MIN_SIZE = 5;
        private const int ROOM_MAX_SIZE = 21;
        static private IGenerator _rng = new XorShift128Generator(0xDEADBEEF);
        public static Map Build(MapType mt, int width, int height, string id, Color wallColor, Color floorColor) {
            var baseMap = new ArrayMap<Terrain>(width, height);
            var trans = new TerrainTranslator(baseMap, wallColor, floorColor);

            List<Coord> doors = new List<Coord>();
            switch (mt) {
                case MapType.DUNGEON:
                    var dunResult = QuickGenerators.GenerateDungeonMazeMap(trans, _rng, 
                        MIN_ROOMS, MAX_ROOMS, ROOM_MIN_SIZE, ROOM_MAX_SIZE);
                    foreach (var (room, connectors) in dunResult) 
                        foreach (var side in connectors)
                            foreach (var door in side)
                                doors.Add(door);
                    break;
                case MapType.CAVES:
                    var caveResult = QuickGenerators.GenerateCellularAutomataMap(trans, _rng);
                    break;
            }

            var fullMap = Map.CreateMap(baseMap, 4, Distance.CHEBYSHEV);
            /*
            foreach (var newDoorSpot in doors)
                fullMap.AddEntity(new Door(newDoorSpot, false))
            */
            return fullMap;
        }
    }
}

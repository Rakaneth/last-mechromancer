using GoRogue.MapViews;
using Microsoft.Xna.Framework;

namespace last_mechromancer.Mapping {
    class TerrainTranslator : SettableTranslationMap<Terrain, bool> {
        public Color WallColor {get; }
        public Color FloorColor {get;}
        public TerrainTranslator(ISettableMapView<Terrain> baseMap, Color wallColor, Color floorColor)
            : base(baseMap) {
                WallColor = wallColor;
                FloorColor = floorColor;
        }
        protected override bool TranslateGet(Terrain value) {
            return value.IsWalkable;
        }

        protected override Terrain TranslateSet(GoRogue.Coord pos, bool value) {
            return value ? Terrain.Floor(pos, FloorColor) : Terrain.Wall(pos, WallColor);
        }
    }
}
using System;
using System.Collections.Generic;
using GoRogue;
using SadConsole;
using Microsoft.Xna.Framework;
using last_mechromancer.UI;
using last_mechromancer.Entity;

namespace last_mechromancer.Mapping {
    public class Terrain : GoRogue.GameFramework.GameObject {
        public string Name {get;}
        public char Glyph {get;}
       // public Cell DrawCell {get;}
        private Terrain(string name, char glyph, Coord pos, Color? fg = null, Color? bg = null, bool isWalkable = true, bool isTransparent = true)
            : base(pos, 0, null, true, isWalkable, isTransparent) {
                Name = name;
                Glyph = glyph;
                var FG = fg ?? Color.Transparent;
                var BG = bg ?? Color.Transparent;
                this.AddComponent(new DrawComponent(glyph, FG, BG));
        }

        public static Terrain Floor(Coord pos, Color floorColor) {
            return new Terrain("Floor", '.', pos, fg: floorColor);
        }

        public static Terrain Wall(Coord pos, Color wallColor) {
            return new Terrain("Wall", '#', pos, fg: wallColor, isWalkable: false, isTransparent: false);
        }

        public static Terrain NullTile(Coord pos) {
            return new Terrain("", '\0', pos, isWalkable: false, isTransparent: false);
        }

        public static Terrain DownStairs(Coord pos) {
            return new Terrain("Stairs down", '>', pos, fg: Swatch.Stairs);
        }

        public static Terrain UpStairs(Coord pos) {
            return new Terrain("Stairs up", '<', pos, fg: Swatch.Stairs);
        }
    }
}
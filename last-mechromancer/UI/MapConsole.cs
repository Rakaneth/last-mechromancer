using System;
using System.Collections.Generic;
using System.Linq;
using GoRogue.MapViews;
using GoRogue.GameFramework;
using SadConsole;
using last_mechromancer.Mapping;
using last_mechromancer.Entity;

namespace last_mechromancer.UI {
    public class MapConsole : ScrollingConsole {
        private Map _map;
        private const int VW_WIDTH = 60;
        private const int VW_HEIGHT = 30;
        public IGameObject Focus { get; set; }
        public MapConsole(Map map) : base(map.Width, map.Height) {
            _map = map;
            ViewPort = new Microsoft.Xna.Framework.Rectangle(0, 0, VW_WIDTH, VW_HEIGHT);
            foreach (var pos in _map.Positions()) {
                var terrain = _map.GetTerrain<Terrain>(pos);
                if (terrain != null) {
                    var comp = terrain.GetComponent<DrawComponent>();
                    Children.Add(comp.Entity);
                }
                foreach (var thing in _map.GetEntities<IGameObject>(pos)) {
                    var thingComp = thing.GetComponent<DrawComponent>();
                    Children.Add(thingComp.Entity);
                }
            }
        }

        private void CenterOnFocus() {
            if (Focus != null) this.CenterViewPortOnPoint(Focus.Position);
        }

        public override void Draw(TimeSpan timeElapsed) {
            CenterOnFocus();
            base.Draw(timeElapsed);
        }


    }
}
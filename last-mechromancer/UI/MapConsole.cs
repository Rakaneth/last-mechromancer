using System;
using GoRogue.MapViews;
using GoRogue.GameFramework;
using SadConsole;
using last_mechromancer.Mapping;

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

            }
        }

        public override void Draw(TimeSpan timeElapsed) {


            base.Draw(timeElapsed);
        }


    }
}
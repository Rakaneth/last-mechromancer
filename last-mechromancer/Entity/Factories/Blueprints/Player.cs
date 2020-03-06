using System;
using GoRogue.Factory;
using GoRogue.GameFramework;

namespace last_mechromancer.Entity.Factories.Blueprints {
    public class PlayerBlueprint : SimpleBlueprint<IGameObject> {
        private string _name;
        private GoRogue.Coord _pos;

        public PlayerBlueprint(string name, GoRogue.Coord pos) : base("player") {
            _name = name;
            _pos = pos;
        }

        public PlayerBlueprint(string name) : this(name, new GoRogue.Coord(0, 0)) { }
        public override IGameObject Create() {
            var foetus = new GameObject(_pos, 3, null, false, false, true);
            foetus.AddComponent(new IdentityComponent(_name, Id));
            foetus.AddComponent(new BatteryComponent());
            foetus.AddComponent(new PoweredSuitComponent());
            foetus.AddComponent(new PlayerComponent());
            foetus.AddComponent(new DrawComponent());
            return foetus;
        }
    }


}
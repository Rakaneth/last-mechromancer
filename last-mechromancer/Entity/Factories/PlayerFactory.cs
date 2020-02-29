using GoRogue.Factory;
using GoRogue.GameFramework;
using last_mechromancer.Entity.Factories.Blueprints;

namespace last_mechromancer.Entity.Factories {
    public class PlayerFactory : Factory<IGameObject> {
        private string _name;
        public PlayerFactory() {
            _name = "No name";
        }

        public PlayerFactory SetName(string name) {
            _name = name;
            Add(new PlayerBlueprint(_name));
            return this;
        }

        public IGameObject Build() {
            return Create("player");
        }
    }
}
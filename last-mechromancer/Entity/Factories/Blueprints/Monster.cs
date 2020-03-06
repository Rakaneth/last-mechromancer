using GoRogue;
using GoRogue.Factory;
using GoRogue.GameFramework;

namespace last_mechromancer.Entity.Factories.Blueprints {

    public class MonsterBlueprintConfig : BlueprintConfig {
        public string Name { get; set; }
        public int PhysAtk { get; set; }
        public int PhysDef { get; set; }
        public int MystAtk { get; set; }
        public int MystDef { get; set; }
        public int HeatAtk { get; set; }
        public int HeatDef { get; set; }
        public int ColdAtk { get; set; }
        public int ColdDef { get; set; }
        public int ShockAtk { get; set; }
        public int ShockDef { get; set; }
        public int MaxHP { get; set; }
    }



    public class MonsterBlueprint : IBlueprint<MonsterBlueprintConfig, IGameObject> {
        public string Id { get; } = "monster";

        public IGameObject Create(MonsterBlueprintConfig config) {
            var foetus = new GameObject(new Coord(0, 0), 2, null, false, false, true);
            foetus.AddComponent(new IdentityComponent(config.Name, Id));
            foetus.AddComponent(new BaseAtkComponent(config.MystAtk,
                config.PhysAtk, config.HeatAtk, config.ColdAtk, config.ShockAtk));
            foetus.AddComponent(new BaseDefComponent(config.MystDef,
                config.PhysDef, config.HeatDef, config.ColdDef, config.ShockDef));
            foetus.AddComponent(new BaseHealthComponent(config.MaxHP));
            foetus.AddComponent(new MonsterComponent());
            return foetus;
        }
    }
}

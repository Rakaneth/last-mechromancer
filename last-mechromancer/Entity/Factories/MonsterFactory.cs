using System.Collections.Generic;
using GoRogue.Factory;
using GoRogue.GameFramework;
using last_mechromancer.Entity.Factories.Blueprints;


namespace last_mechromancer.Entity.Factories {

    public class EntityFactory {
        private PlayerFactory _playerFactory;
        private Factory<MonsterBlueprintConfig, IGameObject> _monsterFactory;
        private Dictionary<string, MonsterBlueprintConfig> _blueprints;
        private static EntityFactory _instance;
        private const string MONSTER_LOG = "FACTORY-MONSTER";
        private bool _monsterTableUpdated;
        public static EntityFactory Instance {
            get {
                if (_instance == null)
                    _instance = new EntityFactory();
                return _instance;
            }
        }
        private EntityFactory() {
            _playerFactory = new PlayerFactory();
            _monsterFactory = new Factory<MonsterBlueprintConfig, IGameObject>();
            _monsterFactory.Add(new MonsterBlueprint());
        }

        public void UpdateMonsterBlueprints(string filename) {
            _blueprints = Utils.ParseYaml<Dictionary<string, MonsterBlueprintConfig>>(filename);
            _monsterTableUpdated = true;
        }

        public IGameObject MakePlayer(string name) {
            return _playerFactory.SetName(name).Build();
        }

        public IGameObject MakeMonster(string buildID) {
            if (!_monsterTableUpdated) {
                Utils.LogError(MONSTER_LOG, "Blueprints not loaded; please load with UpdateBlueprints()");
                return null;
            }
            
            if (!_blueprints.ContainsKey(buildID)) {
                Utils.LogError("FACTORY-MONSTER", $"No template for {buildID} found");
                return null;
            }
            
            Utils.LogInfo("FACTORY-MONSTER", $"Creating {buildID}");
            return _monsterFactory.Create("monster", _blueprints[buildID]);
        }
                                                                                              
    }

}
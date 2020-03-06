using NUnit.Framework;
using last_mechromancer.Entity;
using last_mechromancer.Entity.Factories;
using GoRogue.GameFramework;

namespace last_mechromancer_test {
    public class Tests {

        EntityFactory basicFactory;
        EntityFactory emptyFactory;
        [SetUp]
        public void Setup() {
            basicFactory = new EntityFactory();
            emptyFactory = new EntityFactory();
            basicFactory.UpdateMonsterBlueprints("Data/monsters.yaml");
        }

        [Test]
        public void BasicEntityFactoryTest() {
            //properly initialized factories should make players and monsters
            var player = basicFactory.MakePlayer("Steve");
            var monster = basicFactory.MakeMonster("rat");
            
            Assert.That(player, Is.Not.Null);
            Assert.That(monster, Is.Not.Null);

            var playerName = player.GetComponent<IdentityComponent>().Name;
            var ratAtk = monster.GetComponent<IAtkComponent>().PhysAtk.FinalValue;
            Assert.That(playerName, Is.EqualTo("Steve"));
            Assert.That(ratAtk, Is.EqualTo(1));
        }

        [Test]
        public void EmptyEntityFactoryTest() {
            //uninitiailized factories cannot make monsters
            var uninited = emptyFactory.MakeMonster("rat");
            Assert.That(uninited, Is.Null);

            //nonexistent build IDs cannot make monsters
            emptyFactory.UpdateMonsterBlueprints("Data/monsters.yaml");
            var nonexistent = emptyFactory.MakeMonster("nonexistent");
            Assert.That(nonexistent, Is.Null);
        }
    }
}
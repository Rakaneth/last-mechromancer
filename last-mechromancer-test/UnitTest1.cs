using NUnit.Framework;
using last_mechromancer.Entity;
using last_mechromancer.Entity.Factories;
using GoRogue.GameFramework;

namespace last_mechromancer_test {
    public class Tests {
        [SetUp]
        public void Setup() {
        }

        [Test]
        public void BasicEntityFactoryTest() {
            EntityFactory.Instance.UpdateMonsterBlueprints("Data/monsters.yaml");
            var player = EntityFactory.Instance.MakePlayer("Steve");
            var monster = EntityFactory.Instance.MakeMonster("rat");

            Assert.That(player, Is.Not.Null);
            Assert.That(monster, Is.Not.Null);

            var playerName = player.GetComponent<IdentityComponent>().Name;
            var ratAtk = monster.GetComponent<IAtkComponent>().PhysAtk.FinalValue;
            Assert.That(playerName, Is.EqualTo("Steve"));
            Assert.That(ratAtk, Is.EqualTo(1));
        }
    }
}
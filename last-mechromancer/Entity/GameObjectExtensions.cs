using System;
using GoRogue.GameFramework;

namespace last_mechromancer.Entity {
    public static class GameObjectExtensions {
        public static bool IsPlayer(this IGameObject obj) => obj.HasComponent<PlayerComponent>();
        public static bool IsMonster(this IGameObject obj) => obj.HasComponent<MonsterComponent>();
    }
}

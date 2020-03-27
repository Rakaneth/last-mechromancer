using System;
using GoRogue.GameFramework;

namespace last_mechromancer.Entity {
    public static class GameObjectExtensions {
        public static bool IsPlayer(this IGameObject obj) => obj.HasComponent<PlayerComponent>();
        public static bool IsMonster(this IGameObject obj) => obj.HasComponent<MonsterComponent>();
        public static void Kill(this IGameObject obj) {
            if (obj.IsPlayer()) {
                var batt = obj.GetComponent<BatteryComponent>();
                batt.CurHP = 0;
                batt.CurrCharge = 0;                
            } else {
                var healthComp = obj.GetComponent<IHealthComponent>();
                healthComp.CurHP = 0;
            }
        }

        public static void FullRestore(this IGameObject obj) {
            if (obj.IsPlayer()) {
                var batt = obj.GetComponent<BatteryComponent>();
                batt.CurHP = batt.MaxHP;
                batt.CurrCharge = batt.MaxCharge;
            } else {
                var health = obj.GetComponent<IHealthComponent>();
                health.CurHP = health.MaxHP;
            }
        }

        public static string DisplayMessageString(this IGameObject obj) {
            var display = obj.GetComponent<DrawComponent>();
            var id = obj.GetComponent<IdentityComponent>();
            if (id == null)
                return "Something";
            else if (display == null)
                return id.Name;
            
            return Utils.Decorate(id.Name, display.FG.ToString(), display.BG.ToString());
        }
    }
}

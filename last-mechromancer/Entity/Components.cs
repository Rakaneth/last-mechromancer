using GoRogue;
using GoRogue.GameFramework;
using GoRogue.GameFramework.Components;
using System;
using static last_mechromancer.Utils;
using last_mechromancer.UI;
using SadConsole;
using Microsoft.Xna.Framework;

namespace last_mechromancer.Entity {

    public interface IDefComponent {
        Stat PhysDef { get; }
        Stat MystDef { get; }
        Stat HeatDef { get; }
        Stat ColdDef { get; }
        Stat ShockDef { get; }
    }

    public interface IAtkComponent {
        Stat PhysAtk { get; }
        Stat MystAtk { get; }
        Stat HeatAtk { get; }
        Stat ColdAtk { get; }
        Stat ShockAtk { get; }
    }

    public interface IHealthComponent {
        int CurHP { get; set; }
        int MaxHP { get; set; }
        bool Alive();
    }

    public class MonsterComponent : IGameObjectComponent {
        public IGameObject Parent { get; set; }
    }

    public class PlayerComponent : IGameObjectComponent {
        public IGameObject Parent { get; set; }
    }

    public class BatteryComponent : IHealthComponent, IGameObjectComponent {
        public IGameObject Parent { get; set; }

        private int _currCharge;
        private int _currHP;
        public int DrainCharge { get; set; }
        public int MaxCharge { get; set; }
        public int ReCharge { get; set; }
        public int CurrCharge {
            get => _currCharge;
            set {
                _currCharge = Clamp(value, 0, MaxCharge);
            }
        }
        public int MaxHP { get; set; }
        public int CurHP {
            get => _currHP;
            set {
                if (value < 0) {
                    CurrCharge += value;
                }
                _currHP = Clamp(value, 0, MaxHP);
            }
        }

        public bool BatteryEmpty => CurrCharge == 0;
        public bool NoHP => CurHP == 0;
        public BatteryComponent(int startCharge = 10, int startRecharge = 1) {
            MaxCharge = startCharge;
            ReCharge = startCharge;
            _currCharge = startCharge;
            DrainCharge = 0;
            MaxHP = 10;
            CurHP = 10;
        }

        public bool Alive() => !(BatteryEmpty && NoHP);
    }

    public class PoweredSuitComponent : IAtkComponent, IDefComponent, IGameObjectComponent {

        public IGameObject Parent { get; set; }
        public int UpgradeLevel { get; private set; }
        public Stat MystAtk { get; }
        public Stat MystDef { get; }
        public Stat PhysAtk { get; }
        public Stat PhysDef { get; }
        public Stat ColdAtk { get; }
        public Stat ColdDef { get; }
        public Stat HeatAtk { get; }
        public Stat HeatDef { get; }
        public Stat ShockAtk { get; }
        public Stat ShockDef { get; }

        public PoweredSuitComponent() {
            UpgradeLevel = 1;
            MystAtk = new Stat(Strings.MYST_ATK, 0);
            MystDef = new Stat(Strings.MYST_DEF, 0);
            PhysAtk = new Stat(Strings.PHYS_ATK, 15);
            PhysDef = new Stat(Strings.PHYS_DEF, 15);
            ColdAtk = new Stat(Strings.COLD_ATK, 0);
            ColdDef = new Stat(Strings.COLD_DEF, 0);
            HeatAtk = new Stat(Strings.HEAT_ATK, 0);
            HeatDef = new Stat(Strings.HEAT_DEF, 0);
            ShockAtk = new Stat(Strings.SHOCK_ATK, 0);
            ShockDef = new Stat(Strings.SHOCK_DEF, 0);
        }
    }

    public class BaseHealthComponent : IHealthComponent, IGameObjectComponent {
        private int _curHP;
        public IGameObject Parent { get; set; }

        public int CurHP {
            get => _curHP;
            set {
                _curHP = Clamp(value, 0, MaxHP);
            }
        }
        public int MaxHP { get; set; }

        public bool Alive() => _curHP > 0;
        public BaseHealthComponent(int maxHP) {
            MaxHP = maxHP;
            _curHP = maxHP;
        }

    }

    public class BaseAtkComponent : IAtkComponent, IGameObjectComponent {
        public Stat MystAtk { get; }
        public Stat PhysAtk { get; }
        public Stat HeatAtk { get; }
        public Stat ColdAtk { get; }
        public Stat ShockAtk { get; }
        public IGameObject Parent { get; set; }

        public BaseAtkComponent(int myst = 0, int phys = 0, int heat = 0, int cold = 0, int shock = 0) {
            MystAtk = new Stat(Strings.MYST_ATK, myst);
            PhysAtk = new Stat(Strings.PHYS_ATK, phys);
            HeatAtk = new Stat(Strings.HEAT_ATK, heat);
            ColdAtk = new Stat(Strings.COLD_ATK, cold);
            ShockAtk = new Stat(Strings.SHOCK_ATK, shock);
        }
    }

    public class BaseDefComponent : IDefComponent, IGameObjectComponent {
        public Stat MystDef { get; }
        public Stat PhysDef { get; }
        public Stat HeatDef { get; }
        public Stat ColdDef { get; }
        public Stat ShockDef { get; }
        public IGameObject Parent { get; set; }

        public BaseDefComponent(int myst = 0, int phys = 0, int heat = 0, int cold = 0, int shock = 0) {
            MystDef = new Stat(Strings.MYST_DEF, myst);
            PhysDef = new Stat(Strings.PHYS_DEF, phys);
            HeatDef = new Stat(Strings.HEAT_DEF, heat);
            ColdDef = new Stat(Strings.COLD_DEF, cold);
            ShockDef = new Stat(Strings.SHOCK_DEF, shock);
        }
    }

    public class IdentityComponent : IGameObjectComponent {
        public IGameObject Parent { get; set; }
        public string Name { get; set; }
        public string BuildID { get; }

        public IdentityComponent(string name, string buildID) {
            Name = name;
            BuildID = buildID;
        }
    }

    public class DrawComponent : IGameObjectComponent {
        public IGameObject Parent {get; set;}
        public char Glyph {get; set;}
        public Color FG {get; set;}
        public Color BG {get; set;}

        public DrawComponent(char glyph='@', string fg="white", string bg="transparent") {
            bool keepR, keepG, keepB, keepA, dfault;
            Glyph = glyph;
            FG = Color.White.FromParser(fg, out keepR, out keepG, out keepB, out keepA, out dfault);
            BG = Color.White.FromParser(fg, out keepR, out keepG, out keepB, out keepA, out dfault);
        }
    }



}
using System;
using System.Collections.Generic;
using System.Linq;

namespace last_mechromancer.Entity {
    public class BaseStat {
        public delegate void OnStatChange(BaseStat stat, int newVal, float newMult);
        protected int _baseValue;
        protected float _baseMult;
        public int BaseValue {
            get => _baseValue;
            set {
                ChangeHandler?.Invoke(this, value, _baseMult);
                _baseValue = value;
            }
        }
        public float BaseMult {
            get => _baseMult;
            set {
                ChangeHandler?.Invoke(this, _baseValue, value);
                _baseMult = value;
            }
        }
        public string Name { get; }

        protected event OnStatChange ChangeHandler;

        public BaseStat(string name, int baseValue = 0, float baseMult = 0f) {
            _baseValue = baseValue;
            _baseMult = baseMult;
            Name = name;
        }
    }

    public class Bonus : BaseStat {
        public int Duration { get; private set; }
        public const int INFINITY = -1;
        public bool Expired => Duration == 0;
        public Bonus(string name, int baseValue = 0, float baseMult = 0f, int duration = Bonus.INFINITY)
            : base(name, baseValue, baseMult) {
            Duration = duration;
        }

        public void Tick(int turns = 1) {
            if (Duration <= Bonus.INFINITY)
                return;
            Duration = Math.Max(0, Duration - turns);
        }

        public void Merge(Bonus other) {
            /*
                All bonuses take the highest duration,
                base value, and base multiplier when they
                merge.
            */

            Duration = Math.Max(Duration, other.Duration);
            BaseValue = Math.Max(BaseValue, other.BaseValue);
            BaseMult = Math.Max(BaseMult, other.BaseMult);
        }
    }

    public class Stat : BaseStat {
        public List<Bonus> Bonuses { get; }
        public delegate void OnBonusChange();
        public delegate void OnFinalChange();
        private event OnBonusChange BonusChanged;
        public event OnFinalChange FinalChanged;
        private int _finalValue;
        public int FinalValue {
            get => _finalValue;
            set {
                _finalValue = value;
                FinalChanged?.Invoke();
            }
        }

        public Stat(string name, int baseValue) : base(name, baseValue, 0f) {
            Bonuses = new List<Bonus>();
            ChangeHandler += updateFinalValue;
            BonusChanged += () => updateFinalValue(this, this.BaseValue, this.BaseMult);
            updateFinalValue(this, BaseValue, BaseMult);
        }

        public void AddBonus(Bonus bonus) {
            var existingBonus = Bonuses.FirstOrDefault(b => b.Name == bonus.Name);
            if (existingBonus == null) {
                Bonuses.Add(bonus);
            } else {
                existingBonus.Merge(bonus);
            }
            BonusChanged?.Invoke();
        }

        private void updateFinalValue(BaseStat stat, int newBase, float newMult) {
            int baseAcc = Bonuses.Sum(b => b.BaseValue);
            float multAcc = Bonuses.Sum(b => b.BaseMult);
            FinalValue = (int)((_baseValue + baseAcc) * (1 + _baseMult + multAcc));
        }

        public void RemoveBonus(Bonus bonus) {
            if (!Bonuses.Remove(bonus))
                return;
            BonusChanged?.Invoke();
        }

        public void Tick(int turns = 1) {
            bool removed = false;
            for (int i = Bonuses.Count - 1; i >= 0; i--) {
                var bonus = Bonuses[i];
                bonus.Tick(turns);
                if (bonus.Expired) {
                    Bonuses.RemoveAt(i);
                    removed = true;
                }

            }

            if (removed)
                BonusChanged?.Invoke();
        }

        public override string ToString() => $"{Name} {FinalValue}";
    }
}
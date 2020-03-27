using GoRogue.GameFramework;
using GoRogue;
using last_mechromancer.UI;
using System;
using System.Linq;
using System.Collections.Generic;

namespace last_mechromancer.Entity.Actions {
    public abstract class Action {


        /// <summary>
        /// Performs the given action on obj.
        /// <returns>
        /// Returns an alternate action to try as a result of this action.
        /// Returns null if no further action needs to be processed.
        /// </returns>
        /// </summary>
        public abstract Action Execute();
    }

    
    public class AttackAction : Action {
        private GameObject _defender;
        private GameObject _attacker;

        public AttackAction(GameObject attacker, GameObject defender) {
            _defender = defender;
        }

        public override Action Execute() {
            MessageBus.Instance.Send(new GameLogMessage($"{_attacker.DisplayMessageString()} attacks {_defender.DisplayMessageString()}"));
            return null;
        }
    }

    public class MoveAction : Action {
        private Coord _from;
        private Coord _to;
        private GameObject _mover;

        public MoveAction (GameObject mover, Coord from, Coord to) {
            _from = from;
            _to = to;
            _mover = mover;
        }

        public MoveAction(GameObject mover, Direction dir) 
        : this(mover, mover.Position, mover.Position.Translate(dir.DeltaX, dir.DeltaY)) {}

        public override Action Execute() {
            var thing = _mover.CurrentMap.GetEntity<GameObject>(_to, 1);
            if (thing == null) {
                _mover.Position = _to;
                return null;
            }

            return new AttackAction(_mover, thing);
        }
    }
}
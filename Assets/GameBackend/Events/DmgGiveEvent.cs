using System.Collections.Generic;
using UnityEngine;

namespace GameBackend.Events
{
    public class DmgGiveEvent:EventArgs
    {
        public int trueDmg { get; }
        public Entity attacker { get; }
        public Entity target { get; }
        public float force { get; }
        public List<AtkTags> atkTags { get; }

        public DmgGiveEvent(int trueDmg, float force, Entity attacker, Entity target, List<AtkTags> atkTags)
        {
            name="DmgGiveEvent";
            this.trueDmg = trueDmg;
            this.force = force;
            this.attacker = attacker;
            this.target = target;
            this.atkTags = atkTags;
            
            attacker.eventActive(this);
            target.dmgtake(this);
        }
    }
}
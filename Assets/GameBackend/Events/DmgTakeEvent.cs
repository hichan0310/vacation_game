using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameBackend.Events
{
    public class DmgTakeEvent:EventArgs
    {
        public int realDmg { get; }
        public Entity attacker { get; }
        public Entity target { get; }
        public List<AtkTags> atkTags { get; }


        public DmgTakeEvent(int realDmg, Entity attacker, Entity target, List<AtkTags> atkTags)
        {
            name="DmgTakeEvent";
            this.realDmg = realDmg;
            this.attacker = attacker;
            this.target = target;
            this.atkTags = atkTags;
            
            DmgEventManager.Instance.TriggerDmgTakeEvent(this);
        }
    }
}
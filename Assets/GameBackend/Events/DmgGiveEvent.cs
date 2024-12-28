﻿using System.Collections.Generic;

namespace GameBackend.Events
{
    public class DmgGiveEvent:EventArgs
    {
        public int trueDmg { get; }
        public Entity attacker { get; }
        public Entity target { get; }
        public List<AtkTags> atkTags { get; }

        public DmgGiveEvent(int trueDmg, Entity attacker, Entity target, List<AtkTags> atkTags)
        {
            name="DmgGiveEvent";
            this.trueDmg = trueDmg;
            this.attacker = attacker;
            this.target = target;
            this.atkTags = atkTags;
        }
    }
}
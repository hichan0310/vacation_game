using System.Collections.Generic;

namespace GameBackend.Events
{
    public class DmgInfo
    {
        public int trueDmg { get; }
        public float force { get; }
        public Entity attacker { get; }
        public List<AtkTags> atkTags { get; }
        
        public DmgInfo(int trueDmg, float force, Entity attacker, List<AtkTags> atkTags)
        {
            this.trueDmg = trueDmg;
            this.force = force;
            this.attacker = attacker;
            this.atkTags = atkTags;
        }
    }
}
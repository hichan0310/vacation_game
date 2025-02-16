using System.Collections.Generic;

namespace GameBackend.Events
{
    public class DmgInfo
    {
        public int trueDmg { get; }
        public float knuckBack { get; }
        public Entity attacker { get; }
        public List<AtkTags> atkTags { get; }
        
        public DmgInfo(int trueDmg, float knuckBack, Entity attacker, List<AtkTags> atkTags)
        {
            this.trueDmg = trueDmg;
            this.knuckBack = knuckBack;
            this.attacker = attacker;
            this.atkTags = atkTags;
        }
    }
}
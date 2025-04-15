using System.Collections.Generic;

namespace GameBackend.Events
{
    public class NormalAttackExecuteEvent:EventArgs
    {
        public Entity attacker { get; }
        public List<AtkTags> atkTags { get; }

        public NormalAttackExecuteEvent(Entity attacker, List<AtkTags> atkTags)
        {
            name="NormalAttackExecuteEvent";
            this.attacker = attacker;
            this.atkTags = atkTags;
        }

        public override void trigger()
        {
            attacker.eventActive(this);
        }
    }
}
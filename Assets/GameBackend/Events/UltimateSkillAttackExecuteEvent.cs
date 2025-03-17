using System.Collections.Generic;

namespace GameBackend.Events
{
    public class UltimateSkillAttackExecuteEvent:EventArgs
    {
        public Entity attacker { get; }
        public List<AtkTags> atkTags { get; }

        public UltimateSkillAttackExecuteEvent()
        {
            name="UltimateSkillAttackExecuteEvent";
        }
    }
}
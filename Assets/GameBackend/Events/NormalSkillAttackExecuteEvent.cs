using System.Collections.Generic;

namespace GameBackend.Events
{
    public class NormalSkillAttackExecuteEvent:EventArgs
    {
        public Entity attacker { get; }
        public List<AtkTags> atkTags { get; }

        public NormalSkillAttackExecuteEvent()
        {
            name="NormalSkillAttackExecuteEvent";
        }
    }
}

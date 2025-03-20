using System.Collections.Generic;
using GameBackend.Skills.NormalSkill;

namespace GameBackend.Events
{
    public class NormalSkillExecuteEvent:SkillExecuteEvent
    {
        public NormalSkillExecuteEvent(Entity caster, NormalSkill normalSkill):base(caster, normalSkill)
        {
            name="NormalSkillExecuteEvent";
        }
    }
}

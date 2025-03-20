using System.Collections.Generic;
using GameBackend.Skills.SpecialSkill;

namespace GameBackend.Events
{
    public class SpecialSkillExecuteEvent:SkillExecuteEvent
    {
        public SpecialSkillExecuteEvent(Entity caster, SpecialSkill specialSkill):base(caster, specialSkill)
        {
            name="SpecialSkillExecuteEvent";
        }
    }
}
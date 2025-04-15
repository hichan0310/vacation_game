namespace GameBackend.Events
{
    public class SkillExecuteEvent:EventArgs
    {
        protected Entity caster;
        protected Skill skill;

        protected SkillExecuteEvent(Entity caster, Skill skill)
        {
            this.name="SkillExecuteEvent";
            this.caster = caster;
            this.skill = skill;
        }

        public override void trigger()
        {
            caster.eventActive(this);
        }
    }
}
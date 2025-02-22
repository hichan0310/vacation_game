using GameBackend.Status;

namespace GameBackend.Buffs
{
    public class DefIgnore : Buff
    {
        private float decreaseDef;
        private float time;

        public DefIgnore(float decreaseDef, float time)
        {
            this.decreaseDef = decreaseDef;
            this.time = time;
        }
        
        
        private float timer = 0;

        public override void buffStatus(PlayerStatus status)
        {
            base.buffStatus(status);
            status.increaseDef -= decreaseDef;
        }

        public override void update(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= time)
            {
                removeSelf();
            }
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            target.buffStatus.Add(this);
        }

        public override void removeSelf()
        {
            foreach (var target in targets)
            {
                target.removeListener(this);
                target.removeBuff(this);
            }
        }
    }
}
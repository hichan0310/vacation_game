using GameBackend.Status;
using GameBackend.Events;

namespace GameBackend.ArtifactPrefabs.Grade_Legend
{
    public class CloneOfTheWarriorSword : Artifact, IBuffStatus
    {
        private int count = 0;
        private void Start()
        {
            this.name = "복제된 용사의 검";
            this.description = "적을 처치할 때마다 공격력이 1%씩 최대 100%까지 증가한다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if (eventArgs is EntityDieEvent && count < 100)
            {
                count++;
            }
        }

        public void buffStatus(PlayerStatus status)
        {
            status.increaseAtk += count;
        }
        
        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}

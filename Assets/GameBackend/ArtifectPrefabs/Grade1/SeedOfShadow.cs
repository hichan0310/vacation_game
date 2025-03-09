using GameBackend.Status;

namespace GameBackend.Artifects
{
    public class SeedOfShadow:Artifect, IBuffStatus
    {
        private void Start()
        {
            this.name = "암흑의 씨앗";
            this.description = "암흑 속성 피해가 15% 증가한다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.dmgUp[(int)AtkTags.shadowAttack] += 15;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
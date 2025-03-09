using GameBackend.Status;

namespace GameBackend.Artifects
{
    public class SeedOfFire:Artifect, IBuffStatus
    {
        private void Start()
        {
            this.name = "불의 씨앗";
            this.description = "불 속성 피해가 15% 증가한다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.dmgUp[(int)AtkTags.fireAttack] += 15;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
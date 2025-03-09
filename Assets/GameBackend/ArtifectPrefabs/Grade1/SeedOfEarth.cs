using GameBackend.Status;

namespace GameBackend.Artifects
{
    public class SeedOfEarth:Artifect, IBuffStatus
    {
        private void Start()
        {
            this.name = "대지의 씨앗";
            this.description = "땅 속성 피해가 15% 증가한다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.dmgUp[(int)AtkTags.earthAttack] += 15;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
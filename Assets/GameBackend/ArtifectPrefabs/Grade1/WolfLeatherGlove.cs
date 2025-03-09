using GameBackend.Status;

namespace GameBackend.Artifects
{
    public class WolfLeatherGlove:Artifect, IBuffStatus
    {
        private void Start()
        {
            this.name = "늑대 가죽 장갑";
            this.description = "방어력 5% 상승, 공격력 5% 상승";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.increaseAtk+=5;
            status.increaseDef+=5;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
using GameBackend.Status;

namespace GameBackend.Artifects
{
    public class WolfLeatherCoat:Artifect, IBuffStatus
    {
        private void Start()
        {
            this.name = "늑대 가죽 코트";
            this.description = "방어력 10% 상승";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.increaseDef+=10;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
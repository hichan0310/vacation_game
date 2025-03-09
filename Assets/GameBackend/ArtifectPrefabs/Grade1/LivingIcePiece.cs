using GameBackend.Status;

namespace GameBackend.Artifects
{
    public class LivingIcePiece:Artifect, IBuffStatus
    {
        private void Start()
        {
            this.name = "살아있는 얼음 결정";
            this.description = "치명타 확률이 5% 증가한다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.crit += 5;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
using GameBackend.Status;

namespace GameBackend.Artifects
{
    public class TeethOfGoblin:Artifect, IBuffStatus
    {
        private void Start()
        {
            this.name = "고블린의 이빨";
            this.description = "공격력이 10% 증가한다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.increaseAtk+=10;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
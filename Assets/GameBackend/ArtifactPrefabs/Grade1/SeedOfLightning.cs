using GameBackend.Status;

namespace GameBackend.ArtifactPrefabs.Grade1
{
    public class SeedOfLightning:Artifact, IBuffStatus
    {
        private void Start()
        {
            this.name = "번개의 씨앗";
            this.description = "번개 속성 피해가 15% 증가한다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.dmgUp[(int)AtkTags.lightningAttack] += 15;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
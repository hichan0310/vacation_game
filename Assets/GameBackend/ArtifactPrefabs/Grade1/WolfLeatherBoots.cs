﻿using GameBackend.Status;

namespace GameBackend.ArtifactPrefabs.Grade1
{
    public class WolfLeatherBoots:Artifact, IBuffStatus
    {
        private void Start()
        {
            this.name = "늑대 가죽 장화";
            this.description = "이동 속도 +10%, 점프 공격 피해 +10%";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.dmgUp[(int)AtkTags.jumpAttack] += 10;
            status.movePower *= 1.1f;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
﻿using GameBackend.Status;

namespace GameBackend.ArtifactPrefabs.Grade1
{
    public class SeedOfSword:Artifact, IBuffStatus
    {
        private void Start()
        {
            this.name = " 검의 씨앗";
            this.description = "물리 속성 피해가 15% 증가한다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.dmgUp[(int)AtkTags.physicalAttack] += 15;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
﻿using GameBackend.Status;

namespace GameBackend.ArtifactPrefabs.Grade2
{
    public class CoreOfWind:Artifact, IBuffStatus
    {
        private void Start()
        {
            this.name = "바람의 핵";
            this.description = "바람 속성 피해가 30% 증가한다. ";
        }
        
        public override void eventActive<T>(T eventArgs)
        {
            
        }

        public void buffStatus(PlayerStatus status)
        {
            status.dmgUp[(int)AtkTags.windAttack] += 15;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}
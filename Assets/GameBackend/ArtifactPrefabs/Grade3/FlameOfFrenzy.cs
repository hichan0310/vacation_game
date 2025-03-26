using System.Collections.Generic;
using GameBackend.Status;
using GameBackend.Events;
using UnityEngine;
using GameBackend.Buffs;

namespace GameBackend.ArtifactPrefabs.Grade3
{
    public class FlameOfFrenzy:Artifact, IBuffStatus
    {
        private int count = 0;
        private int max_count = 50;
        private float duration = 0;
        private float max_duration = 5;
        private void Start()
        {
            this.name = "광란의 불꽃";
            this.description = "데미지를 입힐 때마다 공격력이 1% 상승하여 50%까지 중첩된다. 지속시간은 5초이고 중첩되면 지속시간이 갱신된다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if(eventArgs is DmgGiveEvent eve && eve.target != player && count < max_count && duration > 0.01) // 마지막조건은 여러애 동시에 맞으면 너무 빨리 오르는거 아닌가 싶어서 넣은거라 빼도됨
            {
                duration = 0;
                count += 1;
            }
        }
        public override void update(float deltaTime)
        {
            duration += deltaTime;
            if (duration > max_duration)
            {
                count = 0;
            }
        }

        public void buffStatus(PlayerStatus status)
        {
            status.increaseAtk += count;
        }

        public override void registrarTarget(Entity target)
        {
            base.registrarTarget(target);
            player.addBuff(this);
        }
    }
}

using System.Collections.Generic;
using GameBackend.Status;
using GameBackend.Events;
using UnityEngine;
using GameBackend.Buffs;

namespace GameBackend.ArtifactPrefabs.Grade3
{
    public class DeathScythe:Artifact
    {
        private List<AtkTags> atkTags = new() { AtkTags.physicalAttack, AtkTags.artifactDamage, AtkTags.notcriticalHit};
        private void Start()
        {
            this.name = "사신의 낫";
            this.description = "체력 비율이 10% 미만이고 출혈 상태이상에 걸린 적을 공격하면 33% 확률로 즉사시킨다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if(eventArgs is DmgGiveEvent eve && eve.target != player && eve.target.status.nowHp < eve.target.status.maxHp * 0.1f && eve.target.GetComponentInChildren<BloodLoss>() != null && Random.value < 0.33f)
            {
                foreach(IEntityEventListener evelist in eve.target.eventListener)
                {
                    if(evelist is BloodLoss)
                    {
                        new DmgGiveEvent(999999, 0.5f, player, eve.target, atkTags);
                        break;
                    } 
                }

            }
        }
        public override void update(float deltaTime)
        {

        }
    }
}

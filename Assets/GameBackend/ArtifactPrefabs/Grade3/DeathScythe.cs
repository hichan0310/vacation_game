using System.Collections.Generic;
using System.Linq;
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
            this.description = "체력 비율이 10% 미만이고 출혈 상태이상에 걸린 적을 공격하면 33% 확률로 최대 체력에 해당하는 피해를 준다. ";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if(eventArgs is DmgGiveEvent eve)
            {
                if (eve.target.status.nowHp < eve.target.status.maxHp * 0.1f && Random.value < 0.33f)
                {
                    if (eve.target.buffStatus.OfType<BloodLoss>().Any())
                    {
                        
                    }
                }
            }
        }
        public override void update(float deltaTime)
        {

        }
    }
}

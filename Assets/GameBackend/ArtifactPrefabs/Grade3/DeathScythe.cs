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
        private Entity enemy;
        private void Start()
        {
            this.name = "사신의 낫";
            this.description = "체력 비율이 10% 미만이며 출혈 피해를 받는 적에게 최대 체력에 해당하는 피해를 준다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if(eventArgs is DmgGiveEvent eve)
            {
                foreach(IBuffStatus i in eve.target.buffStatus)
                {
                    if(i is BloodLoss && eve.target.status.nowHp < eve.target.status.maxHp * 0.1f && !eve.target.dead)
                    {
                        enemy = eve.target;
                        break;
                    }
                }
            }
        }
        public override void update(float deltaTime)
        {
            if(enemy != null)
            {
                new DmgGiveEvent(enemy.status.maxHp, 0f, player, enemy, atkTags).trigger();
                enemy = null;
            }
        }
    }
}

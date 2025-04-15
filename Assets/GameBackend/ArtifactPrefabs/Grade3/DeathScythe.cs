using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
            this.description = "체력 비율이 10% 미만이며 출혈 피해를 받는 적에게 최대 체력에 해당하는 피해를 준다.";
        }
        public override void eventActive<T>(T eventArgs)
        {
            if(eventArgs is DmgGiveEvent eve)
            {
                if(eve.atkTags.Contains(AtkTags.bloodLossDamage)) return;
                
                foreach(IEntityEventListener i in eve.target.eventListener)
                {
                    if(i is BloodLoss && eve.target.status.nowHp < eve.target.status.maxHp * 0.1f && !eve.target.dead)
                    {
                        var enemy = eve.target;
                        new DmgTakeEvent(enemy.status.maxHp, EmptyEntity.Instance, enemy, new List<AtkTags>()).trigger();
                        break;
                    }
                }
            }
        }
    }
}
